# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia os arquivos e restaura os pacotes
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Expõe a porta
EXPOSE 80

ENTRYPOINT ["dotnet", "CofreDeIdeias.dll"]
