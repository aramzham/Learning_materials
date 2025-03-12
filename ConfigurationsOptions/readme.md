Configuration is a set of parameters that control and influence our application
without configurations our apps would become hardcoded artifacts

configuration gives us the flexibility to run the application based f.e. on the environment we are running

Why configurations are important:
1. maintainability - without changing the code you can change your app's behaviors
2. security - don't hardcode sensitive information in your codebase which can be exposed
3. flexibility - when you change the environment you don't change your code

configuration sources
- environment variables
- command line arguments
- json files
- user secrets
- cloud vaults
- in-memory configuration
- custom implementations

you've got configuration sources as described above and you've got corresponding configuration providers which are the way to materialize those sources (f.e. how to read and represent the json file)

standalone application is that when you don't leverage anything from your framework, f.e. with asp.net core you get DI, logging etc.

Microsoft.Extensions.Hosting depends on 30 transitive packages!!

json configs are environment specific, you have appsettings.Development.json, appsettings.Testing.json etc.

ConfigurationManager = config root + builder

- user secrets are intended for development environment only
- the values are stored on your machine, they are not encrypted
- the file is outside of source control
- for `windows` - %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
  for `linux/macOS` - ~/.Microsoft/usersecrets/<user_secrets_id>/secrets.json
- JsonConfigurationProvider is used for secrets.json source

3 ways you can set up your user secrets
1. create a file with UserSecretsId attribute and provide a guid
2. use your IDE manage user secrets capabilities
3. use `dotnet user-secrets init` command line tooling

dotnet user-secrets init
dotnet user-secrets set "Something:Nested" "value"
dotnet user-secrets list

$env:MYAPP_Delay = "0:0:17"
use such commands to add env vars using powershell
__ will mean something nested

there're plenty of ways to add command line arguments to application
`dotnet run -d "0:0:03" --apiOn=true /todoUri "https://jsonplaceholder.typicode.com/todos"`

in key-per file scenario the name of the file goes as the key and the contents are the values

to implement your custom configuration provider
1. add reference to `Microsoft.Extensions.Configuration.Abstractions`
2. implement the `IConfigurationSource` interface
3. implement the `IConfigurationProvider` interface
4. write extension methods on the `IConfigurationBuilder` for adding sources

given the configuration source stream, the configuration provider must somehow load it into memory

the "options" pattern provides strongly typed classes through DI container

# Options interfaces

- `IOptions<TOptions>`:
    - Singleton service lifetime.
    - only read once, at app startup
- `IOptionsSnapshot<TOptions>`:
    - scoped service lifetime.
    - values are recomputed for each new scope.
    - designed for transient and scoped dependencies.
- `IOptionsMonitor<TOptions>`:
    - singleton service lifetime.
    - enables change detection.
    - supports dynamic reloading of values.

# Monitoring limitations
1. limited to file-system configuration providers:
    - Microsoft.Extensions.Configuration.Ini
    - Microsoft.Extensions.Configuration.Json
    - Microsoft.Extensions.Configuration.KeyPerFile
    - Microsoft.Extensions.Configuration.UserSecrets
    - Microsoft.Extensions.Configuration.Xml
2. some environments, such as File Shares or Docker containers are unreliable for change notifications.
    - set `DOTNET_USE_POLLING_FILE_WATCHER` to `true` for polling every 4 seconds
