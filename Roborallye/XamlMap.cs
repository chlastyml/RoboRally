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
  class MapXaml : IMapDraw
  {
    public MapXaml(Grid grid)
    {
      Grid = grid;
    }

    private Grid Grid { get; set; }

    public void Inicializace(int maxX, int maxY)
    {
      Grid.Children.Clear();

      for (int y = 0; y < maxY; y++)
      {
        for (int x = 0; x < maxX; x++)
        {
          Rectangle newControl = GlobalSetting.Rectangle;
          newControl.Margin = new Thickness(x * GlobalSetting.Size, y * GlobalSetting.Size, 0, 0);
          Grid.Children.Add(newControl);
        }
      }
    }
  }

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
    
    //public override void AddRobot(Robot robot)
    //{
    //  base.AddRobot(robot);
    //  XamlRobot xamlRobot = robot as XamlRobot;
    //  if (xamlRobot != null)
    //  {
    //    xamlRobot.SynchronizacePosition();
    //    Grid.Children.Add(xamlRobot.Template);
    //  }
    //}

    //public override void RemoveRobot(Robot robot)
    //{
    //  base.RemoveRobot(robot);
    //  XamlRobot xamlRobot = robot as XamlRobot;
    //  if (xamlRobot != null)
    //  {
    //    Grid.Children.Remove(xamlRobot.Template);
    //  }
    //}

    //public override void AddWall(Wall wall)
    //{
    //  base.AddWall(wall);
    //  XamlWall xamlWall = wall as XamlWall;
    //  if (xamlWall != null)
    //  {
    //    xamlWall.SynchronizacePosition();
    //    Grid.Children.Add(xamlWall.Template);
    //  }
    //}

    //public override void RemoveWall(Wall wall)
    //{
    //  base.AddWall(wall);
    //  XamlWall xamlWall = wall as XamlWall;
    //  if (xamlWall != null)
    //  {
    //    Grid.Children.Remove(xamlWall.Template);
    //  }
    //}

    //public override void AddEnviromentalMove(Enviroment enviroment)
    //{
    //  base.AddEnviromentalMove(enviroment);
    //  XamlLine xamlWall = enviroment as XamlLine;
    //  if (xamlWall != null)
    //  {
    //    xamlWall.SynchronizacePosition();
    //    Grid.Children.Add(xamlWall.Template);
    //  }
    //}

    //public override void RemoveEnviromentalMove(Enviroment enviroment)
    //{
    //  base.RemoveEnviromentalMove(enviroment);
    //  XamlLine xamlLine = enviroment as XamlLine;
    //  if (xamlLine != null)
    //  {
    //    Grid.Children.Remove(xamlLine.Template);
    //  }
    //}
  }
}
