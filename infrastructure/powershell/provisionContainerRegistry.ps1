param(
	[string] $subscriptionName = "Free Trial",
    [string] $resourceGroupName = "hobbystacks-rg",
    [string] $resourceGroupLocation = "EastUS",
    [string] $containerRegistryName = "hobbystacksRegistry",
    [string] $containerRegistrySku = "Basic"
)

Write-Output "> Connect to Azure"
Connect-AzAccount

Write-Output ""
Write-Output "> Create Resource Group: $resourceGroupName"
Write-Output ""

Get-AzResourceGroup -Name $resourceGroupName -ErrorVariable $notPresent -ErrorAction SilentlyContinue | Out-null

if ($notPresent)
{
    New-AzResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation
}
else
{
    Write-Output "Resource Group [$resourceGroupName] already exists."
}

Write-Output ""
Write-Output "> Create Azure Container Registry: $containerRegistryName"
Write-Output ""

Get-AzContainerRegistry -ResourceGroupName $resourceGroupName -Name $containerRegistryName -ErrorVariable $notPresent -ErrorAction SilentlyContinue

if ($notPresent)
{
    New-AzContainerRegistry -ResourceGroupName $resourceGroupName -Name $containerRegistryName -EnableAdminUser -Sku $containerRegistrySku
}
else
{
    Write-Output "Azure Container Registry [$containerRegistryName] already exists in Resource Group [$resourceGroupName]."
}