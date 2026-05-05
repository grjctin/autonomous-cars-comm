namespace AutonomousCarsComm.Models;

public class Location(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public double GetDistanceTo(Location otherLocation)
    {
        return Math.Sqrt(Math.Pow(X - otherLocation.X, 2) + Math.Pow(Y - otherLocation.Y, 2));
        
    }
}