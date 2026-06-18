BidBoard 

An auction platform built with ASP.NET Core MVC (.NET 8).  
Users can list items, place bids, and track active lots by category.

 Status: In active development — models and DB schema done, controllers and views in progress.

Tech Stack
- ASP.NET Core MVC (.NET 8) + Entity Framework Core
- ASP.NET Core Identity (roles: Seller, Bidder, Admin)
- SignalR (real-time bid updates)
- MS SQL Server · Bootstrap 5

Key Features
- Lot lifecycle management (Draft → Active → Ended)
- Bid placement with minimum step validation
- Real-time updates via SignalR
- Background service for automatic auction closing

Author
**Hanna Kramska** — Junior .NET Developer · [GitHub](https://github.com/HannaKramska)
