#!/bin/bash

# Default values for command-line arguments.
email="" # Adding a valid address is strongly recommended
rsaKeySize=4096
certbotDataPath="" # From scripts' execution context/directory.
isStaging=0 # Set to 1 if you're testing your setup to avoid hitting request limits

# The first domain or subdomain will be used by certbot for the certificate
# location directory .
domains=()

# Parse command-line arguments.
POSITIONAL=()
while [[ $# -gt 0 ]]
do
	key="$1"

	case $key in
		-e|--email)
			email=$2
			shift 2 # Past argument and value.
			;;
		-k|--keysize)
			rsaKeySize=$2
			shift 2 # Past argument and value.
			;;
		-p|--datapath)
			certbotDataPath=$2
			shift 2 # Past argument and value.
			;;
		-d|--domains)
			while [[ ! "$2" =~ ^- ]] && [ ! -z "$2" ];
			do
				domains+=("$2")
				shift # Past argument or next value.
			done
			shift # Past last value.
			;;
		-s|--staging)
			isStaging=1
			shift # Past argument and value.
			;;
		--) # End argument parsing.
			shift
			break
			;;
		*) # Unknown options.
			POSITIONAL+=("$1") # Save it in an array for later.
			shift # Past argument.
			;;
	esac
done

# Validation
if [ -z "$email" ]
then
	echo "Missing -e|--email argument for Let's Encrypt email registration."
	exit 1
fi

if [ -z "$certbotDataPath" ]
then
	echo "Missing -p|--datapath argument for certbot's data directory."
	exit 1
fi

if [ -z "$domains" ]
then
	echo "Missing -d|--domains argument for list of domains to register with Let's Encrypt."
	exit 1
fi

# Set positional arguments in their proper place.
eval set -- "$POSITIONAL"

if [ -d "$certbotDataPath" ]; then
  read -p "Existing data found in '$certbotDataPath'. Continue and replace existing certificate? (y/N) " decision
  echo
  if [ "$decision" == "Y" ] || [ "$decision" == "y" ]; then
    echo "### Deleting existing files."
    rm -rf $certbotDataPath/
  else
    echo "### Initialization process aborted."
    exit
  fi
fi

echo

if [ ! -e "$certbotDataPath/conf/options-ssl-nginx.conf" ] || [ ! -e "$certbotDataPath/conf/ssl-dhparams.pem" ]; then
  echo "### Downloading recommended TLS parameters ..."
  mkdir -p "$certbotDataPath/conf"
  curl -s https://raw.githubusercontent.com/certbot/certbot/master/certbot-nginx/certbot_nginx/tls_configs/options-ssl-nginx.conf > "$certbotDataPath/conf/options-ssl-nginx.conf"
  curl -s https://raw.githubusercontent.com/certbot/certbot/master/certbot/ssl-dhparams.pem > "$certbotDataPath/conf/ssl-dhparams.pem"
  echo
fi

path="/etc/letsencrypt/live/${domains[0]}"
echo "### Creating dummy certificate for domains."
mkdir -p "$certbotDataPath/conf/live/${domains[0]}"
docker-compose run --rm --entrypoint "\
  openssl req -x509 -nodes -newkey rsa:1024 -days 1\
    -keyout '$path/privkey.pem' \
    -out '$path/fullchain.pem' \
    -subj '/CN=localhost'" certbot
echo

echo "### Starting NGINX container and its dependencies."
docker-compose down
docker-compose up --force-recreate -d nginx
echo

echo "### Deleting dummy certificate for domains."
docker-compose run --rm --entrypoint "\
  rm -Rf /etc/letsencrypt/live/${domains[0]} && \
  rm -Rf /etc/letsencrypt/archive/${domains[0]} && \
  rm -Rf /etc/letsencrypt/renewal/${domains[0]}.conf" certbot
echo

echo "### Requesting Let's Encrypt certificate for domains:"
echo "###   ${domains[@]}"

# Build certbot domain arguments by joining $domains and '-d'.
domain_args=""
for domain in "${domains[@]}"; do
  domain_args="$domain_args -d $domain"
done

# Select appropriate email arg
case "$email" in
  "") email_arg="--register-unsafely-without-email" ;;
  *) email_arg="--email $email" ;;
esac

# Enable staging mode if needed
if [ $isStaging != "0" ]; then staging_arg="--staging"; fi

# Requests all domains/subdomains under the same certificate as diffent
# Subject Alternative Names (DNS) entries.
docker-compose run --rm --entrypoint "\
  certbot certonly --webroot -w /var/www/certbot \
    $staging_arg \
    $email_arg \
    $domain_args \
    --rsa-key-size $rsaKeySize \
    --agree-tos \
    --force-renewal \
    --noninteractive" certbot
echo

echo "### Reloading NGINX."
docker-compose exec nginx nginx -s reload