#!/bin/bash

# Check that we have exactly three arguments
if [ "$#" -ne 3 ]; then
  echo "Usage: $0 directory old_word new_word"
  exit 1
fi

directory="$1"
old_word="$2"
new_word="$3"

# Find all directories in the directory and its subdirectories
find "$directory" -depth -type d -name "*$old_word*" | while read dir; do
  new_dir=$(echo "$dir" | sed "s/$old_word/$new_word/g")
  mv -v "$dir" "$new_dir"
done

# Find all files in the directory and its subdirectories
find "$directory" -type f -name "*$old_word*" | while read file; do
  new_file=$(echo "$file" | sed "s/$old_word/$new_word/g")
  mv -v "$file" "$new_file"
done


echo "Replacing contents in files..."
echo "It can take a while!"

# Find all files in the directory and its subdirectories, and rename contents
find "$directory" -type f | while read file; do
  sed -i "s/$old_word/$new_word/g" "$file"
  echo "$file"
done

echo "Removing .sh files..."
echo "${directory}"
rm "${directory}"/createDomain.sh
rm "${directory}"/downloadBase.sh
rm "${directory}"/replace_all.sh


echo "Done."
