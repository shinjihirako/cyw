# Prompt for Environment Input
$environment = Read-Host "Enter the environment (e.g., prd, dev, test)"

# Define global variables
$appname = "basicfit"
$location = "westeurope"
$resourceGroup = "${environment}-${appname}-we-rg"
$servicePrincipalName = "${environment}-${appname}-sp"
$acrName = "${environment}${appname}weacr"
$tenantId = "e514a52d-7ce5-48b1-bdb9-b0c04ea568a1"

# Function to create a Resource Group
function Create-ResourceGroup {
    if (-not (az group show --name $resourceGroup --query "name" -o tsv)) {
        az group create --name $resourceGroup --location $location
        Write-Host "Resource Group $resourceGroup created."
    } else {
        Write-Host "Resource Group $resourceGroup already exists."
    }
}

# Function to create a Service Principal and assign it to a Resource Group
function Create-ServicePrincipal {
    # Check if the Service Principal already exists
    $spExists = az ad sp list --display-name $servicePrincipalName --query "[].appId" -o tsv
    if (-not $spExists) {
        # Create the Service Principal
        $sp = az ad sp create-for-rbac --name $servicePrincipalName
        
        # Assign a role (Contributor) to the Service Principal for the Resource Group
        az role assignment create --assignee $sp.appId --role Contributor --resource-group $resourceGroup
        
        Write-Host "Service Principal $servicePrincipalName created and assigned Contributor role in Resource Group $resourceGroup."
    } else {
        Write-Host "Service Principal $servicePrincipalName already exists."
    }
}

# Function to create Azure Container Registry (ACR)
function Create-ACR {
    if (-not (az acr show --name $acrName --resource-group $resourceGroup --query "name" -o tsv)) {
        az acr create --name $acrName --resource-group $resourceGroup --location $location --sku Basic --admin-enabled true
        Write-Host "Container Registry $acrName created."
    } else {
        Write-Host "Container Registry $acrName already exists."
    }
}

# Function to select which resources to create
function Create-Resources {
    Write-Host "Select resources to create:"
    Write-Host "1. Resource Group"
    Write-Host "2. Service Principal"
    Write-Host "3. Container Registry (ACR)"
    Write-Host "4. All Resources"

    $choice = Read-Host "Enter the number(s) of the resource(s) to create (comma-separated)"

    # Split the input by commas and create each selected resource
    $selectedChoices = $choice.Split(",")

    foreach ($item in $selectedChoices) {
        switch ($item.Trim()) {
            "1" { Create-ResourceGroup }
            "2" { Create-ServicePrincipal }
            "3" { Create-ACR }
            "4" { 
                Create-ResourceGroup
                Create-ServicePrincipal
                Create-ACR
            }
            default { Write-Host "Invalid selection: $item" }
        }
    }
}

# Run the resource creation process
Create-Resources
