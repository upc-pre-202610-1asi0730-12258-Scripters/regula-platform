FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
COPY ["Scripters.Regula.Platform/Scripters.Regula.Platform.csproj", "Scripters.Regula.Platform/"]
RUN dotnet restore "Scripters.Regula.Platform/Scripters.Regula.Platform.csproj"
COPY . .
RUN dotnet build "./Scripters.Regula.Platform.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Scripters.Regula.Platform.dll"]
