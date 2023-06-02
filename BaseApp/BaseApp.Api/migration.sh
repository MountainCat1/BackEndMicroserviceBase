# Define the length of the random string
length=10

# Generate random string
random_string=$(tr -dc 'a-zA-Z0-9' </dev/urandom | head -c"$length")

dotnet ef migrations add TempMigration_${random_string}

dotnet ef database update