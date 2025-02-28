using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.DataLayer;

namespace FlightPlanner.BusinessLogicLayer
{
    public class BookingService
    {
        public FlightRepository flightRepository;

        public BookingService(string connectionString)
        {
            flightRepository = new FlightRepository(connectionString);
        }

        public void BookFlight(int flightId, int customerId, int seats, int travelClass)
        {
            // Überprüfen, ob Kunde existiert
            // könnte Methode in FlightRepository sein, um die Kunden-ID zu überprüfen
            if (!CustomerExists(customerId))
            {
                throw new InvalidOperationException("Der angegebene Kunde existiert nicht.");
            }

            // Buchung erstellen
            flightRepository.CreateBooking(flightId, customerId, seats, travelClass);
        }

        private bool CustomerExists(int customerId)
        {
            // Logik hinzufügen, um zu überprüfen, ob der Kunde existiert
            // könnte eine Methode in der CustomerDataMapper-Klasse sein
            // Für das Beispiel davon ausgehen, dass diese Methode existiert
            var customer = new CustomerDataMapper(connectionString).Read(customerId);
            return customer != null;
        }

        private int CheckAvailableSeats(int flightId)
        {
            // Logik hinzufügen, um die verfügbaren Sitze für Flug zu überprüfen
            // Methode sollte in FlightDataMapper implementiert sein
            return flightRepository.GetAvailableSeats(flightId); // Beispielaufruf
        }

        private decimal CalculatePrice(int flightId, int travelClass, int seats)
        {
            // Logik hinzufügen, um Preis zu berechnen
            // könnte Methode in der FlightDataMapper-Klasse sein
            return flightRepository.GetPrice(flightId, travelClass) * seats; // Beispielaufruf
        }
        class BookingService
        {
            private FlightRepository flightRepository;

            public BookingService(string connectionString)
            {
                flightRepository = new FlightRepository(connectionString);
            }

            public void BookFlight(int flightId, int customerId, int seats, int travelClass, decimal price)
            {
                // Hier könnten zusätzliche Validierungen stattfinden (z.B. Überprüfung der Verfügbarkeit)

                // Flug und Buchung erstellen
                Flight flight = flightRepository.GetFlight(flightId);
                if (flight == null)
                {
                    throw new InvalidOperationException("Flight not found.");
                }

                Booking booking = new Booking
                {
                    FlightId = flight.Id,
                    CustomerId = customerId,
                    Seats = seats,
                    TravelClass = travelClass,
                    Price = price
                };

                flightRepository.CreateBooking(booking);
            }

            public List<Booking> GetBookingsByCustomerId(int customerId)
            {
                return flightRepository.GetBookingsByCustomerId(customerId);
            }
        }


    }
