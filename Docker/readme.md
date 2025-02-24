Install docker desktop with wsl 2 enabled instead of Hyper-V
turn on WSL support in turn Windwos features on or of menu

a container basically runs a single piece of software (like redis, RabbitMQ etc.)

unlike VMs which have their own OS, containers share the machine's OS are much more lightweight

a container is an instance of an image
image is the template to create containers

dockerhub is not the only image registry, azure, aws, google, GitHub, harbor have their own ones

if you run "docker run nginx" and you don't have that image on your machine, docker will first pull the image and then run a container

docker ps - list the containers

docker stop {id} - stop but not delete the container

docker ps -a - list the containers including the stopped ones

docker rm {id} - remove the container completely

docker run -p 80:80 nginx - publishes the container's port to the host, the number left from the colon is the port that we gonna access the container from outside world, the number on the right is the port that the software inside the container listens to (nginx by default sits at port 80) 

you can specify multiple -p parameters
docker run -p 5672:5672 -p 15672:15672 RabbitMQ:3-management

if you have a container with id 933aa0673159 you can specify only first few characters to work with it like
docker rm -f 933 - remove forcefully the running container

docker run -d nginx - run the container in detached mode, like fire and forget, runs in the background, doesn't occupy the terminal

docker logs {id} - get latest logs
docker logs -f {id} - follow the logs in the current terminal window

docker attach {id} - to reattach terminal to the specified container

docker exec -it {id} {command} - execute a command inside the terminal like "docker exec -it a6b ls" or "docker exec -it a6b bash" and have fun inside the container

docker commit {id} {image_name} - create an image from existing container

rabbitmq:3-management - to the left of the colon is the image name, on the right is the tag, if you don't specify the tag it defaults to latest

rabbitmq versions with -management are those with user interface, without -management are more lightweight versions without GUI

tags must be immutable and always point to the same image, if not they will lead to confusions

docker rmi {image_name}:{tag_name} - will untag the image
docker rmi {image_name} - will remove the entire image

docker pull nginx - download nginx image

an image consists of different layers that are based on top of base image
- container layer
- image layer		|
- image layer		|\readonly
- image layer		|/
- base image layer	|

if 2 images use Debian base image, the latter won't be downloaded twice

dockerfile is a list of instructions to create an image

docker build . - path defines the build context directory, this copies everything from the path ignoring everything specified in the .dockerignore file, creates a zip/tar file and sends it to docker daemon
if you don't specify -f parameter it will look for the Dockerfile in the current directory, but you can also do "docker build -f .\DockerCourseApi\Dockerfile ."
we can also specify an image name and a tag "docker build -f .\DockerCourseApi\Dockerfile -t api:v1 ."

we need the dotnet sdk to run restore and publish commands

dockerfile starts with FROM which will specify the base image

WORKDIR /src - gets or creates a directory src inside the container and that becomes the current directory

COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"] - this will copy the .csproj file to a src's subdirectory called DockerCourseApi

.csproj file is like packages.json for node

if we don't change the .csproj file and run docker build => it won't run that layer again, will use the cache

COPY . . - copies everything to matching folder

you'd not want in production to have full .net sdk with your application => you'd go with more lightweight solution like mcr.microsoft.com/dotnet/aspnet:9.0 (runtime dotnet base image)

instead of running a whole bunch of docker run commands one for backend, one for db, one for frontend, you can run docker compose from docker-compose file

docker compose up - spins up all the containers specified in the file

docker compose up -d - for detached run

docker compose ps - listing all the containers in the docker compose

docker compose logs - show all container logs

docker compose logs {service_name} - see logs from only 1 service, service_name comes from .yaml file

docker compose down - removes all the created containers

docker compose build - build all the specified images

docker compose up --build - build before running (in case you've made some changes in code)

each id provided in docker-compose file is a dns entry

context path in docker-compose is relative to docker-compose.yml file

if you have a service that needs to be run after a specific one, use depends_on:[something]

your CI/CD will create an image and push it to container registry (like dockerhub), Kubernetes will pull the image and create containers

you can either push a docker image or a group of images with docker compose push

docker compose run database-seed - run the database seed
docker compose run database database-seed - run both the db and db seed

docker build -f .\DockerCourseApi\Dockerfile --network host . - if you see localhost => then treat it like the local network of container environment

if you remove a container its data will be lost
to address that you can create a volume(a piece of memory on hard drive) and make the container to point to it
then if you remove the container and spin up a new one, the new one can use the same volume and preserve the data

2 containers can point to same volume at the same time (maybe for some configurations)

besides volumes you have bind mount (host file system), tmpfs (host memory)

docker volume create {name} - create a volume

docker volume ls - list volumes

docker volume inspect dometrain - see details

docker volume rm {name} - remove volume

docker run ... -v sqldb-data:/var/opt/mssql - left of semicolon is path outside of container, on the right - inside the container

docker compose down -v - removes the containers alongside with volumes

docker network ls - get list of networks

docker network inspect {name} - explain the network

two containers in same network can talk to each other

bridge is the default network

docker network create {name} - create a network

different networks have different subnets => containers will have different ips

docker run --rm ... - means that when you close the shell the container will go away

docker run --network network-a alpine - create alpine container and attach it to network-a

container id is a dns entry in a custom network not in default bridge

docker network connect network-a container-b1 - connect container-b1 to network-a

docker run -it --rm --name container-a2 --network network-a --hostname dometrain alpine - specify hostname for the container (you can now "ping dometrain" by hostname) 

docker network disconnect network-a container-b1 - disconnect container-b1 from network-a

docker compose uses bridge network, ids become dns entries

in the same network all ports are available for the container to each other

use latest images for better security

container shares kernel with host (with OS), there's no VM

entrypoint ["echo", "hello"]
cmd ["world"] - cmd provides a default value when you run "docker run container_name" it will produce hello world
if you run "docker run container_name dometrain" you will get hello dometrain