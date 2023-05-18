#!/bin/bash

# Define repository URL
repo_url="https://github.com/MountainCat1/BackEndMicroserviceBase.git"

# Check if destination directory argument is provided
if [ $# -eq 0 ]; then
    echo "Usage: $0 <destination_directory>"
    exit 1
fi

# Get the destination directory from the argument
destination_dir="$1"

# Clone the repository to the destination directory
git clone "$repo_url" "$destination_dir"

# Remove Git from the cloned repository
cd "$destination_dir" && rm -rf .git

echo "Repository cloned and Git removed successfully."
