# ShopStudyingAPI

**RU** | [EN](#english)

---

## 🇷🇺 Русский

### О проекте

REST API интернет-магазина на ASP.NET Core 9. Учебный pet-проект с полноценной слоистой архитектурой, JWT-авторизацией, ролевой моделью и CRUD-операциями для продавцов и покупателей.

### Стек технологий

- **ASP.NET Core 9** — веб-фреймворк
- **Entity Framework Core** — ORM
- **PostgreSQL** — база данных
- **JWT** — аутентификация и авторизация (токен в cookie)
- **BCrypt** — хэширование паролей
- **Swagger / OpenAPI** — документация API

### Архитектура

Проект разделён на слои:

```
ShopDomain        — доменные модели, интерфейсы, DTO
ShopApplication   — бизнес-логика (сервисы)
ShopPersistance   — репозитории, DbContext, миграции
ShopStudyingAPI   — контроллеры, точка входа
```

### Функционал

**Пользователи**
- Регистрация и авторизация через JWT (токен в cookie)
- Хэширование паролей через BCrypt
- Ролевая модель: Admin / Buyer / Seller
- Смена роли, удаление пользователя

**Продавцы**
- Стать продавцом (создание магазина с названием и описанием)
- Добавление, редактирование, удаление товаров
- Просмотр своих товаров

**Корзина**
- Добавление товара в корзину
- Просмотр корзины
- Удаление товара из корзины
- Очистка корзины

### Запуск проекта

**Требования:**
- .NET 9 SDK
- PostgreSQL

**1. Клонировать репозиторий**
```bash
git clone https://github.com/SeikaH778/ShopStudyingAPIProject.git
cd ShopStudyingAPIProject
```

**2. Настроить подключение к БД**

Создать `appsettings.json` на основе `appsettings.example.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=shopdb;Username=your_user;Password=your_password"
  },
  "JWTOptions": {
    "SecretKey": "your_secret_key_min_32_characters",
    "ExpiresHours": 24
  }
}
```

**3. Применить миграции**
```bash
dotnet ef database update -s ShopStudyingAPI -p ShopPersistance
```

**4. Запустить**
```bash
cd ShopStudyingAPI
dotnet run
```

**5. Открыть Swagger**
```
http://localhost:5296/swagger
```

---

## <a name="english"></a>🇬🇧 English

### About

A REST API for an online shop built with ASP.NET Core 9. A pet project featuring a clean layered architecture, JWT authentication, role-based access control, and full CRUD operations for sellers and buyers.

### Tech Stack

- **ASP.NET Core 9** — web framework
- **Entity Framework Core** — ORM
- **PostgreSQL** — database
- **JWT** — authentication and authorization (token stored in cookie)
- **BCrypt** — password hashing
- **Swagger / OpenAPI** — API documentation

### Architecture

The project is split into layers:

```
ShopDomain        — domain models, interfaces, DTOs
ShopApplication   — business logic (services)
ShopPersistance   — repositories, DbContext, migrations
ShopStudyingAPI   — controllers, entry point
```

### Features

**Users**
- Registration and login via JWT (cookie-based token)
- Password hashing with BCrypt
- Role-based model: Admin / Buyer / Seller
- Role management, user deletion

**Sellers**
- Become a seller (create a store with name and description)
- Add, update, delete products
- View own products

**Cart**
- Add product to cart
- View cart
- Remove product from cart
- Clear cart

### Getting Started

**Requirements:**
- .NET 9 SDK
- PostgreSQL

**1. Clone the repository**
```bash
git clone https://github.com/SeikaH778/ShopStudyingAPIProject.git
cd ShopStudyingAPIProject
```

**2. Configure database connection**

Create `appsettings.json` based on `appsettings.example.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=shopdb;Username=your_user;Password=your_password"
  },
  "JWTOptions": {
    "SecretKey": "your_secret_key_min_32_characters",
    "ExpiresHours": 24
  }
}
```

**3. Apply migrations**
```bash
dotnet ef database update -s ShopStudyingAPI -p ShopPersistance
```

**4. Run**
```bash
cd ShopStudyingAPI
dotnet run
```

**5. Open Swagger**
```
http://localhost:5296/swagger
```

