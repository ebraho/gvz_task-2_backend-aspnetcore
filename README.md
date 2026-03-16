<img src="assets/img/gvz-logo.svg" alt="GVZ logo" width="400px">

# Aufgaben für die Rekrutierung von Senior Software Entwicklerinnen und Entwicklern

## [karriere.gvz.ch](https://karriere.gvz.ch/)

Hallo

Du hast dich auf eine Stelle bei der [Gebäudeversicherung Kanton Zürich GVZ](https://www.gvz.ch/) im Bereich Softwareentwicklung beworben und bist hier gelandet?

Wunderbar, hier bist du genau richtig. Dieses Repository beinhaltet Aufgaben für Kandidatinnen und Kandidaten, die sich im Bewerbungsprozesses befinden.

Bitte bearbeite die Aufgabe(n), zu denen du via persönlichen Kontakt gebeten wurdest. Andere Aufgaben brauchst du nicht zu bearbeiten.

Danke, dass du dir Zeit für die Bearbeitung der Aufgabe(n) nimmst.

### Hilfsmittel

Entwickle so, wie du es im Alltag tust. Insbesondere:

- nutze ein Entwicklungs-Setup deiner Wahl, so dass du dich wohl fühlst
- benutze das Internet, künstliche Intelligenz oder andere Quellen, die dich weiterbringen

### Abgabe

Zur Abgabe der Resultate schlagen wir vor, ein Repository bei [GitHub](https://github.com/) oder [GitLab](https://about.gitlab.com/) zu erstellen.

Bitte achte darauf, dass du vor der Abgabe alle Commits gepusht hast und das Repository öffentlich ist.

### Bewertung

Wir bewerten funktionale Korrektheit, Einfachheit, Lesbarkeit und (soweit möglich) dein Vorgehen.

Viel Erfolg!

## Aufgaben

### Aufgabe 1 - Frontend-Entwicklung mit Angular

Im Ordner `./task-1_frontend-angular` befindet sich Quellcode für eine [Angular](https://angular.dev/) Applikation.

Die Applikation hat den Zweck, das Verhalten von UX Bauteilen zu demonstrieren.

Implementiere folgende Veränderungen und Erweiterungen:

1. Neue Komponente - Eingabe einer Postadresse

- Erstelle eine Komponente im Ordner `src/app/demo`, mit der eine Postadresse eingegeben werden kann.
- Verwende die bestehenden Komponenten in `src/app/parts`: `card`, `number-input`, `select` und `text-input`.
- Validiere Eingaben von Benutzern.

2. Erweiterung der Navigation

- Füge eine weitere Sektion mit Namen "Kombinierte Bauteile" zur Navigationsleiste auf der linken Seite hinzu.
- Füge ein [Route](https://angular.dev/guide/routing/define-routes) hinzu, über welche die in Schritt 1 erstellte Komponente zur Eingabe einer Postadresse angezeigt wird. Zeige die Route in der Sektion "Kombinierte Bauteile" an.

### Aufgabe 2 - Backend-Entwicklung mit ASP.NET Core

Im Ordner `./task-2_backend-aspnetcore` befindet sich Quellcode für ein [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) API.

Das Controller-basierte REST API hat den Zweck, REST Endpunkte für die Erstellung und die Abfrage von Nachrichten zur Verfügung zu stellen.

Als Nachricht wird hier eine strukturierte Kommunikation von Informationen zwischen Kunden und der GVZ bezeichnet.

Die Applikation nutzt [Entity Framework Core](https://learn.microsoft.com/de-de/ef/core/), um mit einer [EF Core In-Memory Datenbank](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/) zu interagieren.

Implementiere folgende Veränderungen und Erweiterungen:

1. Neuer Nachrichten-Typ: Verwaltungsänderung - Klassen, Mapping, Datenbankkontext

- Erstelle Klassen für das Speichern und Übermitteln einer Nachricht des Typs Verwaltungsänderung. Die Nachricht des Typs Verwaltungsänderung soll bei der Übermittlung folgende Informationen enthalten:
  - Vorname, Name und Postadresse einer Privatperson
  - Ein Freitext von maximal 1024 Zeichen
- Validiere die übermittelten Informationen. Alle Informationen sind erforderlich.
- Für Mapping nutze [Mapster](https://github.com/MapsterMapper/Mapster).
- Erweitere den [Entity Framework Core Datenbankkontext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/).

2. Neuer Nachrichten-Typ: Verwaltungsänderung - Endpunkte

- Füge einen Endpunkt für das Erstellen einer Nachricht des Typs Verwaltungsänderung hinzu.
- Erweitere den Endpunkt `GET /messages` für das Abrufen von Nachrichten im `Controller/MessagesController.cs`, so dass auch alle Nachrichten des Typs Verwaltungsänderung abgerufen werden.

3. Neuer Nachrichten-Typ: Verwaltungsänderung - Integrationstests für Endpunkte

- Füge Integrationstests für den hinzugefügten Endpunkt hinzu.
- Passe die bestehenden Integrationstests für den Endpunkt `GET /messages` so an, dass Nachrichten des Typs Verwaltungsänderung inkludiert werden.

### Aufgabe 3 - DevOps

Im Ordner `./task-1_frontend-angular` befindet sich Quellcode für eine [Angular](https://angular.dev/) Applikation.

Im Ordner `./task-2_backend-aspnetcore` befindet sich Quellcode für ein [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) API.

Schreibe eine Continuous Integration Pipeline für eine der beiden Applikationen. Nutze dafür entweder [GitHub Actions](https://github.com/features/actions) oder [GitLab CI/CD](https://docs.gitlab.com/topics/build_your_application/).

Die Pipeline soll folgende Schritte beinhalten:

- Installation von Dependencies
- Statische Code-Analyse
  - [Angular](https://angular.dev/) Applikation: nutze [Angular ESLint](https://github.com/angular-eslint/angular-eslint)
  - [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) API: nutze [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)
- Ausführung von Tests - nur für [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) API
- Erstellung eines Docker Container Image: Schreibe ein Dockerfile mit den Stages Build und Serve:
  - Basis für [Angular](https://angular.dev/) Applikation:
    - Build: [Node.js](https://nodejs.org/en) mit [Tag 24.14-alpine](https://hub.docker.com/_/node)
    - Serve: [Nginx](https://nginx.org/) mit [Tag latest](https://hub.docker.com/_/nginx)
  - Basis für [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) API:
    - Build: .NET SDK mit [Tag 10.0](https://mcr.microsoft.com/en-us/artifact/mar/dotnet/sdk)
    - Serve: ASP.NET Core Runtime mit [Tag 10.0](https://mcr.microsoft.com/en-us/artifact/mar/dotnet/aspnet)

Die Veröffentlichung des erstellten Docker Container Image ist nicht Teil der Aufgabe. Demonstriere eine erfolgreiche Ausführung von `docker build`.
