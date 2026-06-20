# DeliveryOrder

Тестовое приложение для создания и просмотра заказов на доставку.

## Стек

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- React + TypeScript
- Docker Compose
- xUnit

## Запуск

Из корня проекта:

```bash
docker compose up --build
```

Запуск тестов из корня проекта:

```bash
dotnet test DeliveryOrder.sln
```
## Просмотр

| Сервис | Хост | Порт | Локальный адрес |
|---|---|---:|---|
| Frontend | `frontend` | `80` | `http://localhost:3000` |
| Backend API | `backend` | `8080` | `http://localhost:5000` |
| PostgreSQL | `postgres` | `5432` | `localhost:15432` |