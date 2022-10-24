## Script Runner API

This project is a .Net Core API designed to run arbitrary PowerShell scripts stored in a Azure Storage blob container.

## Endpoints
For additional detail, run the API locally and browse to the swagger documentation.

### Execute a single powershell script

Returns output of script execution

**URL** : `/runscript/SCRIPT_NAME.ps1`

**Method** : `GET`

# Configuration

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
