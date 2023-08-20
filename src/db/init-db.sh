#!/bin/env bash

# Try 50 times to execute database
for i in {1..50};
do
    sqlcmd -U sa -P $MSSQL_SA_PASSWORD -i sql/init.sql
    if [ $? -eq 0 ]
    then
        echo "database up!"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done

