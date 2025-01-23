namespace FlightPlanner.DataLayer
{
    public class Airline
    {
        public int Id { get; set; }
        public string RegisteredCompanyName { get; set; }
        public string Country { get; set; }
        public string HeadQuarters { get; set; }

        public Airline(int id, string registeredCompanyName, string country, string headQuarters)
        {
            Id = id;
            RegisteredCompanyName = registeredCompanyName;
            Country = country;
            HeadQuarters = headQuarters;
        }
        public Airline() { }
        public override string ToString()
        {
            return $"Airline ID: {Id}, Company Name: {RegisteredCompanyName}, Country: {Country}, Headquarters: {HeadQuarters}";
        }
    }
}