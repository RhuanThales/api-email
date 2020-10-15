FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
EXPOSE 4100
COPY publish_output .
ENTRYPOINT ["dotnet", "api-email.dll"]
