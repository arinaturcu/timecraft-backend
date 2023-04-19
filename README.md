# TimeCraft

docker-compose -f .\docker-compose.yml -p timecraft-app-db up -d

You can use PGAdmin (https://www.pgadmin.org/) or DBeaver (https://dbeaver.io/download/) to access the database on localhost:5432 with database/user/password "timecraft". 

To work with the database migrations in .NET install the dotnet-ef tool by using the following command:

dotnet tool install --global dotnet-ef --version 6.*

To create a new migration use the following command and replace migration_name with the name of your new migration, usually the first migration is called "InitialCreate":

dotnet ef migrations add <migration_name> --context WebAppDatabaseContext --project .\TimeCraft.Infrastructure --startup-project .\TimeCraft.Backend