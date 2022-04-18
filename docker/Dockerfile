FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/
ENV ASPNETCORE_ENVIRONMENT="Production"
COPY ../out/ .
ENTRYPOINT ["dotnet", "./IOKode.Butterfly.dll"]