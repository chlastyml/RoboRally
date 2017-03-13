using System;

namespace RoborallyLogic
{
  public class Position : ICloneable
  {
    public Position()
    {
      Orientation = Orientation.Up;
      Coordinates = new Coordinates();
    }

    public Position(Position position)
    {
      Orientation = position.Orientation;
      Coordinates = new Coordinates(Coordinates);
    }

    public Position(Orientation orientation, int x, int y)
    {
      Orientation = orientation;
      Coordinates = new Coordinates(x, y);
    }

    public Position(Coordinates coordinates, Orientation orientation = Orientation.Up)
    {
      Orientation = orientation;
      Coordinates = coordinates;
    }

    public Orientation Orientation { get; set; }
    public Coordinates Coordinates { get; set; }
    
    public Coordinates NextCoordinates()
    {
      return Coordinates.GetOneNearby(Orientation);
    }

    public Position NextPosition()
    {
      return new Position(Coordinates.GetOneNearby(Orientation), Orientation);
    }

    public override string ToString()
    {
      return string.Format("{0,5} - {1}", Orientation, Coordinates);
    }

    public object Clone()
    {
      Position result = new Position(new Coordinates(Coordinates), Orientation);
      return result;
    }
  }
}