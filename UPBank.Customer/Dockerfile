#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UPBank.Customer.API/UPBank.Customer.API.csproj", "UPBank.Customer.API/"]
COPY ["UPBank.Customer.Domain/UPBank.Customer.Domain.csproj", "UPBank.Customer.Domain/"]
COPY ["UPBank.Customer.Infra/UPBank.Customer.Infra.csproj", "UPBank.Customer.Infra/"]
COPY ["UPBank.Customer.Application/UPBank.Customer.Application.csproj", "UPBank.Customer.Application/"]

RUN dotnet restore "./UPBank.Customer.API/UPBank.Customer.API.csproj"
COPY . .
WORKDIR "/src/UPBank.Customer.API"
RUN dotnet build "./UPBank.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UPBank.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UPBank.Customer.API.dll"]