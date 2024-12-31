FROM mcr.microsoft.com/devcontainers/base:bullseye

# Install Java 11
RUN sudo apt-get update && \
    sudo apt-get install -y openjdk-11-jdk

COPY antlr-4.13.0-complete.jar antlr-4.13.0-complete.jar

ENTRYPOINT ["/bin/bash"]