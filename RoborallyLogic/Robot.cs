namespace RoborallyLogic
{
  public class Robot : LogicObject, IFireAble, IMoveAble
  {
    public bool Player { get; set; }

    public Robot(string name, Position position, bool player = false) : this(9, 3, name, position, player)
    {
      Player = player;
      Name = name;
      Position = position;
      LifeTokenCount = 1;
      DamagedCount = 9;
      StartPosition = position.Copy();
    }

    public Robot(int damagedCount, int lifeTokenCount, string name, Position position, bool player = false) : base(position)
    {
      DamagedCount = damagedCount;
      LifeTokenCount = lifeTokenCount;
      Name = name;
      Player = player;
      StartPosition = Position.Copy();
    }

    public bool IsMovedEnvironment { get; set; }
    
    public string Name { get; set; }
    public Position StartPosition { get; set; }

    public int LifeTokenCount { get; set; }
    public int DamagedCount { get; set; }

    public IWeapon Weapon { get; set; }

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

    public virtual bool Move(Orientation orientation, int distance, bool ignoreEnvironment = false)
    {
      for (int i = 0; i < distance; i++)
      {
        if (!Move(orientation, ignoreEnvironment))
        {
          return false;
        }
      }
      return true;
    }
    
    public virtual bool Move(Orientation orientation, bool ignoreEnvironment = false)
    {
      Coordinates newCoordinates = Position.Coordinates.GetOneNearby(orientation);
      //if (Position.Coordinates.Equals(newCoordinates))
      //{
      //  return false;
      //}

      if (Map.MovingRobot(newCoordinates, orientation, ignoreEnvironment))
      {
        Position.Coordinates = newCoordinates;
        SynchronizacePosition();
        return true;
      }
      return false;
    }
    #endregion
    
    public void MoveForward(int distance = 1)
    {
      Move(Position.Orientation, distance);
      //SynchronizacePosition();
    }

    public void MoveBack(int distance = 1)
    {
      Move(Position.Orientation.Oposite(), distance);
      //SynchronizacePosition();
    }

    public virtual void TurnLeft()
    {
      if (Position.Orientation == 0)
        Position.Orientation = (Orientation)3;
      else
        Position.Orientation--;
      SynchronizacePosition();
    }

    public virtual void TurnRight()
    {
      if (Position.Orientation == (Orientation)3)
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
      Position = StartPosition.Copy();
      SynchronizacePosition();
    }

    #endregion

    private bool IsAlive
    {
      get { return DamagedCount > 0; }
    }

    public override string ToString()
    {
      return string.Format("{0}: {1} - {2}Ž {3} dmg", Name, Position, LifeTokenCount, DamagedCount);
    }
  }
}