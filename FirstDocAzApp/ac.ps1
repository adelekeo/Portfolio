# Variables
$rg = "rg-myapp-dev"
$aciName = "chapter1-myap-aci"
$dnsLabel = "chapter1myap"    # must be unique in East US
$image = "firstdocazappacr.azurecr.io/chapter1/firstdocazapp:latest"

# 1. Delete existing container instance
az container delete `
  --resource-group $rg `
  --name $aciName `
  --yes

# 2. Re-create with DNS name label
az container create `
  --resource-group $rg `
  --name $aciName `
  --image $image `
  --dns-name-label $dnsLabel `
  --ports 80 `
  --cpu 1 `
  --memory 1 `
  --registry-login-server firstdocazappacr.azurecr.io `
  --registry-username <YOUR_ACR_USERNAME> `
  --registry-password <YOUR_ACR_PASSWORD>

# 3. Show FQDN
az container show `
  --resource-group $rg `
  --name $aciName `
  --query "ipAddress.fqdn" `
  --output tsv

