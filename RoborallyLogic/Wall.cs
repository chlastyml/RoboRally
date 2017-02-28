namespace RoborallyLogic
{
  public class Wall : LogicObject
  {
    public Coordinates Coordinates { get { return Position.Coordinates; } }
    public Orientation Orientation { get { return Position.Orientation; } }

    protected int X { get { return Position.Coordinates.X; } }
    protected int Y { get { return Position.Coordinates.Y; } }

    public Wall(Position position) : base(position) { }

    public override string ToString()
    {
      return string.Format("{0}", Position);
    }
  }
}