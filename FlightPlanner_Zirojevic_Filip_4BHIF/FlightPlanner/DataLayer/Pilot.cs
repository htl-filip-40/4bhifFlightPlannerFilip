using System;

public class Pilot
{
    public int Id { set; get; }
    public string Lastname { set; get; }
    public DateTime Birthday { set; get; }
    public string Qualification { set; get; }
    public int FlightHours { set; get; }
    public DateTime FirstDate { set; get; }
    public int AirlineID { set; get; }
    public string Destination { get; }
    public string Duration { get; }
    public object DepartureDate { get; }
    public string PlaneId { get; }
    public string Departure { get; }

    // Parameterloser Konstruktor
    public Pilot() { }

    // Konstruktor mit Parametern
    public Pilot(int id, string lastname, DateTime birthday, string qualification, int flightHours, DateTime firstDate, int airlineID)
    {
        Id = id;
        Lastname = lastname;
        Birthday = birthday;
        Qualification = qualification;
        FlightHours = flightHours;
        FirstDate = firstDate;
        AirlineID = airlineID;
    }
}