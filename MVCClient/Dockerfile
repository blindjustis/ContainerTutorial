FROM incdev:latest AS publish
WORKDIR /src/MVCClient/out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
EXPOSE 80
COPY --from=publish /src/MVCClient/out /app
ENTRYPOINT ["dotnet", "MVCClient.dll"]