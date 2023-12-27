namespace HexaMaui
{
    public class Layout
    {
        public Orientation HexOrientation;
        public Point Size;
        public Point Origin;

        public Layout(Orientation orientation, Point size, Point origin) 
        {
            HexOrientation = orientation;
            Size = size;
            Origin = origin;
        }

        public static Point HexToPixel(Layout layout, Hex hex)
        {
            Orientation currentOrientation = layout.HexOrientation;
            double x = (currentOrientation.f0 * hex.q + currentOrientation.f1 * hex.r) * layout.Size.X;
            double y = (currentOrientation.f2 * hex.q + currentOrientation.f3 * hex.r) * layout.Size.Y;
            return new Point(x + layout.Origin.X, y + layout.Origin.Y);
        }

        public static FractionalHex PixelToHex(Layout layout, Point point) 
        {
            Orientation currentOrientation = layout.HexOrientation;
            Point pointCalculated = new Point(
                                            point.X - layout.Origin.X / layout.Size.X ,
                                            point.Y - layout.Origin.Y / layout.Size.Y
                                    );
            double q = currentOrientation.b0 * pointCalculated.X + currentOrientation.b1 * pointCalculated.Y;
            double r = currentOrientation.b2 * pointCalculated.X + currentOrientation.b3 * pointCalculated.Y;
            return new FractionalHex(q, r, -q -r);
        }

    }
}
