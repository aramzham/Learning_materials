FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DockerCourseApi.csproj", "./"]
RUN dotnet restore
COPY . .
#RUN dotnet build "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/build
#RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .

# change user to non-root (gecos means don't interactively prompt for various info)
RUN adduser --disabled-password --gecos '' appuser
USER appuser

ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
