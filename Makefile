# Start a SQL Server 2022 container.
# Requires: Docker running, password set via env var PASSWORD (default: YourPassword123)
# Example:  make run/db PASSWORD=MyPass123
PASSWORD ?= YourPassword123

.PHONY: run/db
run/db:
	docker run -e "ACCEPT_EULA=Y" \
	           -e "MSSQL_SA_PASSWORD=$(PASSWORD)!" \
	           -p 1433:1433 \
	           --name sqlserver \
	           --hostname sqlserver \
	           -d mcr.microsoft.com/mssql/server:2022-latest

# Stop and remove the SQL Server container.
.PHONY: stop/db
stop/db:
	docker stop sqlserver && docker rm sqlserver


# Apply all pending EF Core migrations to the database.
.PHONY: migrations/up
migrations/up:
	cd App && dotnet ef database update

# Create a new EF Core migration.
# Requires: NAME variable  (e.g. make migrations/add NAME=AddPhoneToUser)
.PHONY: migrations/add
migrations/add:
	cd App && dotnet ef migrations add $(NAME)


# Build the application.
.PHONY: build
build:
	cd App && dotnet build

# Run the application (https://localhost:5001).
.PHONY: run/app
run/app:
	cd App && dotnet watch --urls "http://localhost:5001"


# Configure the SQL Server connection string via dotnet user-secrets.
# Requires: PASSWORD variable (default: YourPassword123)
# Example:  make setup/secrets PASSWORD=MyPass123
.PHONY: setup/secrets
setup/secrets:
	cd App && dotnet user-secrets init
	cd App && dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
	  "Server=localhost,1433;Database=DesafioAec;User Id=sa;Password=$(PASSWORD)!;TrustServerCertificate=True;"

# Full first-time setup: start DB, configure secrets, apply migrations.
# Requires: Docker running, PASSWORD variable (default: YourPassword123)
.PHONY: setup
setup: run/db setup/secrets migrations/up
