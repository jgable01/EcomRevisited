# Use the official Microsoft .NET Core SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EcomRevisited/EcomRevisited.csproj", "EcomRevisited/"]
RUN dotnet restore "EcomRevisited/EcomRevisited.csproj"
COPY . .
WORKDIR "/src/EcomRevisited"
RUN dotnet build "EcomRevisited.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcomRevisited.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "EcomRevisited.dll"]
