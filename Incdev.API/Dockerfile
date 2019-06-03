FROM incdev:latest AS publish
WORKDIR /src/Incdev.API/out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
EXPOSE 80
COPY --from=publish /src/Incdev.API/out /app
ENTRYPOINT ["dotnet", "Incdev.API.dll"]
