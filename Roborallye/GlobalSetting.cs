﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Roborallye
{
  static class GlobalSetting
  {
    public static double Size { get { return _rectangle.Width; } }

    public static KeyHelper KeyHelper { get; set; }

    private static Rectangle _rectangle;

    public static Rectangle Rectangle
    {
      get { return _rectangle.DeepCopy(); }
      set
      {
        _rectangle = value;
      }
    }

    public static Grid RobotTemplate { get; set; }

    public static Grid WallTemplate { get; set; }
    public static Grid TransportBelt { get; set; }
    public static Grid TargetTemplate { get; set; }
    public static Grid MainGrid { get; set; }

    public static int MapSizeX
    {
      get { return 20; }
    }
    public static int MapSizeY
    {
      get { return 20; }
    }
  }
}
