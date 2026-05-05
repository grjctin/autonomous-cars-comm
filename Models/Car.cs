using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;

namespace AutonomousCarsComm.Models;

public class Car
{
    //car info
    private static int _idCounter = 1;
    public int Id { get; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int EngineCapacity { get; set; }
    public int Milleage { get; set; }
    //current speed
    public int CurrentSpeed { get; set; }
    //current location
    public Location CurrentLocation { get; set; }
    //encountered events
    public List<Event> EncounteredEvents { get; set; } = new();
    public List<Message> ReceivedMessages { get; set; } = new();
    

    public Car(string brand, string model, int engineCapacity, int milleage, Location location)
    {
        Id = _idCounter++;
        Brand=brand;
        Model=model;
        EngineCapacity=engineCapacity;
        Milleage=milleage;
        CurrentSpeed = 0;
        CurrentLocation=location;
    }

    //receive info from other cars
    public void receiveMessage(Message message)
    {
        Console.WriteLine("This is car " + Id + ", I received the following message from car " + message.SenderId + ":\n" + message.Content);
        ReceivedMessages.Add(message);
    }
    public List<Message> getMessages()
    {
        return ReceivedMessages;
    }

    //send info to another car
    public void sendMessage(Car otherCar, string content)
    {
        var message = new Message
        {
            SenderId = Id,
            ReceiverId = otherCar.Id,
            Content = content
        };
        otherCar.receiveMessage(message);
    }

    //set the current speed
    public void SetSpeed(int newSpeed)
    {
        CurrentSpeed = newSpeed;
    }

    //set current location
    public void setCurrentLocation(Location newLocation)
    {
        Console.WriteLine("Car " + Id + " is changing location to (" + newLocation.X + "," + newLocation.Y +")");
        CurrentLocation = newLocation;
        //check and update events list
        updateEncounteredEvents();
    }

    //calculate distance to another car
    public double GetDistanceToCar(Car otherCar)
    {
        return CurrentLocation.GetDistanceTo(otherCar.CurrentLocation);
    }

    //caclulate distance to event
    public double GetDistanceToEvent(Event e)
    {
        return CurrentLocation.GetDistanceTo(e.Location);
    }

    //pick closest car
    public Car? GetClosestCar(List<Car> cars)
    {
        if (cars.Count > 1)
        {
            return cars.OrderBy(c=>GetDistanceToCar(c)).Where(c=> c.Id != this.Id).FirstOrDefault();
        }
        else return this;
    }

    //get main info as string
    public string GetCarInfo()
    {
        return "Car Id: " + Id + 
        ", Brand: " + Brand + 
        ", Model: " + Model + 
        ", Engine capacity: " + EngineCapacity + 
        ", Milleage: " + Milleage + 
        ", Current traveling speed: " + CurrentSpeed +
        ", Current location coordinates (x,y): (" + CurrentLocation.X + "," + CurrentLocation.Y + ")";
    }

    public string GetEncounteredEventsString()
    {
        if (EncounteredEvents.Count == 0)
            return "No events nearby";
        
        string eventsString = "";
        foreach(var e in EncounteredEvents)
        {
            eventsString += "\n" + e.Name + " at (" + e.Location.X + "," + e.Location.Y + ")";
        }
        return eventsString;
    }
    
    public void AddEvent(Event e)
    {
        EncounteredEvents.Add(e);
    }

    //check and update events list after location change
    private void updateEncounteredEvents()
    {
        //location changed, removing events further than 10 units
        EncounteredEvents.RemoveAll(e => this.GetDistanceToEvent(e)>10);
    }
}