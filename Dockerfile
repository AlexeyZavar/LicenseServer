FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RadolynAPI.X/RadolynAPI.X.csproj", "RadolynAPI.X/"]
COPY ["RadolynAPI.X.Database/RadolynAPI.X.Database.csproj", "RadolynAPI.X.Database/"]
COPY ["RadolynAPI.X.Core/RadolynAPI.X.Core.csproj", "RadolynAPI.X.Core/"]
RUN dotnet restore "RadolynAPI.X/RadolynAPI.X.csproj"
COPY . .
WORKDIR "/src/RadolynAPI.X"
RUN dotnet build "RadolynAPI.X.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadolynAPI.X.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadolynAPI.X.dll"]
