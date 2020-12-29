FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /api
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY "ProvaML.sln" "ProvaML.sln"
COPY "ProvaML.API/ProvaML.API.csproj" "ProvaML.API/ProvaML.API.csproj"
COPY "ProvaML.API.IntegrationTests/ProvaML.API.IntegrationTests.csproj" "ProvaML.API.IntegrationTests/ProvaML.API.IntegrationTests.csproj"
COPY "ProvaML.Application/ProvaML.Application.csproj" "ProvaML.Application/ProvaML.Application.csproj"
COPY "ProvaML.Domain/ProvaML.Domain.csproj" "ProvaML.Domain/ProvaML.Domain.csproj"
COPY "ProvaML.Infrastructure/ProvaML.Infrastructure.csproj" "ProvaML.Infrastructure/ProvaML.Infrastructure.csproj"
COPY "ProvaML.Infrastructure.Tests/ProvaML.Infrastructure.Tests.csproj" "ProvaML.Infrastructure.Tests/ProvaML.Infrastructure.Tests.csproj"
COPY "ProvaML.Migration/ProvaML.Migration.csproj" "ProvaML.Migration/ProvaML.Migration.csproj"
RUN dotnet restore
COPY . .
WORKDIR /src/ProvaML.API
RUN dotnet build -c Release -o /api

FROM build AS publish
RUN dotnet publish -c Release -o /api

FROM base AS final
WORKDIR /api
COPY --from=publish /api .
ENTRYPOINT ["dotnet", "ProvaML.API.dll"]