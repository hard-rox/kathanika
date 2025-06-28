#!/bin/bash

# Restore .NET local tools
dotnet tool restore

# Verify tools installation
echo "Verifying .NET tools installation..."
dotnet tool list

echo "Tools restored successfully!"
