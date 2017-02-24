using System;

namespace RoborallyLogic
{
  public enum Orientation
  {
    Left = 0,
    Up = 1,
    Right = 2,
    Down = 3
  }

  public static class Ex
  {
    public static Orientation Oposite(this Orientation source)
    {
      switch (source)
      {
        case Orientation.Up:
          return Orientation.Down;
        case Orientation.Down:
          return Orientation.Up;
        case Orientation.Left:
          return Orientation.Right;
        case Orientation.Right:
          return Orientation.Left;
      }
      throw new Exception();
    }

    public static Orientation Random(this Orientation source)
    {
      switch (new Random().Next(0, 4))
      {
        case 0:
          return Orientation.Down;
        case 1:
          return Orientation.Up;
        case 2:
          return Orientation.Right;
        case 3:
          return Orientation.Left;
      }
      throw new Exception();
    }
  }
}