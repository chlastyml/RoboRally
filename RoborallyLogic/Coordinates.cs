using System;

namespace RoborallyLogic
{
  public class Coordinates
  {
    public Coordinates(Coordinates coordinates)
    {
      X = coordinates.X;
      Y = coordinates.Y;
    }

    public Coordinates()
    {
      X = 0;
      Y = 0;
    }

    public Coordinates(int x, int y)
    {
      X = x;
      Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public Coordinates GetOneNearby(Orientation orientation)
    {
      switch (orientation)
      {
        case Orientation.Up:
          return new Coordinates(X, Y - 1);
        case Orientation.Left:
          return new Coordinates(X - 1, Y);
        case Orientation.Down:
          return new Coordinates(X, Y + 1);
        case Orientation.Right:
          return new Coordinates(X + 1, Y);
      }
      //TODO
      throw new Exception();
    }

    public Orientation GetOrientation(Coordinates coordinates)
    {
      int x = coordinates.X - X;
      if (x == 1)
        return Orientation.Right;
      if (x == -1)
        return Orientation.Left;

      int y = coordinates.Y - Y;
      if (y == 1)
        return Orientation.Down;
      if (y == -1)
        return Orientation.Up;

      throw new Exception();
    }

    public override bool Equals(object obj)
    {
      Coordinates coordinates = obj as Coordinates;
      if (coordinates == null) return base.Equals(obj);

      return coordinates.X == X && coordinates.Y == Y;
    }

    public override string ToString()
    {
      return string.Format("[{0},{1}]", X, Y);
    }
  }
}