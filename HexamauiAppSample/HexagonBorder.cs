using HexaMaui;
using Microsoft.Maui.Controls.Shapes;
using AbsoluteLayout = Microsoft.Maui.Controls.AbsoluteLayout;

namespace HexamauiAppSample
{
    /// <summary>
    /// TODO: creates a single hexagon through a border and filles in a color. 
    /// You can set content to show text (like hexBorder.Content = new Label {...})
    /// </summary>
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

        /// <summary>
        /// The x,y coordinates to the axial q=0,r=0 hex on the hexagonal grid.
        /// </summary>
        public Point Origin { get; set; }

        /// <summary>
        /// The targeted hex that is to be filled in this border
        /// </summary>
        public Point TargetHex { get; set; }



        public HexagonBorder()
        {
            //Sets default hexagon property values
            Orientation = Orientation.PointyLayout;
            Size = new Point(100, 100);
            Origin = new Point(0, 0);
            TargetHex = Origin;

            DefaultBuilder();
        }
        public HexagonBorder(Orientation orientation, Point size, Point origin, Point? targetHex)
        {
            if (targetHex == null)
            {
                targetHex = origin;
            }

            Orientation = orientation;
            Size = size;
            Origin = origin;
            TargetHex = (Point)targetHex;
            DefaultBuilder();
        }


        /// <summary>
        /// Copied from documentation on borders - example of setters.
        /// </summary>
        private void DefaultBuilder()
        {
            HexagonLayout layout = new HexagonLayout(Orientation, Size, Origin);
            int x = (int)TargetHex.X;
            int y = (int)TargetHex.Y;
            //assuming padding means the spacing i reserve for this hexagon
            Thickness padding = new Thickness(Size.X / 2, Size.Y / 2);
            Hex hex = new Hex(x, y);
            List<Point> points = Hexagon.HexagonPolygonCorners(layout, hex);
            PointCollection pointCollection = new PointCollection(points.ToArray());

            this.Stroke = Color.FromArgb("#C49B33");
            this.Background = Color.FromArgb("#2B0B98");
            this.StrokeThickness = 2;
            this.Padding = padding;
            this.StrokeShape = new Polygon
            {
                Points = pointCollection
            };
            this.Content = new Label
            {
                Text = "102",
                TextColor = Colors.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                FontAutoScalingEnabled = true,
                MaxLines = 1,
                HorizontalOptions = LayoutOptions.Center,
            };
        }
    }

    public class HexGridTest
    {
        public AbsoluteLayout AbsoluteLayoutVar { get; set; }
        /*public HexGridTest()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.VerticalOptions = LayoutOptions.Center;
            absoluteLayout.HorizontalOptions = LayoutOptions.Center;
            Point center = new(absoluteLayout.X, absoluteLayout.Y);
            Point size = new Point(60, 60);
            HexagonLayout hexLayout = new HexagonLayout(Orientation.PointyLayout, size, center);  
            List<Hex> hexes = new List<Hex>(36);
            hexes = HexagonalGridShapes.GetMapShapes(hexes, GridOrientationConsts.PointyOrientation, HexagonalGridShapes.Hexagon);

            hexes.ForEach(hex =>
            {
                var point = new Point(hex.q, hex.r);
                absoluteLayout.Children.Add(new HexagonBorder(Orientation.PointyLayout, size, center, point));
            });
            AbsoluteLayout = absoluteLayout;
        }*/
        public HexGridTest()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.VerticalOptions = LayoutOptions.Center;
            absoluteLayout.HorizontalOptions = LayoutOptions.Center;
            //absoluteLayout.MaximumHeightRequest = 60;
            //absoluteLayout.MaximumWidthRequest = 60;
            
            Point center = new(absoluteLayout.X, absoluteLayout.Y);
            Point size = new Point(60, 60);
            HexagonLayout hexLayout = new HexagonLayout(Orientation.PointyLayout, size, center);
            List<Hex> hexes = new List<Hex>(4);
            hexes = HexagonalGridShapes.GetMapShapes(hexes, GridOrientationConsts.PointyOrientation, HexagonalGridShapes.Hexagon);

            hexes.ForEach(hex =>
            {
                var point = new Point(hex.q, hex.r);
                PointCollection points = new PointCollection(Hexagon.HexagonPolygonCorners(hexLayout, hex).ToArray());
                Point pixels = hexLayout.HexToPixel(hex);
                Polygon hexagon = new Polygon
                {
                    //Frame = new(pixels.X, pixels.Y, size.X, size.Y),
                    Points = points,
                    Fill = Color.FromArgb("#2B0B98"),
                    Stroke = Color.FromArgb("#C49B33"),

                    Aspect = Stretch.Uniform,
                    StrokeThickness = 1,
                    TranslationX = pixels.X,
                    TranslationY = pixels.Y,
                    
                };

                //TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                //TapGestureRecognizer.
                /*Border borderedHex = new Border
                {
                    Stroke = Color.FromArgb("#C49B33"),
                    StrokeShape = hexagon,
                    GestureRecognizers =
                    {

                    }

                };

                absoluteLayout.Children.Add(borderedHex);*/
            });
            AbsoluteLayoutVar = absoluteLayout;
        }

        private string PathGeoString(PointCollection points)
        {
            string pathGeometryString = string.Empty;
            List<Point> pts = points.ToList();
            foreach(Point point in pts)
            {
                pathGeometryString += point.X + ", " + point.Y + " ";
            }
            pathGeometryString.TrimEnd(' ');
            return pathGeometryString;
        }

    }

}
