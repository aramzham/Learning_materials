Purpose of this solution:
1. create a gRPC service
2. make it available for http calls with <a href="https://learn.microsoft.com/en-us/aspnet/core/grpc/json-transcoding?view=aspnetcore-7.0">JSON transcoding</a>

<img src="https://github.com/aramzham/Learning_materials/assets/25085025/933f5172-b25e-4999-a403-fb4bf79a6f1a"/>

For our example you'll need to run these commands to add corresponding packages:
```shell
dotnet add package grpc.aspnetcore
dotnet add package grpc.tools
dotnet add package microsoft.aspnetcore.grpc.jsontranscoding
dotnet add package microsoft.entityframeworkcore.design
dotnet add package microsoft.entityframeworkcore.sqlite
```