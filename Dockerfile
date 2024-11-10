# Usa la imagen de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establece el directorio de trabajo en /app
WORKDIR /app

# Copia el archivo de solución y los archivos de proyecto
COPY *.sln ./
COPY Domain/Domain.csproj Domain/
COPY Application/Application.csproj Application/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY API/API.csproj API/

# Restaura las dependencias de todos los proyectos
RUN dotnet restore

# Copia el resto del código fuente y compila la aplicación
COPY . .
RUN dotnet publish API/API.csproj -c Release -o /publish

# Usa la imagen de ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Establece el directorio de trabajo en /app
WORKDIR /app

# Copia los archivos publicados desde la etapa anterior
COPY --from=build /publish .

# Expone el puerto en el que corre la API
EXPOSE 8080

# Define el comando de inicio para la API
ENTRYPOINT ["dotnet", "API.dll"]
