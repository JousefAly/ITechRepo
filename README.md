# ITechRepo
## Launching your copy
1. Add file appsettings.json to the directory : ITech/
2. paste this snippet below in the file
```
{
  "ConnectionStrings": {
    "DefaultConnection": "yourconnectionstring" 
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

```
here is example of my connection string

`"Server=(localdb)\\mssqllocaldb;Database=ITechAppData;Trusted_Connection=True;MultipleActiveResultSets=true"`

3.update-database in package manager console
