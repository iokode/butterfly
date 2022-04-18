FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_ENVIRONMENT="Production"
COPY ../out/ /app
ENTRYPOINT ["dotnet", "/app/IOKode.Butterfly.dll"]