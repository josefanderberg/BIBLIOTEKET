# Bibliotekssystem 2.0

Ett bibliotekshanteringssystem byggt med .NET 9, Entity Framework Core och Blazor Server.

## Projektstruktur

```
BIBLIOTEK/
├── BIBLIOTEK/              # Konsolapplikation (Del 1)
├── BIBLIOTEK.Core/         # Domänmodeller och interfaces
├── BIBLIOTEK.Data/         # Entity Framework, DbContext, Repository
├── BIBLIOTEK.Web/          # Blazor Server webbgränssnitt
└── BIBLIOTEK.TEST/         # Enhetstester (xUnit)
```

## Köra projektet

### Webbgränssnitt (Blazor)
```bash
cd BIBLIOTEK.Web
dotnet run
```
Öppna sedan `http://localhost:5146` i webbläsaren.

### Konsolapplikation
```bash
cd BIBLIOTEK
dotnet run
```

### Köra tester
```bash
dotnet test
```

## Databasmodell

Systemet använder SQLite med Entity Framework Core. Databasen skapas automatiskt vid första körning.

### Tabeller

| Tabell | Beskrivning |
|--------|-------------|
| **Books** | Böcker med ISBN, titel, författare, utgivningsår och tillgänglighet |
| **Members** | Medlemmar med medlems-ID, namn, e-post och registreringsdatum |
| **Loans** | Lån som kopplar ihop bok och medlem med datum |

### Relationer

```
Book (1) ──── (*) Loan (*) ──── (1) Member
```

- En bok kan ha många lån (lånehistorik)
- En medlem kan ha många lån
- Varje lån refererar till exakt en bok och en medlem

## Blazor-sidor

| Sida | URL | Funktioner |
|------|-----|------------|
| Hem | `/` | Välkomst + statistik (antal böcker, medlemmar, aktiva lån) |
| Böcker | `/books` | Lista, sök, sortering, lägg till bok, ta bort |
| Bokdetaljer | `/books/{id}` | Detaljerad info, lånehistorik, låna/returnera |
| Medlemmar | `/members` | Lista med aktiva lån per medlem, registrera ny medlem, länk till detaljer |
| Medlemsdetaljer | `/members/{id}` | Visa medlemsinfo och fullständig lånehistorik |
| Utlåning | `/loans` | Skapa nytt lån, aktiva lån, markering av försenade |

## Teknologi

- **.NET 9.0**
- **Entity Framework Core 9.0.2** (SQLite)
- **Blazor Server**
- **xUnit** (enhetstester)
- **bUnit** (komponenttester)
- **InMemory Database** (för testning)

## Tester

Projektet innehåller omfattande tester för att säkerställa kvalitet:
- **Repository-operationer**: CRUD-tester för Books och Members.
- **Sökfunktionalitet**: Verifiering av sökning på titel, författare och namn.
- **Databasintegrationer**: Tester för lån och boktillgänglighet.
- **Komponenttester**: bUnit-tester för `BookCard` för att verifiera korrekt rendering av bokstatus.

Kör alla tester med `dotnet test`.

## Arkitektur

Projektet följer **Repository Pattern** för att separera dataåtkomst från affärslogik. Detta gör koden mer testbar och lätt att underhålla. Varje entitet (Book, Member, Loan) har sitt eget repository.
