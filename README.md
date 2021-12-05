# Kanbanboar App

## Specifikáció
A konkrét feladat egy teendőket kezelő webalkalmazás backendjének és frontendjének elkészítése. A teendőket adatbázisban tároljuk és a webes felületen jelenítjük meg, a kiszolgáló pedig REST interfészen keresztül érhető el.
A teendők rendelkeznek címmel, leírással, határidővel és állapottal (függőben, folyamatban, kész, elhalasztva). A határidő helyett a prioritást a teendők sorrendje határozza meg, tehát az előbbi adataik mellett még az egymáshoz képesti sorrendet is tároljuk és megjelenítjük.

## Beüzemelés

A szoftver beüzemeléséhez először a backendet kell felkonfigurálni.
Létre kell hozni egy sql adatbázist. Az ehhez tartozó `ConnectionString-et` be kell illeszteni a backendhez tartozó projekt (kanbanboard-backend) `appsettings.json` fájlban a `KanbanBoardContexthez` :
```
"ConnectionStrings": {
    "KanbanBoardContext": <ConnectionString>
  }
```
Ezután a projekt mappájában (kanbanboard-backend) ki kell adni az alábbi parancsot:
*(A parancs végrehajtásához szükség van a [Entity Framework Core tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)-ra.)*
```
dotnet ef database update
```
A parancs hatására EFCore Migrations segítségevel létrejön az adatbázis séma.
Ez a projekt tartalmazza a `wwwroot` mappában egy frontendhez tartozó buildet. 
A futtatáshoz már csak egy parancs hiányzik:
```
dotnet run
```
A `localhost:5001/index.html`-en elérhatő a kliens alkalmazás, a `localhost:5001/swagger/index.html`-en az api dokumentáció.


Amennyiben szeretnénk külön futtatni a frontendet, az alábbi parancsok kiadása szükséges a frontendhez tartozó mappában:
```
npm install
npm start
```
*Ahhoz, hogy az alkalmazás érdemben használható legyen szükséges, hogy a backend fusson.*

## Backend
Microsoft SQL Server adatbázist használ az alkalmazás.
Az SQL adatbázis code-first megközelítéssel készült. 
A létrejött adatbázis ER-diagramja:
**kép**

A szerver oldal egy ASP.NET core api alkalmazás, ami .NET 5-re van targetelve. Az adatbázis leképzés EntityFramworkCore-al történik.
A kontrollerek végzik a http kommunikációt a kliensekkel, a Dal-ban a CardRepository és a LaneRepository osztályokban valósul meg.
A kliensekkel DTO-kal történik a kommunikáció, így csak a szükséges információk utaznak a hálózaton.
Az alkalmazás a Dependency Injection tervezési mintát követi, így a controllerek a repositorykat dependency injection-el kapják meg, a repository-k szintén DI-vel érik el a KanbanBoardContext-et, hogy az adatbázison műveleteket tudjanak végezni. Az adatbázis query-k Linq segítségvel vannak megvalósítva.

**talán kép**

### Unit testek 
A `Test` alkönyvtárban találhatók a tesztek. A Lane törlése van letesztelve, 4 teszttel (2 a controller válaszát teszteli, 2 a repository-ban való törlésre vonatkozik). A tesztekhez [Moq](https://www.nuget.org/packages/Moq/), [MockQueryable.Moq](https://www.nuget.org/packages/MockQueryable.Moq/) (ez az async query-k tesztelését teszi egyszerűbbé Moq segítségével), [MSTest.TestAdapter](https://www.nuget.org/packages/MSTest.TestAdapter/), [MSTest.TestFrameWork](https://www.nuget.org/packages/MSTest.TestFramework/) nuget package-k szolgáltak segítségül. 

### API dokumentáció
Az alkalmazáshoz tartozik egy [Swashbuckle](https://www.nuget.org/packages/Swashbuckle.AspNetCore/) által automatikusan generált api dokumentáció is, ami a `/swagger/index.html` endpoint-on érhető el.

## Frontend
A kliens alkalmazás React-el készült. 
A kártyák Drag and Drop módszerrel mozgathatóak az oszlopok között, ennek segítségéül a [react-beautiful-dnd](https://github.com/atlassian/react-beautiful-dnd) npm package-t használtam. 
A kinézete [Material UI](https://mui.com/)-al készült.


### Felépítése
Az alkalmazás komponensei az `src/Components` mappában található. Ennek tartalma:
- **App**: felülírja az alapértelmezett témáját az alkalmazásnak. Megjeleníti az alkalmazás fejlécét (Toolbar) 
- **KanbanBoard**: Megjelníti az oszlopokat, az oszlpok mellé megjelenít egy leghosszabb oszlop mértű gombot, amivel új oszlopot lehet megadni, kattintás hatására egy `NewLaneDialog`-ot. Kezelei ha változás történt az alkalmazásban (new card, new lane, ...). Tárolja az oszlopokat és a kártáykat.
- **KanbanLane**: Megjelenít egy oszlopot a kártyákkal. Rendelkezik egy *delete* és *add new card* gombbal. Az utóbbi egy `EditNewCardDiaolg`-ot jelenít meg.
- **TaskCard**: Megjelenít egy kártyát az adataival. Rendelkezik egy *delete* és *edit* gombbal. Az utóbbi egy `EditNewCardDiaolg`-ot jelenít meg.
- **NewLaneDialog**: Megjelenít egy ablakot ahol meg lehet adni az új oszlop adatait.
- **EditNewCardDialog**: Kártya hozzáadáshoz/módosításhoz az adatok megadására szolgál.

A `network` mappában található a `HttpCommunication` osztály, ami a szerverrel való kommunikációért felelős. Ha hibát észlel, akkor jelzi a KanbanBoard komponensnek.
