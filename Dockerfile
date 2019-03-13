#
#	LEAN Algorithm Docker Container November-2016
#	Cross platform deployment for multiple brokerages
#

FROM quantconnect/lean:foundation

MAINTAINER QuantConnect <contact@quantconnect.com>

################################
# Copy over binaries and data (mounting is not ideal):
COPY ./Launcher/bin/WrapRelease /root/Wrap
COPY ./Launcher/bin/Debug /root/Wrap/Lean/Launcher/bin/Debug
COPY ./Data /root/Wrap/Lean/Data

################################
# Change directory to Wrap
# WORKDIR /root/Lean/Launcher/bin/Debug
WORKDIR /root/Wrap

################################
# Kick off Wrap
# CMD ["mono", "QuantConnect.Lean.Launcher.exe"]
CMD ["mono", "wrap.exe", "-m", "slave", "-s", "1"]

# Usage: 
# docker build -t quantconnect/lean:foundation -f DockerfileLeanFoundation .
# docker build -t quantconnect/lean:algorithm -f Dockerfile .
# docker run -e ALGO_GROUP=1 quantconnect/lean:algorithm
