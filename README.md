# SkyBooker

## Übersicht
SkyBooker ist eine auf Microservices basierende Anwendung für Flugbuchungen. Sie besteht aus drei Hauptdiensten:
- **AuthService**: Verwaltet Benutzerauthentifizierung und -autorisierung.
- **FlightService**: Verwaltet Flugdaten und -operationen.
- **BookingService**: Verwaltet Buchungsoperationen.

## Swagger-Dokumentation
Jeder Dienst enthält Swagger-Dokumentation für eine einfache API-Erkundung. Zugriff auf die Swagger UI unter:
- **AuthService**: `http://localhost:[AUTH_PORT]/swagger`
- **FlightService**: `http://localhost:[FLIGHT_PORT]/swagger`
- **BookingService**: `http://localhost:[BOOKING_PORT]/swagger`

## Benutzerrollen
Die Anwendung unterstützt zwei Benutzerrollen:
- **User**: Standardrolle für reguläre Benutzer. Kann Flüge anzeigen und ihre Buchungen verwalten.
- **Admin**: Erweiterte Rolle mit zusätzlichen Berechtigungen. Kann Flüge erstellen, aktualisieren und löschen.

## Verwendung der Rollen
### Erstellen eines Admin-Benutzers
1. Registrieren Sie einen neuen Benutzer über den AuthService:
   - **Endpoint:** `POST http://localhost:[AUTH_PORT]/api/register`
   - **Headers:** `Content-Type: application/json`
   - **Body:**
     ```json
     {
       "username": "admin",
       "password": "YourSecurePassword123",
       "email": "admin@example.com",
       "role": "Admin"
     }
     ```
2. Melden Sie sich an, um ein JWT-Token zu erhalten:
   - **Endpoint:** `GET http://localhost:[AUTH_PORT]/api/login?username=admin&password=YourSecurePassword123`
   - Dies gibt ein JWT-Token zurück, das die "Admin"-Rolle enthält.

### Verwendung des JWT-Tokens
Fügen Sie das JWT-Token im Authorization-Header für geschützte Endpunkte ein:
- **Headers:** `Authorization: Bearer <your_jwt_token_here>`

### Geschützte Endpunkte
- **FlightService**: Der POST-Endpunkt zum Erstellen von Flügen ist geschützt und erfordert die "Admin"-Rolle.
- **BookingService**: Alle Endpunkte sind geschützt und erfordern ein gültiges JWT-Token.

## Ausführen der Anwendung
1. Stellen Sie sicher, dass Docker und Docker Compose installiert sind.
2. Führen Sie die Anwendung mit Docker Compose aus:
   ```bash
   docker-compose up
   ```
3. Greifen Sie auf die Dienste über ihre jeweiligen Ports zu.

## Weitere Informationen
Weitere Details finden Sie in der individuellen Dienstedokumentation oder in der Swagger UI. 