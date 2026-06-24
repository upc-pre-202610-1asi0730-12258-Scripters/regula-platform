FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app

COPY Scripters.Regula.Platform/*.csproj Scripters.Regula.Platform/
RUN dotnet restore ./Scripters.Regula.Platform

COPY . .
RUN dotnet publish ./Scripters.Regula.Platform -c Release -o out
    
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Scripters.Regula.Platform.dll"]