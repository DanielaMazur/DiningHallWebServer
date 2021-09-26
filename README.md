# Run the app

```
$ docker build -t dining-hall-server-image -f ./DiningHallServer/Dockerfile .
$ docker run -d -p 3000:3000 --name dining-hall-server-container dining-hall-server-image
```

Open http://localhost:3000/
