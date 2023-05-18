#!/bin/bash

# Prompt for parameter 1
read -p "Domain name: " domainName

bash ./downloadBase.sh ./"${domainName}"

bash ./replace_all.sh ./"${domainName}" BaseApp "$domainName"
bash ./replace_all.sh ./"${domainName}" BaseApp "$domainName"
bash ./replace_all.sh ./"${domainName}" BaseApp "$domainName"
bash ./replace_all.sh ./"${domainName}" BaseApp "$domainName"

