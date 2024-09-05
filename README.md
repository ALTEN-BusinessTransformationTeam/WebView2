Build
```
dotnet build
```

Run
```
dotnet run 
```

or

Run and Watch
```
dotnet watch run
```

Publish

```
dotnet publish "my-new-app.csproj" -c Release -o ./publish --no-restore --runtime win-x64 --self-contained true
```


Publish 1 file

```
dotnet publish "my-new-app.csproj" -c Release -o ./publish --no-restore --runtime win-x64 --self-contained true /p:PublishTrimmed=true /p:PublishSingleFile=true
```
