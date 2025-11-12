$RG="rg-myapp-dev"
$LOCATION="eastus"
$ACR_NAME="firstdocazappacr"  # change if needed; must be unique

az acr create `
  --resource-group $RG `
  --name $ACR_NAME `
  --sku Basic `
  --admin-enabled true










