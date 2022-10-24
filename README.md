# Script Runner API

This project is a .Net Core API designed to run arbitrary PowerShell scripts stored in a Azure Storage blob container.

## Launching

1. Clone this repo
2. Open project folder `cd ScriptRunnerApi`
3. Restore dependencies `dotnet restore`
4. Run the API `dotnet run`

## Endpoints
For additional detail, run the API locally and browse to the swagger documentation.

### Execute a single powershell script

Returns output of script execution

**URL** : `/runscript/SCRIPT_NAME.ps1`

**Method** : `GET`

## Configuration

The API is configured via the appsettings.json file. Here is a sample configuration:

``` json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ScriptContainer": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=storageaccount;AccountKey=someaccountkey",
    "Container": "scripts"
  }
}
```
