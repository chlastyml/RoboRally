namespace RoborallyLogic
{
  public class Robot : IFireAble, IMoveAble
  {
    public bool Player { get; set; }

    public Robot(string name, Position position, bool player = false)
    {
      Player = player;
      Name = name;
      Position = position;
      LifeTokenCount = 0;
      DamagedCount = 9;
      StartPosition = position.Copy();
    }

    public bool IsMovedEnvironment { get; set; }

    public int X
    {
      get { return Position.Coordinates.X; }
    }
    public int Y
    {
      get { return Position.Coordinates.Y; }
    }
    public Orientation Orientation
    {
      get { return Position.Orientation; }
    }

    public string Name { get; set; }
    public Position Position { get; set; }
    public Position StartPosition { get; set; }
    public ILogicMap Map { get; set; }

    public int LifeTokenCount { get; set; }
    public int DamagedCount { get; set; }

    public void MoveUp()
    {
      Orientation backup = Position.Orientation;
      Position.Orientation = Orientation.Up;
      MoveForward();
      Position.Orientation = backup;
    }

    public void MoveLeft()
    {
      Orientation backup = Position.Orientation;
      Position.Orientation = Orientation.Left;
      MoveForward();
      Position.Orientation = backup;
    }

    public void MoveDown()
    {
      Orientation backup = Position.Orientation;
      Position.Orientation = Orientation.Down;
      MoveForward();
      Position.Orientation = backup;
    }

    public void MoveRight()
    {
      Orientation backup = Position.Orientation;
      Position.Orientation = Orientation.Right;
      MoveForward();
      Position.Orientation = backup;
    }

    public void MoveForward()
    {
      Move(Position.Orientation);
    }

    public virtual void TurnLeft()
    {
      if (Position.Orientation == 0)
        Position.Orientation = (Orientation)3;
      else
        Position.Orientation--;
    }

    public virtual void TurnRight()
    {
      if (Position.Orientation == (Orientation)3)
        Position.Orientation = 0;
      else
        Position.Orientation++;
    }

    public virtual bool Move(Orientation orientation, bool ignoreEnvironment = false)
    {
      Coordinates newCoordinates = Position.Coordinates.GetOneNearby(orientation);

      if (Position.Coordinates.Equals(newCoordinates))
        return false;


      if (Map.MovingRobot(newCoordinates, orientation, ignoreEnvironment))
      {
        Position.Coordinates = newCoordinates;
        return true;
      }
      return false;
    }

    public void MoveBack()
    {
      TurnLeft();
      TurnLeft();
      MoveForward();
      TurnLeft();
      TurnLeft();
    }

    public IWeapon Weapon { get; set; }

    public void Fire()
    {
      if (Weapon != null)
        Weapon.Fire(Map, this);
    }

    public void TakeDamage()
    {
      DamagedCount--;
      if (!IsAlive)
      {
        TryRestart();
      }
    }

    private void TryRestart()
    {
      if (LifeTokenCount > 0)
      {
        LifeTokenCount--;
        DamagedCount = 9;
        RestartPosition();
      }
      else
      {
        Map.RemoveRobot(this);
      }
    }

    private void RestartPosition()
    {
      Position = StartPosition.Copy();
    }

    public void TakeDamage(int damage)
    {
      DamagedCount = DamagedCount - damage;
      if (!IsAlive)
      {
        TryRestart();
      }
    }

    public bool IsAlive
    {
      get { return DamagedCount > 0; }
    }

    public override string ToString()
    {
      return string.Format("{0}: {1} - {2}Ž {3} dmg", Name, Position, LifeTokenCount, DamagedCount);
    }
  }
}