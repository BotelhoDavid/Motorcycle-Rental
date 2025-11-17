# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
# Porta padrão para o ASP.NET Core
EXPOSE 8080 

# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copia a Estrutura do Projeto para RESTORE
# COPIAMOS TODAS AS PASTAS E ARQUIVOS DENTRO de 'src/RentApi/' para o diretório 'RentApi/'
# Isso garante que todos os arquivos .csproj e a estrutura de pastas necessária para o restore existam.
COPY src/RentApi/ RentApi/

# 2. Executa o Restore
# O restore agora tem certeza de que todos os .csproj estão presentes.
RUN dotnet restore RentApi/API/Rent.API.csproj

# 3. Copia o Código-Fonte Restante (DEPOIS do restore)
# O código-fonte completo só é copiado aqui para invalidar o cache APENAS se o código mudar.
COPY . .

# 4. Move para o diretório de construção da API
WORKDIR /src/RentApi/API
RUN dotnet build "Rent.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- Publish Stage ---
FROM build AS publish
WORKDIR /src/RentApi/API
# Publica o projeto
RUN dotnet publish "Rent.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Final runtime Stage ---
FROM base AS final
WORKDIR /app
# Copia o resultado do publish para a imagem final
COPY --from=publish /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Rent.API.dll"]