# Restore .NET local tools
dotnet tool restore

# Verify tools installation
Write-Host "Verifying .NET tools installation..." -ForegroundColor Cyan
dotnet tool list

Write-Host "Tools restored successfully!" -ForegroundColor Green
