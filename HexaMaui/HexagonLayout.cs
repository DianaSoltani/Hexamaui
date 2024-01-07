namespace HexaMaui
{
    public class HexagonLayout
    {
        public Orientation HexOrientation;
        /// <summary>
        /// Scale transformation to determine the size of the hexagon.
        /// </summary>
        public Point Size;
        /// <summary>
        /// Sets the center of the q=0, r=0 hexagon. 
        /// </summary>
        public Point Origin;

        public HexagonLayout(Orientation orientation, Point size, Point origin) 
        {
            HexOrientation = orientation;
            Size = size;
            Origin = origin;
        }

        public Point HexToPixel(Hex hex)
        {
            Orientation currentOrientation = this.HexOrientation;
            double x = (currentOrientation.f0 * hex.q + currentOrientation.f1 * hex.r) * this.Size.X;
            double y = (currentOrientation.f2 * hex.q + currentOrientation.f3 * hex.r) * this.Size.Y;
            return new Point(x + this.Origin.X, y + this.Origin.Y);
        }

        public FractionalHex PixelToHex(Point point) 
        {
            Orientation currentOrientation = this.HexOrientation;
            Point pointCalculated = new Point(
                                            point.X - this.Origin.X / this.Size.X ,
                                            point.Y - this.Origin.Y / this.Size.Y
                                    );
            double q = currentOrientation.b0 * pointCalculated.X + currentOrientation.b1 * pointCalculated.Y;
            double r = currentOrientation.b2 * pointCalculated.X + currentOrientation.b3 * pointCalculated.Y;
            return new FractionalHex(q, r, -q -r);
        }

    }
}
