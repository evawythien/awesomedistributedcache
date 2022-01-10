# Introduction 

The objective of that application is play with a distributed cache using redis as a in-memory storage.

# Building
To build the solution in Visual Studio, you'll need:
    Visual Studio 2019.

# Getting Started

To configure the environment and test the application, it's necessary to have installed:

- [Docker desktop](https://www.docker.com/products/docker-desktop)


## Setup Redis Commander with Docker Image.

Redis-Commander is a node.js web application used to view, edit and manage a Redis database.
First we download the most recent image from the [repository](https://hub.docker.com/_/redis).

```` 
docker pull rediscommander/redis-commander
```` 

Then if you're running redis on localhost:6379, this is all you need to get started is:
```` 
docker run --rm --name redis-commander -d -p 8081:8081 rediscommander/redis-commander:latest
```` 

Now you have your redis at http://localhost:8081
