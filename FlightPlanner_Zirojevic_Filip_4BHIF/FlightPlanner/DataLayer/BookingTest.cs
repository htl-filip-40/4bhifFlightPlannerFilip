using System;

class BookingTest
{
    static void Main(string[] args)
    {
        // Ersetze diese mit deiner echten Verbindungszeichenfolge
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDb;Initial Catalog=FlightPlanner;Integrated Security=SSPI";

        // Erstelle eine Instanz des BookingService
        BookingService bookingService = new BookingService(connectionString);

        // Beispielwerte für die Buchung
        int flightId = 1; // Angenommene ID eines existierenden Fluges
        int customerId = 1; // Angenommene ID eines existierenden Kunden
        int seats = 2; // Anzahl der zu buchenden Sitze
        int travelClass = 1; // Angenommene Reise-Klasse
        decimal price = 200.00m; // Preis der Buchung

        try
        {
            // Flug buchen
            bookingService.BookFlight(flightId, customerId, seats, travelClass, price);
            Console.WriteLine("Flight booked successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while booking flight: {ex.Message}");
        }

        // Überprüfe die Buchungen des Kunden
        var bookings = bookingService.GetBookingsByCustomerId(customerId);
        Console.WriteLine("Customer Bookings:");
        foreach (var booking in bookings)
        {
            Console.WriteLine(booking.ToString());
        }
    }

    public void TestDeleteCustomer(int customerId)
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDb;Initial Catalog=FlightPlanner;Integrated Security=SSPI";
        FlightRepository flightRepository = new FlightRepository(connectionString);

        try
        {
            Console.WriteLine($"Lösche Customer mit ID {customerId}...");
            flightRepository.DeleteCustomer(customerId);
            Console.WriteLine($"Customer mit ID {customerId} erfolgreich gelöscht.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Löschen des Customers mit ID {customerId}: {ex.Message}");
        }
    }

}
