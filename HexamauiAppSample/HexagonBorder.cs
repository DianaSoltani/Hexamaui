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
        
        public HexGridTest()
        {
            //Hex Layout Set-Up
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.VerticalOptions = LayoutOptions.Center;
            absoluteLayout.HorizontalOptions = LayoutOptions.Center;
            Point center = new(absoluteLayout.X, absoluteLayout.Y);
            Point size = new Point(60.0, 60.0);
            HexagonLayout hexLayout = new HexagonLayout(Orientation.FlatLayout, size, center);
            List<Hex> hexes = new List<Hex>(7);
            hexes = HexagonalGridShapes.GetMapShapes(hexes, GridOrientationConsts.FlatOrientation, HexagonalGridShapes.Hexagon);

            int[] lostNumbers = { 4, 8, 15, 16, 23, 42 };
            int i = 0;

            hexes.ForEach(hex =>
            {
                var point = new Point(hex.q, hex.r);
                Point[] pointsArray = Hexagon.HexagonPolygonCorners(hexLayout, hex).ToArray();
                PointCollection points = new PointCollection(pointsArray);
                Point pixels = hexLayout.HexToPixel(hex);

                double height = Orientation.PointyLayout == hexLayout.HexOrientation ? 2 * size.Y : Math.Sqrt(3) * size.Y;
                double width = Orientation.PointyLayout == hexLayout.HexOrientation ? Math.Sqrt(3) * size.X : 2 * size.X;
                double strokeThickness = 3;

                Polygon currentHex = new Polygon
                {
                    Points = points,
                    Fill = Color.FromArgb("#2B0B98"),
                    Stroke = Color.FromArgb("#C49B33"),
                    Aspect = Stretch.Fill,
                    //StrokeThickness = 2,
                };
                Border border = new Border
                {
                    Stroke = currentHex.Stroke,
                    Padding = 0,
                    Background = currentHex.Fill,
                    StrokeThickness = strokeThickness,
                    TranslationX = pixels.X,
                    TranslationY = pixels.Y,
                    HeightRequest = height - strokeThickness,
                    WidthRequest = width - strokeThickness,
                    StrokeShape = currentHex,
                    Content = new Label
                    {
                        Text = lostNumbers[i++%6].ToString(),
                        TextColor = Colors.White,
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        FontAutoScalingEnabled = true,
                        MaxLines = 1,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    },
                };
                

                //Tap Gesture Set-Up
                TapGestureRecognizer HexagonTapGestureRecognizer = new TapGestureRecognizer
                {
                    Buttons = ButtonsMask.Primary,
                };
                HexagonTapGestureRecognizer.Tapped += (s, e) => OnHexagonPrimaryTapped(s!, e); 
                
                TapGestureRecognizer HexagonSecondaryTapGestureRecognizer = new TapGestureRecognizer
                {
                    Buttons = ButtonsMask.Secondary,
                };
                HexagonSecondaryTapGestureRecognizer.Tapped += (s, e) => OnHexagonSecondaryTapped(s!, e);

                border.GestureRecognizers.Add(HexagonTapGestureRecognizer);
                border.GestureRecognizers.Add(HexagonSecondaryTapGestureRecognizer);

                absoluteLayout.Children.Add(border);
            });
            AbsoluteLayoutVar = absoluteLayout;
        }

        //following https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/gestures/tap?view=net-maui-8.0#define-the-button-mask
        public void OnHexagonPrimaryTapped(object sender, TappedEventArgs e)
        {
            Border hex = (Border)sender;
            hex.Background = Colors.LimeGreen;
        }

        public void OnHexagonSecondaryTapped(object sender, TappedEventArgs e)
        {
            Border hex = (Border)sender;
            hex.Background = Colors.Red;
        }


    }
}
