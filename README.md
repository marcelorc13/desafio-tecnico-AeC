# DesafioAec

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)

---

## Executando localmente

### 1. Clone o repositório

```bash
git clone https://github.com/youruser/DesafioAec.git
cd DesafioAec
```

### 2. Inicie o SQL Server com Docker

```bash
docker run -e "ACCEPT_EULA=Y" \
           -e "MSSQL_SA_PASSWORD=YourPassword123!" \
           -p 1433:1433 \
           --name sqlserver \
           --hostname sqlserver \
           -d mcr.microsoft.com/mssql/server:2022-latest
```

Aguarde alguns segundos para o container estar pronto e verifique:

```bash
docker ps # sqlserver deve aparecer como em execução
```

### 3. Configure a string de conexão

```bash
cd App
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=localhost,1433;Database=DesafioAec;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;"
```

### 4. Execute as migrations

```bash
dotnet ef database update
```

### 5. Execute a aplicação

```bash
dotnet run 
# ou `dotnet watch` para hot reload
```

A aplicação estará disponível em `https://localhost:5001`
