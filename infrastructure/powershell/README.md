# Infrastructure - PowerShell Scripts

## provisionContainerRegistry.ps1

### Overview

In order to run the PowerShell script, you will need to [install the Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest). You can refer to [Microsoft's quickstart guide](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-azure-cli) to learn more about the commands used in the script.

```PowerShell
.\provisionContainerRegistry.ps1 -resourceGroupName "hobbystacks-rg" -resourceGroupLocation "EastUS" -containerRegistryName "hobbystacksRegistry" -containerRegistrySku "Basic"
```

### Parameters

<!-- TODO: Move to /src/infrastructure/powershell/readme.md! -->

**`resourceGroupName`** *String*

Specifies a name for the resource group that will hold our Azure Container Registry. The resource name must be **unique in the subscription**.

**`resourceGroupLocation`** *String*

Specifies the location of the resource group. The resource group does not have to be in the same location your Azure subscription or in the same location as its resources. For regions offering Azure Container Registry, see [Products available by region](https://azure.microsoft.com/en-ca/global-infrastructure/services/?products=container-registry).

**`containerRegistryName`** *String*

Specifies a name for the Azure Container Registry. The registry name must be **unique within Azure (i.e. globally)**, and contain 5-50 alphanumeric characters.

**`containerRegistrySku`** *String*

Specifies the SKU for the Azure Container Registry. For details on available service tiers, see [Container registry SKUs](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-skus).

### Pulling from Azure Container Registry

<!-- TODO: Move to /src/infrastructure/powershell/readme.md! -->

Before pushing and pulling container images using `docker` commands, you must first log in to your container registry. The login server name is in the format `<registry-name>.azurecr.io` (all lowercase), for example, *hobbystacksregistry.azurecr.io*.

```PowerShell
docker login -username <azureUser> <acrLoginServer>
docker pull <acrLoginServer>/<registry-image-name>:v1

docker login -username <azureUser> hobbystacksregistry.azurecr.io
docker pull hobbystacksregistry.azurecr.io/hobby-stacks-api:v1
```

Don't forget to tag your images using the [docker tag](https://docs.docker.com/engine/reference/commandline/tag/) command before pushing them to your Azure Container Registry. Replace `<acrLoginServer>` with the login server name of your ACR instance.

```PowerShell
docker tag <local-image-name> <acrLoginServer>/<registry-image-name>:v1
docker push <acrLoginServer>/<registry-image-name>:v1

docker tag hobby-stacks-api hobbystacksregistry.azurecr.io/hobby-stacks-api:v1
docker push hobbystacksregistry.azurecr.io/hobby-stacks-api:v1
```