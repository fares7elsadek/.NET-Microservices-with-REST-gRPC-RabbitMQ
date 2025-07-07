# 🧱 Platform-Command Microservices with .NET 9, Kubernetes & RabbitMQ

This is a demo microservices project built using **ASP.NET Core (.NET 9)** that demonstrates a full microservices architecture using both **synchronous (HTTP/gRPC)** and **asynchronous (RabbitMQ)** communication, along with **Docker** and **Kubernetes** orchestration.

## 🚀 Features

### ✅ Microservices Architecture
- **PlatformService** – exposes platform data (e.g. .NET, Java)
- **CommandService** – manages commands associated with platforms

### 🗣️ Communication Methods
- **HTTP REST** – for synchronous service-to-service calls
- **gRPC** – for fast, contract-based communication
- **RabbitMQ** – for asynchronous event-driven messaging

### ⚙️ Technologies Used
- **.NET 9 (ASP.NET Core Web API)**
- **Entity Framework Core (In-Memory & SQL Server)**
- **gRPC**
- **RabbitMQ** (via MassTransit or raw client)
- **AutoMapper**
- **Docker & Docker Compose**
- **Kubernetes (K8s)** – for service orchestration
- **Kubernetes Ingress (NGINX)**
- **Helm** (optional – if you used it)
- **Swagger / OpenAPI** – for API documentation


## ☸️ Running with Kubernetes

> Make sure Docker Desktop or Minikube with Kubernetes is running.

```bash
# Apply all Kubernetes manifests
kubectl apply -f *.yaml
```

To access services, use port forwarding or set up NGINX ingress.

---

## 📡 Message Flow Overview

```
Client → PlatformService (REST API)
    ↓ (HTTP)
CommandService ← PlatformService
    ↓ (RabbitMQ Event)
CommandService updates DB
```

---

## 📘 API Documentation

Each service exposes Swagger UI:

* **PlatformService**: `/swagger`
* **CommandService**: `/swagger`

---

## 🧪 Future Enhancements

* [ ] Add authentication (JWT)
* [ ] Implement centralized logging with ELK or Seq
* [ ] Add retry policy & circuit breakers (Polly)
* [ ] Deploy to cloud (Azure / AWS)


