# form-file-upload
This is a demonstration project which demonstrate how to do a file upload and store the files to S3 object store and how to update the MongoDB database in a ASP.NET MVC (.NET 5) app. This project also demonstrated how to make use of Rook-Ceph distributed storage in Kubernetes cluster.

## Prerequisites
- Docker engine
- docker-compose
- kubectl
- helm v3

## How to Build
```shell
docker-compose -f docker-compose.build.yml build
```

## How to Run Locally
```shell
docker-compose up
```

Then navigate to http://localhost:5000 to try to upload a file.

## How to Deploy to Kubernetes
```shell
cd k8s
helm install ffu .
```

Where `ffu` is the name of the helm release. You can use another name instead.
