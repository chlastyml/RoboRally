namespace RoborallyLogic
{
  public class Robot : LogicObject, IFireAble, IMoveAble
  {
    public Robot(int damagedCount, int lifeTokenCount, string name, Position position, bool player = false) : base(position)
    {
      DamagedCount = damagedCount;
      LifeTokenCount = lifeTokenCount;
      Name = name;
      Player = player;
      StartPosition = (Position) Position.Clone();
      Weapon = new Ultimate();
    }

    public bool Player { get; set; }
    public bool IsMovedEnvironment { get; set; }
    public string Name { get; private set; }
    public Position StartPosition { get; set; }
    public int LifeTokenCount { get; private set; }
    public int DamagedCount { get; private set; }

    private bool IsAlive
    {
      get { return DamagedCount > 0; }
    }

    public IWeapon Weapon { get; set; }

    public override string ToString()
    {
      return string.Format("{0,15}: {1,15} - {2,3} Ž {3,3} dmg", Name, Position, LifeTokenCount, DamagedCount);
    }

    #region Moves and orientation

    #region NONInstruction

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

    public virtual bool Move(Orientation orientation, int distance)
    {
      for (int i = 0; i < distance; i++)
      {
        if (!Move(orientation))
        {
          return false;
        }
      }
      return true;
    }

    public virtual bool Move(Orientation orientation, bool isEnviromental = false)
    {
      Map.UpdateRobots();

      Coordinates newCoordinates = Position.Coordinates.GetOneNearby(orientation);

      if (Map.MovingRobot(newCoordinates, orientation, isEnviromental))
      {
        Position.Coordinates = newCoordinates;
        SynchronizacePosition();
        IsMovedEnvironment = isEnviromental;
        return true;
      }
      return false;
    }
    
    #endregion

    public void MoveForward(int distance = 1)
    {
      Move(Position.Orientation, distance);
    }

    public void MoveBack(int distance = 1)
    {
      Move(Position.Orientation.Oposite(), distance);
    }

    public virtual void TurnLeft()
    {
      if (Position.Orientation == 0)
        Position.Orientation = (Orientation) 3;
      else
        Position.Orientation--;
      SynchronizacePosition();
    }

    public virtual void TurnRight()
    {
      if (Position.Orientation == (Orientation) 3)
        Position.Orientation = 0;
      else
        Position.Orientation++;
      SynchronizacePosition();
    }

    public virtual void TurnBack()
    {
      Position.Orientation = Position.Orientation.Oposite();
      SynchronizacePosition();
    }

    #endregion

    #region Fire and damage

    public void Fire()
    {
      if (Weapon != null)
      {
        Weapon.Fire(Map, this);
      }
    }

    public void TakeDamage()
    {
      TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
      DamagedCount = DamagedCount - damage;
      if (!IsAlive)
      {
        TryRestart();
      }
    }

    #endregion

    #region Restart

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
        Map.Remove(this);
      }
    }

    private void RestartPosition()
    {
      if (!Map.IsSomeRobotOnPosition(StartPosition.Coordinates))
      {
        Position = (Position)StartPosition.Clone();
        SynchronizacePosition();
      }

      while (true)
      {
        Coordinates potencionalCoordinates = StartPosition.Coordinates.GetOneNearby(Helper.RandomOrientation);

        if (!Map.IsSomeRobotOnPosition(potencionalCoordinates))
        {
          Position = new Position(potencionalCoordinates, StartPosition.Orientation);
          SynchronizacePosition();
          return;
        }
      }
    }

    #endregion
  }
}