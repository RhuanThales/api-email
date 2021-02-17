FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
EXPOSE 4800
COPY publish_output .
ENTRYPOINT ["dotnet", "api-email.dll"]
