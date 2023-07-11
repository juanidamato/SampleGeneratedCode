rem https://dotnetthoughts.net/generating-code-coverage-reports-in-dotnet-core/
rem dotnet tool install --global dotnet-coverage
rem dotnet tool install --global dotnet-reportgenerator-globaltool
rem dotnet coverage collect dotnet test --output .\Tests\CodeCoverage.cobertura.xml --output-format cobertura
rem reportgenerator -reports:.\Tests\CodeCoverage.cobertura.xml -targetdir:".\Tests\CoverageReport" -reporttypes:Html -assemblyfilters:+MinimalApi
rem google-chrome .\Tests\CoverageReport\index.htm