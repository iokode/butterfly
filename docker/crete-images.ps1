param (
    [Parameter(Mandatory = $true)] [string] $Version
)

docker build . -t iokode/butterfly:$Version-amd64 --file ./docker/Dockerfile-amd64
docker build . -t iokode/butterfly:$Version-arm64v8 --file ./docker/Dockerfile-arm64v8
docker tag iokode/butterfly:$Version-amd64 iokode/butterfly:latest-amd64
docker tag iokode/butterfly:latest-amd64 iokode/butterfly:latest
docker tag iokode/butterfly:$Version-arm64v8 iokode/butterfly:latest-arm64v8