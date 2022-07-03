param (
    [Parameter(Mandatory = 1)] [string] $Version
)

docker push iokode/butterfly:latest
docker push iokode/butterfly:latest-amd64
docker push iokode/butterfly:latest-arm64v8
docker push iokode/butterfly:$Version-amd64
docker push iokode/butterfly:$Version-arm64v8