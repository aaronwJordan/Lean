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
# Kick off Wrap
WORKDIR /root/Wrap
ENTRYPOINT ["dotnet", "wrap.dll"]
CMD ["--mode slave", "--slaves 2"]

# Usage: 
# docker build -t quantconnect/lean:foundation -f DockerfileLeanFoundation .
# docker build -t quantconnect/lean:algorithm -f Dockerfile .
# docker run -v "C:\QuantyBois\Lean\Data:/root/Lean/Data" quantconnect/lean:algorithm
