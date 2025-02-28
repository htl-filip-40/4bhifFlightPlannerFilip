using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class Booking
    {
        public int Id { get; set; } // Primary Key
        public int FlightId { get; set; } // Foreign Key
        public int CustomerId { get; set; } // Foreign Key
        public int Seats { get; set; }
        public int TravelClass { get; set; }
        public decimal Price { get; set; }

        public Booking()
        {
        }

        public Booking(int flightId, int customerId, int seats, int travelClass, decimal price)
        {
            FlightId = flightId;
            CustomerId = customerId;
            Seats = seats;
            TravelClass = travelClass;
            Price = price;
        }

        public override string ToString()
        {
            return $"Id: {Id}, FlightId: {FlightId}, CustomerId: {CustomerId}, " +
                   $"Seats: {Seats}, TravelClass: {TravelClass}, Price: {Price}";
        }
    }

}
