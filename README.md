# ⚽ Fantasy Football Backend System

> A production-grade Fantasy Premier League clone built with ASP.NET Core 8, Clean Architecture, DDD, and CQRS.

---

## 📖 Table of Contents

- [Project Overview](#project-overview)
- [System Architecture](#system-architecture)
- [Domain Model](#domain-model)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Features & Requirements](#features--requirements)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
- [Design Decisions](#design-decisions)
- [Roadmap](#roadmap)

---

## 🎯 Project Overview

**Fantasy Football** is a backend system that replicates the core experience of Fantasy Premier League (FPL). Managers build their squads, make transfers, compete in leagues, and earn points based on real-world player performances.

The system is designed to handle **millions of users** with a focus on:
- **Performance** — Result Pattern instead of exceptions, efficient queries
- **Scalability** — Clean Architecture, separated concerns
- **Security** — ASP.NET Core Identity + JWT Authentication
- **Real-time** — SignalR Live Match Engine (roadmap)

---

## 🏗️ System Architecture

The project follows **Clean Architecture** with strict dependency rules:

```
Domain ← Application ← Infrastructure ← API
```

| Layer | Responsibility |
|---|---|
| **Domain** | Business entities, rules, value objects, domain events |
| **Application** | Use cases, CQRS handlers, interfaces, validators |
| **Infrastructure** | EF Core, SQL Server, Identity, JWT, repositories |
| **API** | Controllers, middleware, DTOs, Swagger |

### Dependency Rules
- **Domain** → depends on nothing
- **Application** → depends on Domain only
- **Infrastructure** → depends on Application
- **API** → depends on Infrastructure + Application

---

## 📦 Domain Model

### Core Entities

| Entity | Description |
|---|---|
| `Player` | Real-world football player with price, position, and points |
| `Team` | Premier League club |
| `Gameweek` | A round of fixtures (38 per season) |
| `Fixture` | A match between two teams |
| `Manager` | A fantasy game user/manager |
| `FantasyTeam` | A manager's squad of 15 players |
| `FantasyPlayer` | A player in a fantasy team (with captain/bench flags) |
| `Transfer` | A buy/sell action during a gameweek |
| `League` | A competition between managers |
| `LeagueMember` | A manager's membership in a league |
| `GameweekScore` | A manager's points for a specific gameweek |
| `PlayerEvent` | A real-world event (goal, assist, card, etc.) |

### Value Objects

| Value Object | Validation |
|---|---|
| `Price` | Must be between £4.0m and £15.0m |
| `TotalPoints` | Cannot be negative |
| `Email` | Must contain '@', cannot be empty |
| `UserName` | 3–20 characters, alphanumeric + underscore only |

### Business Rules (Domain Constants)

```csharp
SquadSize         = 15
MaxPlayersPerTeam = 3       // Max players from same club
InitialBudget     = £100m
FreeTransfersPerWeek = 1
TransferPenaltyPoints = 4   // Points deducted per extra transfer
MinPlayerPrice    = £4.0m
MaxPlayerPrice    = £15.0m
```

### Enums

**PlayerPosition:** `Goalkeeper`, `Defender`, `Midfielder`, `Forward`

**EventType:** `Goal`, `Assist`, `CleanSheet`, `YellowCard`, `RedCard`, `Saves`, `PenaltyMiss`, `PenaltySave`

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| **Framework** | ASP.NET Core 8 |
| **ORM** | Entity Framework Core 8 |
| **Database** | SQL Server |
| **Authentication** | ASP.NET Core Identity + JWT Bearer |
| **Mediator** | MediatR 14 |
| **Validation** | FluentValidation 12 |
| **Mapping** | Mapster |
| **API Docs** | Swagger / Swashbuckle |
| **Real-time** | SignalR |
| **Caching** | Redis (StackExchange.Redis) |
| **Background Jobs** | Hangfire |
| **Rate Limiting** | ASP.NET Core RateLimiting Middleware |

---

## 🎨 Frontend Design System

The frontend application uses a custom tailored **Dark Mode** aesthetic with exact color specifications to match the premium "Stitch" design requirements. These colors are implemented via HSL CSS Variables configured in `tailwind.config.js`.

| Element | Color Hex | Usage |
|---|---|---|
| **Background** | `#09090B` | Base dark background for the entire application |
| **Surface** | `#18181B` | Elevated elements (Cards, Popovers, Drawers, Sidebar) |
| **Primary** | `#2563EB` | Main call-to-action buttons, active states, active links |
| **Secondary** | `#8B5CF6` | Highlights, vice-captain badges, secondary actions |
| **Accent** | `#22D3EE` | Special badges, premium features (Fantasy Chips) |
| **Success** | `#22C55E` | Positive point gains, successful transfers, up-arrows |
| **Warning** | `#F59E0B` | Caution alerts, warnings about impending deadlines |
| **Danger** | `#EF4444` | Destructive actions, point deductions, red cards |
| **Text** | `#FFFFFF` | Primary readable text |
| **Sub Text** | `#A1A1AA` | Muted text, hints, timestamps |
| **Border** | `#27272A` | Dividers, input borders, faint borders on cards |

---

## 📁 Project Structure

```
FantasyFootball/
├── src/
│   ├── FantasyFootball.Domain/
│   │   ├── Entities/
│   │   │   ├── BaseEntity.cs
│   │   │   ├── Player.cs
│   │   │   ├── Team.cs
│   │   │   ├── Gameweek.cs
│   │   │   ├── Fixture.cs
│   │   │   ├── Manager.cs
│   │   │   ├── FantasyTeam.cs
│   │   │   ├── FantasyPlayer.cs
│   │   │   ├── Transfer.cs
│   │   │   ├── League.cs
│   │   │   ├── LeagueMember.cs
│   │   │   ├── GameweekScore.cs
│   │   │   └── PlayerEvent.cs
│   │   ├── ValueObjects/
│   │   │   ├── Price.cs
│   │   │   ├── TotalPoints.cs
│   │   │   ├── Email.cs
│   │   │   └── UserName.cs
│   │   ├── Enums/
│   │   │   ├── PlayerPosition.cs
│   │   │   └── EventType.cs
│   │   ├── Exceptions/
│   │   │   └── DomainException.cs
│   │   └── Constants/
│   │       └── FantasyRules.cs
│   │
│   ├── FantasyFootball.Application/
│   │   ├── Common/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IRepository.cs
│   │   │   │   ├── IUnitOfWork.cs
│   │   │   │   ├── IPlayerRepository.cs
│   │   │   │   ├── ITeamRepository.cs
│   │   │   │   ├── IManagerRepository.cs
│   │   │   │   ├── IFantasyTeamRepository.cs
│   │   │   │   ├── ILeagueRepository.cs
│   │   │   │   ├── IGameweekRepository.cs
│   │   │   │   ├── IFixtureRepository.cs
│   │   │   │   ├── IIdentityService.cs
│   │   │   │   └── IJwtService.cs
│   │   │   ├── Behaviors/
│   │   │   │   ├── ValidationBehavior.cs
│   │   │   │   └── LoggingBehavior.cs
│   │   │   └── Exceptions/
│   │   │       ├── NotFoundException.cs
│   │   │       └── ValidationException.cs
│   │   ├── DTOs/
│   │   │   ├── PlayerDto.cs
│   │   │   ├── TeamDto.cs
│   │   │   ├── ManagerDto.cs
│   │   │   ├── FantasyTeamDto.cs
│   │   │   ├── FantasyPlayerDto.cs
│   │   │   ├── TransferDto.cs
│   │   │   ├── LeagueDto.cs
│   │   │   ├── LeagueMemberDto.cs
│   │   │   ├── GameweekDto.cs
│   │   │   ├── FixtureDto.cs
│   │   │   ├── GameweekScoreDto.cs
│   │   │   └── PlayerEventDto.cs
│   │   └── UseCases/
│   │       ├── Players/
│   │       ├── Teams/
│   │       ├── Managers/
│   │       ├── FantasyTeams/
│   │       ├── Transfers/
│   │       ├── Leagues/
│   │       └── Gameweeks/
│   │
│   ├── FantasyFootball.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── AppDbContext.cs
│   │   │   └── Configurations/
│   │   │       ├── PlayerConfiguration.cs
│   │   │       ├── ManagerConfiguration.cs
│   │   │       ├── FantasyTeamConfiguration.cs
│   │   │       ├── FixtureConfiguration.cs
│   │   │       ├── TransferConfiguration.cs
│   │   │       ├── LeagueMemberConfiguration.cs
│   │   │       └── ApplicationUserConfiguration.cs
│   │   ├── Repositories/
│   │   │   ├── Repository.cs
│   │   │   ├── UnitOfWork.cs
│   │   │   ├── PlayerRepository.cs
│   │   │   ├── TeamRepository.cs
│   │   │   ├── ManagerRepository.cs
│   │   │   ├── FantasyTeamRepository.cs
│   │   │   ├── LeagueRepository.cs
│   │   │   ├── GameweekRepository.cs
│   │   │   └── FixtureRepository.cs
│   │   ├── Identity/
│   │   │   ├── ApplicationUser.cs
│   │   │   ├── IdentityService.cs
│   │   │   └── JwtService.cs
│   │   └── DependencyInjection/
│   │       └── DependencyInjection.cs
│   │
│   └── FantasyFootball.API/
│       ├── Controllers/
│       │   ├── AuthController.cs
│       │   ├── PlayersController.cs
│       │   ├── TeamsController.cs
│       │   ├── FantasyTeamsController.cs
│       │   ├── TransfersController.cs
│       │   ├── LeaguesController.cs
│       │   └── GameweeksController.cs
│       └── Program.cs
│
└── tests/
    ├── FantasyFootball.Domain.Tests/
    └── FantasyFootball.Application.Tests/
```

---

## ✅ Features & Requirements

### 1. Authentication & Authorization
- [x] Register with email, password, team name
- [x] Login with JWT token
- [x] ASP.NET Core Identity integration
- [x] One-to-one: `ApplicationUser` ↔ `Manager`

### 2. Player Management
- [x] Get all players
- [x] Get player by ID
- [x] Create player (Admin)
- [x] Update player (Admin)
- [x] Delete player (Admin)
- [x] Filter by position, team, price
- [x] Sort by form, points, price
- [x] Search by name

### 3. Fantasy Team
- [x] Create fantasy team (auto on register)
- [x] Get my team
- [x] Select starting 11 from squad
- [x] Set captain and vice-captain
- [x] Change formation
- [x] Budget tracking

### 4. Transfers
- [x] Buy player (deduct from budget)
- [x] Sell player (add to budget)
- [x] Free transfer per gameweek
- [x] Transfer penalty (-4 pts per extra)
- [x] Transfer deadline enforcement
- [x] Max 3 players from same club

### 5. Leagues
- [x] Create public/private league
- [x] Join league via code
- [x] Leave league
- [x] League standings
- [x] Weekly rankings
- [x] Freemium limits (max 2 custom leagues for free users)

### 6. Scoring Engine
- [x] Calculate points per player event
- [x] Apply captain multiplier (x2)
- [x] Apply triple captain chip (x3)
- [x] Bench boost chip
- [x] Wildcard chip

### 7. Points System

| Event | GK | DEF | MID | FWD |
|---|---|---|---|---|
| Goal | 6 | 6 | 5 | 4 |
| Assist | 3 | 3 | 3 | 3 |
| Clean Sheet | 6 | 6 | 1 | 0 |
| Yellow Card | -1 | -1 | -1 | -1 |
| Red Card | -3 | -3 | -3 | -3 |
| Penalty Save | 5 | - | - | - |
| Penalty Miss | -2 | -2 | -2 | -2 |
| Every 3 saves | 1 | - | - | - |
| Playing 60+ min | 2 | 2 | 2 | 2 |

### 8. Gameweek Management
- [x] Active gameweek tracking
- [x] Deadline enforcement
- [x] Gameweek score calculation

### 9. Real-time & Performance
- [x] Live match scores via SignalR
- [x] Live player points updates
- [x] Goal/card notifications
- [x] Rank change notifications
- [x] Background Jobs (Hangfire) for auto-subs
- [x] Global Leaderboard Caching (Redis)
- [x] API Rate Limiting

### 10. Social & Monetization
- [x] Live Chat (Trash Talk) in leagues via SignalR
- [x] Freemium constraints (Premium users only can send Stickers)
- [x] Edit and Delete chat messages in real-time
- [x] Weekly awards

### 11. Advanced Authentication *(Roadmap)*
- [x] Email Confirmation & OTP Verification
- [x] Two-Factor Authentication (2FA)
- [x] Forgot & Reset Password
- [x] Refresh Tokens & Revocation

### 12. AI Features *(Roadmap)*
- [x] Suggested captain pick
- [x] Suggested transfers
- [x] Best XI recommendation
- [x] Risk analysis

---

## 🌐 API Endpoints

### Auth
```
POST /api/auth/register   → Register new manager
POST /api/auth/login      → Login and get JWT token
```

### Players
```
GET    /api/players        → Get all players
GET    /api/players/{id}   → Get player by ID
POST   /api/players        → Create player [Admin]
PUT    /api/players/{id}   → Update player [Admin]
DELETE /api/players/{id}   → Delete player [Admin]
```

### Teams
```
GET /api/teams             → Get all teams
GET /api/teams/{id}        → Get team by ID
```

### Fantasy Team
```
GET  /api/fantasyteams/my        → Get my team
POST /api/fantasyteams           → Create team
PUT  /api/fantasyteams/captain   → Set captain
PUT  /api/fantasyteams/formation → Change formation
```

### Transfers
```
POST   /api/transfers        → Make a transfer
GET    /api/transfers/my     → Get my transfers
DELETE /api/transfers/{id}   → Cancel transfer
```

### Leagues
```
GET  /api/leagues             → Get public leagues
POST /api/leagues             → Create league
POST /api/leagues/join        → Join league by code
GET  /api/leagues/{id}        → Get league standings
DELETE /api/leagues/{id}/leave → Leave league
```

### Gameweeks
```
GET /api/gameweeks            → Get all gameweeks
GET /api/gameweeks/active     → Get current gameweek
GET /api/gameweeks/{id}/fixtures → Get fixtures for gameweek
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (or SQL Server Express)
- Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/FantasyFootball.git
cd FantasyFootball
```

2. **Update connection string** in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=FantasyFootballDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "FantasyFootball",
    "Audience": "FantasyFootballUsers"
  }
}
```

3. **Run migrations**
```bash
cd src/FantasyFootball.API
dotnet ef database update
```

4. **Run the API**
```bash
dotnet run
```

5. **Open Swagger**
```
http://localhost:5000/swagger
```

---

## 🎨 Design Decisions

### Why Clean Architecture?
- Separation of concerns — each layer has one job
- Testability — Domain and Application are fully testable without infrastructure
- Flexibility — swap SQL Server for PostgreSQL by changing Infrastructure only

### Why DDD?
- Business rules live in the Domain, not in controllers or services
- `private set` + factory methods protect entity invariants
- Value Objects (`Price`, `Email`) are self-validating

### Why CQRS + MediatR?
- Reads and writes are separated — easier to optimize each independently
- Pipeline Behaviors add cross-cutting concerns (validation, logging) automatically
- Each use case is a single, focused class

### Why Result Pattern instead of Exceptions?
- Better performance — no stack trace allocation
- Explicit error handling — forces callers to handle failures
- Structured errors with `Code` + `Message` for consistent API responses

### Why ASP.NET Core Identity?
- Security-critical code should not be written from scratch
- Password hashing, lockout, token providers — all battle-tested
- `ApplicationUser` (Auth) ↔ `Manager` (Business) separation keeps concerns clean

---

## 🗺️ Roadmap

### Phase 1 — MVP ✅
- [x] Domain model
- [x] Clean Architecture setup
- [x] Authentication (JWT + Identity)
- [x] Players CRUD
- [x] Database migration

### Phase 2 — Core Game 🔄
- [x] Fantasy team management
- [x] Transfer system
- [x] Scoring engine
- [x] Leagues

### Phase 3 — Real-time & Scale ✅
- [x] SignalR Live Match Engine
- [x] Background Jobs (Hangfire)
- [x] Redis Caching
- [x] API Rate Limiting
- [x] Push notifications

### Phase 4 — Social & Auth 🔄
- [x] Social features (Live Chat, Trash Talk)
- [x] Freemium limits & Premium constraints
- [x] Advanced Auth (2FA, Refresh Tokens, Email Verification)
- [x] Profile Management

### Phase 5 — AI & Deployment 🔜
- [ ] AI captain suggestions
- [ ] Transfer recommendations
- [x] Docker + CI/CD
- [ ] Azure deployment
- [ ] Load testing

---

## 👨‍💻 Author

Built with ❤️ by Ahmed — learning ASP.NET Core Clean Architecture by building something he loves.

---

> *"The best way to learn is to build something you care about."*
