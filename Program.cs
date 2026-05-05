using System.Runtime.InteropServices;
using AutonomousCarsComm;
using AutonomousCarsComm.Models;

var car1 = new Car("Audi","A1",1600,78520,new Location(10,10));
var car2 = new Car("Audi","A5",2000,6000,new Location(0,4));
var car3 = new Car("Ford","Mustang",2300,189000,new Location(3,0));
var car4 = new Car("Volkswagen","Golf",1900,278520,new Location(8,12));
var cars = new List<Car>
{
    car1,car2,car3,car4
};

car1.SetSpeed(50);
car2.SetSpeed(120);
car3.SetSpeed(130);
car4.SetSpeed(90);

car1.sendMessage(car2, car1.GetCarInfo());
Console.WriteLine("Closest car to car2 is " + car2.GetClosestCar(cars).GetCarInfo());
Console.WriteLine("Distance between car2 and car3 is: " + car2.GetDistanceToCar(car3));

var event1 = new Event("Pothole", new Location(8,10));
var event2 = new Event("Police", new Location(0,5));
AddEventToCars(cars, event1);
AddEventToCars(cars, event2);

Console.WriteLine(cars[0].GetEncounteredEventsString());
cars[0].setCurrentLocation(new Location(20,20));
Console.WriteLine(cars[0].GetEncounteredEventsString());




void AddEventToCars(List<Car> cars, Event e)
{
    foreach(var car in cars)
    {
        if(car.GetDistanceToEvent(e)<=10) car.EncounteredEvents.Add(e);
    }
}