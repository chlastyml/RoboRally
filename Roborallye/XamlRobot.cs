using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RoborallyLogic;
using Orientation = RoborallyLogic.Orientation;

namespace Roborallye
{
  interface IXamlDraw
  {
    Grid Template { get; set; }
    void SynchronizacePosition();
  }

  class XamlDraw : IDraw, IXamlDraw
  {
    public XamlDraw(Grid template, IPosition positionObject)
    {
      PositionObject = positionObject;
      Template = template;
    }

    public Grid Template { get; set; }
    private IPosition PositionObject { get; set; }
    public void SynchronizacePosition()
    {
      if (Template != null && PositionObject != null)
      {
        ActualizaceMargin();
        ActualizaceOrientation();
      }
    }

    public void Add()
    {
      GlobalSetting.MainGrid.Children.Add(Template);
      SynchronizacePosition();
    }

    public void Remove()
    {
      GlobalSetting.MainGrid.Children.Remove(Template);
    }

    private void ActualizaceMargin()
    {
      Template.Margin = new Thickness(GlobalSetting.Rectangle.Width * PositionObject.Position.Coordinates.X,
        GlobalSetting.Rectangle.Height * PositionObject.Position.Coordinates.Y, 0, 0);
    }

    private void ActualizaceOrientation()
    {
      switch (PositionObject.Position.Orientation)
      {
        case Orientation.Up:
          Template.LayoutTransform = new RotateTransform(-180);
          break;
        case Orientation.Left:
          Template.LayoutTransform = new RotateTransform(90);
          break;
        case Orientation.Down:
          Template.LayoutTransform = new RotateTransform(0);
          break;
        case Orientation.Right:
          Template.LayoutTransform = new RotateTransform(-90);
          break;
      }
    }
  }
}