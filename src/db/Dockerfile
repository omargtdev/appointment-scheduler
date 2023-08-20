FROM mcr.microsoft.com/mssql/server:2022-latest

# Required env
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Str4ngP@ssword

# Adding tools to path
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Creating '/startup' and change to that dit
WORKDIR /startup

# Copying init.sql to WORKDIR directory
COPY . ./

# Add execution permissions
USER root
RUN chmod +x ./init-db.sh

EXPOSE 1433/tcp

# Creating/Initializing database with the respective user
USER mssql
ENTRYPOINT /bin/bash ./entrypoint.sh
