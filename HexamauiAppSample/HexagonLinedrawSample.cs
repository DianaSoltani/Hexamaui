using HexaMaui;
using Microsoft.Maui;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexamauiAppSample
{
    public class HexagonLinedrawSample : IDrawable
    {
        void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
        {
            throw new NotImplementedException();
        }
    }

    public class HexagonBorder : Border
    {
        /// <summary>
        /// Determines which natural orientation you want the hexagon to be in -- FlatLayout or PointyLayout
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// The size of a hexagon here is defined as the farthest point from the center of the hexagon (touching the corners of the shape)
        /// </summary>
        public Point Size { get; set; }



        public HexagonBorder()
        {
            DefaultBuilder();
        }

        /// <summary>
        /// Copied from documentation on borders - example of setters.
        /// </summary>
        private void DefaultBuilder()
        {
            Orientation orientation = Orientation.PointyLayout;
            HexagonLayout layout = new HexagonLayout(orientation, new Point(50,50), new Point(0,0));
            Hex hex = new Hex(0,0);
            List<Point> points = Hexagon.HexagonCorners(layout, hex);
            PointCollection pointCollection = new PointCollection(points.ToArray());
            
            this.Stroke = Color.FromArgb("#C49B33");
            this.Background = Color.FromArgb("#2B0B98");
            this.StrokeThickness =2;
            this.Padding = new Thickness(50, 50);
            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Center;
            this.StrokeShape = new Polygon
            {
                Points = pointCollection
            };
            this.Content = new Label
            {
                Text = "102",
                TextColor = Colors.Black,
                FontSize = 10,
                FontAttributes = FontAttributes.Bold,
                FontAutoScalingEnabled = true,
            };
            
            
        }
    }
}
