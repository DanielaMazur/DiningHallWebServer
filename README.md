# Run the app

1. Create the <i>kitchen</i> bridge network.
   (It should be created only once for both Severs)

```
$ docker network create kitchen
```

2. Build the project and create the <i>dining-hall-server-image</i>. After that create a container (for the previously created image) called <i>dining-hall-server-container</i>. The container will run over the <i>kitchen</i> network, on the port 3000. This configurations will allow the KitchenServer to communicate with DiningHallServer using the **dining-hall-server-container** host.

```
$ docker build -t dining-hall-server-image -f ./DiningHallServer/Dockerfile .
$ docker run --net kitchen -d -p 3000:3000 --name dining-hall-server-container dining-hall-server-image
```
