book-library
============

If you are experiencing any problems while the DB is initializing with the dummy data, please run the following commands inside `Nuget Package Manager Console`

```PowerShell
PM> SqlLocalDB.exe stop v11.0 
```

```PowerShell
PM> SqlLocalDB.exe delete v11.0 
```