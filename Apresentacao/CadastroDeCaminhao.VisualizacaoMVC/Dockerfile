#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 4000:80
EXPOSE 5000:443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Apresentacao/CadastroDeCaminhao.VisualizacaoMVC/CadastroDeCaminhao.VisualizacaoMVC.csproj", "Apresentacao/CadastroDeCaminhao.VisualizacaoMVC/"]
COPY ["Core/CadastroDeCaminhao.Aplicacao/CadastroDeCaminhao.Aplicacao.csproj", "Core/CadastroDeCaminhao.Aplicacao/"]
COPY ["Core/CadastroDeCaminhao.Dominio/CadastroDeCaminhao.Dominio.csproj", "Core/CadastroDeCaminhao.Dominio/"]
COPY ["Infraestrutura/CadastroDeCaminhao.Data/CadastroDeCaminhao.Data.csproj", "Infraestrutura/CadastroDeCaminhao.Data/"]
RUN dotnet restore "Apresentacao/CadastroDeCaminhao.VisualizacaoMVC/CadastroDeCaminhao.VisualizacaoMVC.csproj"
COPY . .
WORKDIR "/src/Apresentacao/CadastroDeCaminhao.VisualizacaoMVC"
RUN dotnet build "CadastroDeCaminhao.VisualizacaoMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroDeCaminhao.VisualizacaoMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroDeCaminhao.VisualizacaoMVC.dll"]
