docker container stop $(docker container ls --all -q --filter name=wlog)

docker rm $(docker container ls --all -q --filter name=wlog)

docker rmi $(docker images --filter "reference=wlog" -q)

docker rmi $(docker images --filter "dangling=true" -q)
