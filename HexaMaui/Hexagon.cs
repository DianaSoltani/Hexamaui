namespace HexaMaui
{
    // All the code in this file is included in all platforms.
    internal class HexDirections
    {
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
        private readonly IEnumerable<Hex> HexDirection = new List<Hex>
        {
            new(1,0,-1),
            new(1,-1,0),
            new(0,-1,1),
            new(-1,0,1),
            new(-1,1,0),
            new(0,1,-1)
        };

        /// <summary>
        /// Returns the correct one tile translation vector for the intended direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal Hex GetHexDirection(int direction)
        {
            if (direction < 0 || direction > 5)
            {
                throw new ArgumentOutOfRangeException("HexDirection expects a range from 0 to 5.");
            }
            return HexDirection.ElementAt(direction);
        }

        internal Hex HexNeighbor(Hex hex, int direction)
        {
            return Hex.HexAdd(hex, GetHexDirection(direction));
        }
    }

    public class FractionalHex
    {
        internal double q, r, s;
        internal FractionalHex(double q, double r, double s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
        }
    }
    public class Hex
    {
        internal readonly int q;
        internal readonly int r;
        internal readonly int s;
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

        internal static bool Equals(Hex a, Hex b)
        {
            return a.q == b.q && a.s == b.s && a.r == b.r;
        }

        internal static Hex HexAdd(Hex a, Hex b)
        {
            return new(a.q + b.q, a.r + b.r, a.s + b.s);
        }
        internal static Hex HexSubtract(Hex a, Hex b)
        {
            return new(a.q - b.q, a.r - b.r, a.s - b.s);
        }
        internal static Hex HexMultiply(Hex a, int k)
        {
            return new(a.q * k, a.r * k, a.s * k);
        }

        internal int Length()
        {
            return (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
        }
        internal int DistanceTo(Hex destination)
        {
            return HexSubtract(this, destination).Length();
        }

        internal static Hex HexRound(FractionalHex hex)
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
    }
    public class Hexagon : GraphicsView
    {
        internal static Point HexCornerOffset(HexagonLayout layout, int corner)
        {
            Point size = layout.Size;
            double angle = 2.0 * Math.PI * (layout.HexOrientation.startAngle + corner) / 6;
            return new Point(size.X * Math.Cos(angle), size.Y * Math.Sin(angle));
        }

        private static float LinearInterpolation(double a, double b, double t)
        {
            return (float)(a * (1 - t) + b * t);
        }

        internal static FractionalHex HexLinearInterpolation(FractionalHex a, FractionalHex b, double t)
        {
            return new FractionalHex(
                        LinearInterpolation(a.q, b.q, t),
                        LinearInterpolation(a.s, b.s, t),
                        LinearInterpolation(a.r, b.r, t)
                   );
        }
        public static List<Point> HexagonCorners(HexagonLayout layout, Hex hex)
        {
            List<Point> corners = new List<Point>();
            Point center = HexaMaui.HexagonLayout.HexToPixel(layout, hex);
            for (int i = 0; i < 6; i++)
            {
                Point offset = HexCornerOffset(layout, i);
                corners.Add(offset);
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
