param(
	[string] $mainAppDomain = "app.hobbystacks.com"
)

# ASSUMES THAT OPENSSL IS INSTALLED ON COMPUTER AND AVAILABLE THROUGH PATH.

$certPath = "..\..\data\certbot\conf\live\$mainAppDomain\"
$certPfx = $certPath + "cert.pfx"
$certPublicKey = $certPath + "fullchain.pem"
$certPrivateKey = $certPath + "privkey.pem"

Write-Output "> Create Certificate Directory"

if (!(Test-Path -Path $certPath ))
{
	New-Item -ItemType directory -Path $certPath | Out-null
	Write-Output " - Successfully exported the IIS Express Development Certificate."
}
else
{
	Write-Output " - Directory already exists."
}

Write-Output ""
Write-Output "> Export fullchain.pem and privkey.pem"

$pwd = Read-Host -Prompt " - Enter password for the certificate" -AsSecureString
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\) | Where Subject -eq "CN=localhost"
Export-PfxCertificate -Cert $cert -FilePath $certPfx -Password $pwd -ErrorVariable $notCreated -ErrorAction SilentlyContinue | Out-null

if ($notCreated)
{
	Write-Output " - Problem exporting the IIS Express Development Certificate."
}
else
{
	Write-Output " - Successfully exported the IIS Express Development Certificate."

	# We will pass the SecureString as a String to the openssl arguments.
	$unpwd = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($pwd))

	# NGINX cannot use .pfx files. We need to convert them to .pem files with openssl.
	openssl pkcs12 -in $certPfx -nocerts -out $certPrivateKey -passin pass:$unpwd -passout pass:$unpwd
	openssl pkcs12 -in $certPfx -clcerts -nokeys -out $certPublicKey -passin pass:$unpwd
	openssl rsa -in $certPrivateKey -out $certPrivateKey

	Remove-Item $certPfx
}

Write-Output ""
Write-Output "> Fetch options-ssl-nginx.conf and ssl-dhparams.pem files."
curl -Uri https://raw.githubusercontent.com/certbot/certbot/master/certbot-nginx/certbot_nginx/tls_configs/options-ssl-nginx.conf -OutFile "..\..\data\certbot\conf\options-ssl-nginx.conf"
curl -Uri https://raw.githubusercontent.com/certbot/certbot/master/certbot/ssl-dhparams.pem -OutFile "..\..\data\certbot\conf\ssl-dhparams.pem"