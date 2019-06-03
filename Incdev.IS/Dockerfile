FROM incdev:latest AS publish
WORKDIR /src/Incdev.IS/out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
EXPOSE 80
COPY --from=publish /src/Incdev.IS/out /app
ENTRYPOINT ["dotnet", "Incdev.IS.dll"]
