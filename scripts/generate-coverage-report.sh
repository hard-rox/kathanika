#!/bin/bash

# Generate code coverage report for .NET projects

# Run tests with coverage
dotnet test Kathanika.sln /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Generate HTML report
dotnet reportgenerator "-reports:./src/**/coverage.opencover.xml" "-targetdir:./coverage-report" "-reporttypes:Html"

echo "Coverage report generated in ./coverage-report directory"
echo "Open ./coverage-report/index.html to view the report"
