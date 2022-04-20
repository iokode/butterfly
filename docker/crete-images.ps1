docker build . -t butterfly:latest-amd64 --file ./docker/Dockerfile-amd64
docker build . -t butterfly:latest-arm64v8 --file ./docker/Dockerfile-arm64v8
docker tag butterfly:latest-amd64 butterfly:latest