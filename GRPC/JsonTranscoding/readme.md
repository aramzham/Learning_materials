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

Update your ```appsettings.json``` with code below.<br/>
Http1 calls you will need for api requests, http2 for gRPC ones.
```json lines
"Kestrel": {
    "EndpointDefaults": {
      "Protocols": "Http1AndHttp2"
    }
  }
```

After you have wired up the db in the ```Program.cs``` like this:<br/>
```csharp
builder.Services.AddDbContextPool<AppDbContext>(o => o.UseSqlite("Data Source=ToDoDatabase.db"));
```
run the EF migration script to create the database:
```shell
dotnet ef migrations add InitialMigration
dotnet ef database update
```

When you add a new .proto file into your project, do not forget to add it in the .csproj file as well.
```xml
<Protobuf Include="Protos\todo.proto" GrpcServices="Server" />
```

In order to be able to use json transcoding, copy the ```annotations.proto``` and ```http.proto``` from <a href="https://learn.microsoft.com/en-us/aspnet/core/grpc/json-transcoding?view=aspnetcore-7.0">the link</a> and paste them into ```/google/api``` folder.

<hr>

For more information check out this <a href="https://www.youtube.com/watch?v=Rqz9XiSqH3E">tutorial by Les Jackson</a>.