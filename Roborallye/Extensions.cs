using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Roborallye
{
  internal static class Extensions
  {
    public static Grid DeepCopy(this Grid sourceGrid, bool random = false)
    {
      Grid resultGrid = new Grid();

      resultGrid.VerticalAlignment = sourceGrid.VerticalAlignment;
      resultGrid.HorizontalAlignment = sourceGrid.HorizontalAlignment;
      resultGrid.Margin = new Thickness(sourceGrid.Margin.Left, sourceGrid.Margin.Top, sourceGrid.Margin.Right, sourceGrid.Margin.Bottom);

      foreach (object child in sourceGrid.Children)
      {
        Rectangle rec = child as Rectangle;
        if (rec != null)
        {
          resultGrid.Children.Add(rec.DeepCopy(random));
        }
      }

      return resultGrid;
    }

    public static Rectangle DeepCopy(this Rectangle source, bool random = false)
    {
      Rectangle rectangle = new Rectangle();
      rectangle.Stroke = source.Stroke;
      rectangle.StrokeThickness = source.StrokeThickness;
      rectangle.Width = source.Width;
      rectangle.Height = source.Height;
      rectangle.Margin = new Thickness(source.Margin.Left, source.Margin.Top, source.Margin.Right, source.Margin.Bottom);
      rectangle.VerticalAlignment = source.VerticalAlignment;
      rectangle.HorizontalAlignment = source.HorizontalAlignment;
      rectangle.Fill = random ? PickBrush() : source.Fill;
      
      rectangle.LayoutTransform = source.LayoutTransform;

      return rectangle;
    }

    private static Brush PickBrush()
    {
      Brush result = Brushes.Transparent;

      Random rnd = new Random();

      Type brushesType = typeof(Brushes);

      PropertyInfo[] properties = brushesType.GetProperties();

      int random = rnd.Next(properties.Length);
      result = (Brush)properties[random].GetValue(null, null);

      return result;
    }
  }
}
