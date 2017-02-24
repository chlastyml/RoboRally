using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using RoborallyLogic;

namespace Roborallye
{
  class XamlMap : Map
  {
    public XamlMap(int x, int y, Grid grid) : base(x, y)
    {
      Grid = grid;

      Size = GlobalSetting.Rectangle.Width;

      Inicialize();
    }

    private double Size { get; set; }

    private void Inicialize()
    {
      for (int y = 0; y < MaxY; y++)
      {
        for (int x = 0; x < MaxX; x++)
        {
          Rectangle newControl = GlobalSetting.Rectangle;
          newControl.Margin = new Thickness(x*GlobalSetting.Size, y*GlobalSetting.Size, 0, 0);
          Grid.Children.Add(newControl);
        }
      }
    }

    private Grid Grid { get; set; }
    
    public override void AddRobot(Robot robot)
    {
      base.AddRobot(robot);
      XamlRobot xamlRobot = robot as XamlRobot;
      if (xamlRobot != null)
      {
        xamlRobot.SynchronizacePosition();
        Grid.Children.Add(xamlRobot.Template);
      }
    }

    public override void RemoveRobot(Robot robot)
    {
      base.RemoveRobot(robot);
      XamlRobot xamlRobot = robot as XamlRobot;
      if (xamlRobot != null)
      {
        Grid.Children.Remove(xamlRobot.Template);
      }
    }

    public override void AddWall(Wall wall)
    {
      base.AddWall(wall);
      XamlWall xamlWall = wall as XamlWall;
      if (xamlWall != null)
      {
        xamlWall.SynchronizacePosition();
        Grid.Children.Add(xamlWall.Template);
      }
    }

    public override void RemoveWall(Wall wall)
    {
      base.AddWall(wall);
      XamlWall xamlWall = wall as XamlWall;
      if (xamlWall != null)
      {
        Grid.Children.Remove(xamlWall.Template);
      }
    }

    public override void AddEnviromentalMove(IEnviromentMove enviromentMove)
    {
      base.AddEnviromentalMove(enviromentMove);
      XamlLine xamlWall = enviromentMove as XamlLine;
      if (xamlWall != null)
      {
        xamlWall.SynchronizacePosition();
        Grid.Children.Add(xamlWall.Template);
      }
    }

    public override void RemoveEnviromentalMove(IEnviromentMove enviromentMove)
    {
      base.RemoveEnviromentalMove(enviromentMove);
      XamlLine xamlLine = enviromentMove as XamlLine;
      if (xamlLine != null)
      {
        Grid.Children.Remove(xamlLine.Template);
      }
    }

    public string GetRobotsStats()
    {
      return string.Format("{0}", string.Join(Environment.NewLine, Robots.Select(r => r.ToString())));
    }
  }
}
