
# Run multiple processes in a container
# https://docs.docker.com/config/containers/multi-service_container/

#!/bin/bash

# Start the first process
./api/WLog.API &

# Start the second process
./ui/WLog.Site &

# Wait for any process to exit
wait

# Exit with status of process that exited first
exit $?
