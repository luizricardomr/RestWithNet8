FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithNet8.Api/db/migrations/ /home/database/
COPY ./RestWithNet8.Api/db/dataset/ /home/database/
COPY ./RestWithNet8.Api/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh