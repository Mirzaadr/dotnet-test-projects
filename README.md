# 🧾 Order Processing System (Event-Driven with RabbitMQ)

A simple **.NET Core + Clean Architecture** project designed to learn **message queue (MQ)** concepts using **RabbitMQ** and **Docker**.

This project simulates a real-world **order processing workflow** where different services communicate asynchronously using events.

---

## 🚀 Features

* Clean Architecture (Domain, Application, Infrastructure, API)
* RabbitMQ integration (Dockerized)
* Event-driven workflow
* Background workers for async processing
* EF Core (SQLite) for persistence

---

## 🏗️ Architecture Overview

```
Order API → RabbitMQ → Payment Worker → (next steps...)
```

Each component is loosely coupled and communicates through events.

---

## 📦 Project Structure

```
OrderSystem/
├── src/
│   └── App
│       ├── OrderService.API
│       ├── OrderService.Application
│       ├── OrderService.Domain
│       └── OrderService.Infrastructure
│   └── Workers
│       ├── PaymentWorker
|       └── InventoryWorker
├── docker/
│   └── docker-compose.yml
```

---

## 🧱 Tech Stack

* .NET Core
* Entity Framework Core (SQLite)
* RabbitMQ
* Docker

---

## 🐳 Setup RabbitMQ

```bash
cd docker
docker compose up -d
```

RabbitMQ Dashboard:

```
http://localhost:15672
username: guest
password: guest
```

---

## ▶️ Running the Application

### 1. Run API

```bash
dotnet run --project OrderService.API
```

### 2. Run Workers

```bash
dotnet run --project PaymentWorker
dotnet run --project InventoryWorker
```

---

## 🧪 Test the Flow

Send request:

```
POST /api/orders
```

Expected flow:

1. Order created
2. Event published to RabbitMQ
3. Payment worker processes event
4. (Next steps handled by other workers)

---

## 🎯 Learning Goals

This project helps you understand:

* Message Queues (MQ)
* Event-driven architecture
* Clean Architecture in .NET
* Asynchronous processing
* Service decoupling

---

## 📌 Future Improvements

* Event chaining (Payment → Inventory → Notification)
* Retry & Dead-letter queues
* Outbox pattern
* Idempotency handling
* Logging & monitoring

---

## 📖 Notes

This project is intentionally simple and educational. It is designed to demonstrate core concepts before introducing production-level complexity.

---

## 👨‍💻 Author

Built as a learning project for mastering distributed systems and messaging in .NET.
