#!/bin/bash

# Prompt for parameter 1
read -p "Domain name: " domainName

tempDir="${domainName}_temp"

echo "### Downloading repo..."
bash ./downloadBase.sh ./"${tempDir}"

echo "### Replacing placeholders..."
bash ./replace_all.sh ./"${tempDir}" BaseApp "$domainName"
bash ./replace_all.sh ./"${tempDir}" BaseApp "$domainName"
bash ./replace_all.sh ./"${tempDir}" BaseApp "$domainName"
bash ./replace_all.sh ./"${tempDir}" BaseApp "$domainName"


echo "### Moving project out of temp directory..."
mv "$tempDir/$domainName" "$domainName"

echo "### Removing temp directory..."
rm -f -r "$tempDir"

