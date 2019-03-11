#
#	LEAN Algorithm Docker Container November-2016
#	Cross platform deployment for multiple brokerages
#

FROM quantconnect/lean:foundation

MAINTAINER QuantConnect <contact@quantconnect.com>

################################
# Copy over binaries:
COPY ./Launcher/bin/Release /root/Lean/Launcher/bin/Release
COPY ./Launcher/bin/WrapRelease /root/Wrap

################################
# Change directory to Wrap
WORKDIR /root/Wrap

# What version are we using?
# CMD ["mono", "--version"]

# Kick off Wrap
CMD ["mono", "wrap.exe", "-m", "slave", "-s", "1"]

# Usage: 
# docker build -t quantconnect/lean:foundation -f DockerfileLeanFoundation .
# docker build -t quantconnect/lean:algorithm -f Dockerfile .
# docker run -v "C:\repos\quantybois\lean\Data:/root/Lean/Data" quantconnect/lean:algorithm
