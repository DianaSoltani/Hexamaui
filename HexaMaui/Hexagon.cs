namespace HexaMaui
{
    // All the code in this file is included in all platforms.
    public class FractionalHex
    {
        public double q, r, s;
        public FractionalHex(double q, double r, double s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
        }
    }
    public class Hex
    {
        public readonly int q;
        public readonly int r;
        public readonly int s;
        public Hex(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
            if (q + r + s != 0)
            {
                throw new ArgumentException("The axial coordinates provided are not valid. " +
                    "Ensure your coodinates sum to 0.");
            }
        }
        public Hex(int q, int r)
        {
            this.q = q;
            this.r = r;
            this.s = -q - r;
        }

        public bool Equals(Hex other)
        {
            return this.q == other.q && this.r == other.r && this.s == other.s;
        }

        public Hex Add(Hex b)
        {
            return new Hex(q + b.q, r + b.r, s + b.s);
        }


        public Hex Subtract(Hex b)
        {
            return new Hex(q - b.q, r - b.r, s - b.s);
        }


        public Hex Scale(int k)
        {
            return new Hex(q * k, r * k, s * k);
        }


        public Hex RotateLeft()
        {
            return new Hex(-s, -q, -r);
        }


        public Hex RotateRight()
        {
            return new Hex(-r, -s, -q);

        }

        public int Length()
        {
            return (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
        }
        public int DistanceTo(Hex destination)
        {
            return Subtract(destination).Length();
        }

        public static Hex HexRound(FractionalHex hex)
        {
            int qInt = (int)(Math.Round(hex.q));
            int rInt = (int)(Math.Round(hex.r));
            int sInt = (int)(Math.Round(hex.s));
            double qDiff = Math.Abs(qInt - hex.q);
            double rDiff = Math.Abs(rInt - hex.r);
            double sDiff = Math.Abs(sInt - hex.s);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                qInt = -rInt - sInt;
            }
            else if (rDiff > sDiff)
            {
                rInt = -qInt - sInt;
            }
            else
            {
                sInt = -qInt - rInt;
            }
            return new Hex(qInt, rInt, sInt);
        }
        /// <summary>
        /// <list type="bullet">
        ///  <item> 0 : (Pointy) Top Right, (Flat) Top Right</item>
        ///  <item> 1 : (Pointy) , (Flat) </item>
        ///  <item> 2 : (Pointy) , (Flat) </item>
        ///  <item> 3 : (Pointy) , (Flat) </item>
        ///  <item> 4 : (Pointy) , (Flat) </item>
        ///  <item> 5 : (Pointy) , (Flat) </item>
        ///  </list>
        /// </summary>
        public static readonly List<Hex> HexDirections = new List<Hex>
            {
                new Hex(1, 0, -1),
                new Hex(1, -1, 0),
                new Hex(0, -1, 1),
                new Hex(-1, 0, 1),
                new Hex(-1, 1, 0),
                new Hex(0, 1, -1)
            };

        public static readonly List<Hex> HexDiagonals = new List<Hex>
        {
            new(2, -1, -1),
            new(1, -2, 1),
            new(-1, -1, 2),
            new(-2, 1, 1),
            new(-1, 2, -1),
            new (1, 1, -2)
        };
        static public Hex Direction(int direction)
        {
            return Hex.HexDirections[direction];
        }


        public Hex Neighbor(int direction)
        {
            return Add(Hex.Direction(direction));
        }

        public Hex DiagonalNeighbor(int direction)
        {
            return Add(HexDiagonals[direction]);
        }

    }
    public class Hexagon : GraphicsView
    {
        public static Point HexCornerOffset(HexagonLayout layout, int corner)
        {
            Point size = layout.Size;
            double angle = 2.0 * Math.PI * (layout.HexOrientation.startAngle - corner) / 6;
            return new Point(size.X * Math.Cos(angle), size.Y * Math.Sin(angle));
        }

        private static float LinearInterpolation(double a, double b, double t)
        {
            return (float)(a * (1.0 - t) + b * t);
        }

        public static FractionalHex HexLinearInterpolation(FractionalHex a, FractionalHex b, double t)
        {
            return new FractionalHex(
                        LinearInterpolation(a.q, b.q, t),
                        LinearInterpolation(a.r, b.r, t),
                        LinearInterpolation(a.s, b.s, t)
                   );
        }
        public static List<Point> HexagonPolygonCorners(HexagonLayout layout, Hex hex)
        {
            List<Point> corners = new List<Point>();
            Point center = layout.HexToPixel(hex);
            for (int i = 0; i < 6; i++)
            {
                Point offset = HexCornerOffset(layout, i);
                Point adjustedPoint = new Point(center.X + offset.X, center.Y + offset.Y);
                corners.Add(adjustedPoint);
            }
            return corners;
        }

        /// <summary>
        /// Todo:
        /// </summary>
        /// <param name="a">Hex</param>
        /// <param name="b">Hex</param>
        /// <returns>List<Hex> object.</Hex></returns>
        public static List<Hex> HexLinedraw(Hex a, Hex b)
        {
            int distance = a.DistanceTo(b);
            List<Hex> result = new List<Hex>();
            FractionalHex a_nudge = new FractionalHex(a.q + 1e-06, a.r + 1e-06, a.s - 2e-06);
            FractionalHex b_nudge = new FractionalHex(b.q + 1e-06, b.r + 1e-06, b.s - 2e-06);
            double step = 1.0 / Math.Max(distance, 1);
            for (int i = 0; i <= distance; i++)
            {
                result.Add(Hex.HexRound(HexLinearInterpolation(a_nudge, b_nudge, step * i)));
            }
            return result;
        }


    }
}
