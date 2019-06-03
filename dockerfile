#Build whole solution
FROM microsoft/dotnet:2.2-sdk as build-env
WORKDIR /src
COPY . ./
RUN dotnet publish -c Release -o out | tee log.txt