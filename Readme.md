# Getting started

Run redis:
```
docker pull redis
docker run --name my-redis -p 5002:6379 -d redis
```

Redis runned on port 5002

Install EntityFramework CLI:
```
dotnet tool install --global dotnet-ef
```


From root
```
cd RedisMasstransitConsumer
dotnet ef database update

```
From root

```
dotnet run --project RedisMasstransitConsumer
```

Run 2+ instances RedisMasstransitDemoApp
```
dotnet run --project RedisMasstransitDemoApp
```

