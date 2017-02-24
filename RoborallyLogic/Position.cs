namespace RoborallyLogic
{
  public class Position
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

    public Position(Orientation orientation, Coordinates coordinates)
    {
      Orientation = orientation;
      Coordinates = coordinates;
    }

    public Orientation Orientation { get; set; }
    public Coordinates Coordinates { get; set; }

    public Position Copy()
    {
      Position result = new Position(Orientation, new Coordinates(Coordinates));
      return result;
    }

    public Coordinates NextCoordinates()
    {
      return Coordinates.GetOneNearby(Orientation);
    }

    public Position NextPosition()
    {
      return new Position(Orientation, Coordinates.GetOneNearby(Orientation));
    }

    public override string ToString()
    {
      return string.Format("{0} - {1}", Orientation, Coordinates);
    }
  }
}