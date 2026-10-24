# Ja-Notghi — Social Web Application

A collaborative React and ASP.NET Core application with account registration, login, profile services, follow relationships, and email-based password recovery.

## Stack and structure

- `client/`: React 18 browser application.
- `server/API/`: ASP.NET Core 8 API, authentication, DTOs, and HTTP setup.
- `server/Application/`, `server/Domain/`, `server/Infrastructure/`: services, domain entities, persistence, and email integration.
- `server/Test/`: existing backend tests.
- `compose.yaml`: PostgreSQL, migration, backend, and frontend services.

## Local configuration

Use .NET 8, Node.js/npm, and PostgreSQL, or review the Docker Compose workflow.

A sanitized template is provided at `server/API/.env.example`. Copy it to `server/API/.env`, then supply your own database URL, JWT signing key, and SMTP settings. The application loads environment configuration through DotNetEnv.

## Run the source

```sh
dotnet restore server/API/API.csproj
dotnet run --project server/API/API.csproj
```

Use a separate terminal for the client:

```sh
cd client
npm ci
npm start
```

Review client API URLs, database migrations, and the email-template path before starting the full flow. For Compose, set `POSTGRES_PASSWORD` and align the database URL in the API environment file with it:

```sh
docker compose up --build
```

Ports in the supplied Compose configuration are 8080 for the API and 3000 for the browser client. A full deployment and email-delivery run were not validated during this update.

## Credits and source

This is a public portfolio copy of the shared project [UTeam-400/Ja-Notghi](https://github.com/UTeam-400/Ja-Notghi), prepared for Sobhan Alaeddini's account. Original authorship and existing license notices are retained. See [SOURCE.md](SOURCE.md) for the source revision and publication notes.
