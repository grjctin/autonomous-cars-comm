namespace AutonomousCarsComm.Models;

public class Event(string name, Location location)
{
    public string Name { get; set; } = name;
    public Location Location { get; set; } = location;
}