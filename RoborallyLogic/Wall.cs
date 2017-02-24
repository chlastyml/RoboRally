namespace RoborallyLogic
{
  public class Wall
  {
    public ILogicMap Map { get; set; }
    public Coordinates Coordinates { get; set; }
    public Orientation Orientation { get; set; }

    public int X
    {
      get { return Coordinates.X; }
    }
    public int Y
    {
      get { return Coordinates.Y; }
    }

    public Wall(Coordinates coordinates, Orientation orientation)
    {
      Coordinates = coordinates;
      Orientation = orientation;
    }

    public override string ToString()
    {
      return string.Format("{0}:{1}", Coordinates, Orientation);
    }
  }

  //public class Wall
  //{
  //  public ILogicMap Map { get; set; }
  //  public Coordinates Coordinates1 { get; set; }
  //  public Coordinates Coordinates2 { get; set; }

  //  public int X1
  //  {
  //    get { return Coordinates1.X; }
  //  }
  //  public int Y1
  //  {
  //    get { return Coordinates1.Y; }
  //  }
  //  public int X2
  //  {
  //    get { return Coordinates1.X; }
  //  }
  //  public int Y2
  //  {
  //    get { return Coordinates1.Y; }
  //  }

  //  public Wall(Coordinates coordinates1, Coordinates coordinates2)
  //  {
  //    Coordinates1 = coordinates1;
  //    Coordinates2 = coordinates2;
  //  }

  //  public override string ToString()
  //  {
  //    return string.Format("{0}:{1}", Coordinates1, Coordinates2);
  //  }
  //}
}