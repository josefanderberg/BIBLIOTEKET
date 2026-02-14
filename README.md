# Bibliotekssystem - Inlämningsuppgift Del 1

Detta är ett konsolbaserat bibliotekssystem utvecklat i C# .NET 9.0.

## Lösning och Designval

Jag har valt att implementera lösningen baserat på **Alternativ B: Komposition**.

### Arkitektur
Systemet är uppbyggt kring en central `Library`-klass som agerar som fasad/koordinator. Istället för att låta denna klass göra allt arbete, delegeras ansvaret till specialiserade "hjälpklasser" (managers):

1.  **BookCatalog:** Ansvarar för bibliotekets bokbestånd. Den hanterar lagring, sökning och sortering av böcker.
2.  **MemberRegistry:** Ansvarar för medlemsregistret. Den hanterar lagring och sökning av medlemmar.
3.  **LoanManager:** Ansvarar för utlåningslogiken. Den sköter kopplingen mellan en bok och en medlem, samt håller koll på förfallodatum.

Detta designmönster (Komposition) gör koden modulär och lättare att underhålla. Varje klass har ett tydligt ansvarsområde (Single Responsibility Principle).

### Klasser
*   `Book` & `Member`: Dataklasser som implementerar `ISearchable`-interfacet för enhetlig sökning.
*   `Loan`: Representerar ett lån med koppling till bok och medlem, samt logik för förfallodatum (`IsOverdue`).
*   `Library`: Huvudklassen som binder ihop allt och erbjuder metoder som `BorrowBook` och `ReturnBook` till användargränssnittet.

## Hur man kör programmet

1.  Öppna terminalen i projektmappen `BIBLIOTEK/BIBLIOTEK`.
2.  Kör kommandot:
    ```bash
    dotnet run
    ```
3.  Följ menyvalen i konsolen för att hantera böcker, medlemmar och lån.

## Tester

Projektet innehåller en uppsättning enhetstester implementerade med **xUnit**. Testerna täcker:
*   Bokhantering och properties.
*   Lånelogik (förfallodatum, återlämning).
*   Sökfunktionalitet (`ISearchable`).
*   Statistik och sortering.

För att köra testerna:
1.  Gå till testprojektets mapp `BIBLIOTEK/BIBLIOTEK.TEST`.
2.  Kör kommandot:
    ```bash
    dotnet test
    ```
