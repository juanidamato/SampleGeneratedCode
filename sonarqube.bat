dotnet sonarscanner begin /k:"samplegeneratedcode" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_e7e39d4b57200881c396bb52d692d32641ed13e8"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_e7e39d4b57200881c396bb52d692d32641ed13e8"