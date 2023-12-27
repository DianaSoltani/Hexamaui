using System.Numerics;

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
        internal Hex(int q, int r, int s)
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
        internal Hex(int q, int r)
        {
            this.q = q;
            this.r = r;
            this.s = -q - r;
        }

        internal bool Equals(Hex other)
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
        internal Point HexCornerOffset(Layout layout, int corner)
        {
            Point size = layout.Size;
            double angle = 2.0 * Math.PI * (layout.HexOrientation.startAngle + corner) / 6;
            return new Point(size.X * Math.Cos(angle), size.Y * Math.Sin(angle));
        }
        public Vector<Point> HexagonCorners(Layout layout, Hex hex)
        {
            Vector<Point> corners = new Vector<Point>();
            Point center = HexaMaui.Layout.HexToPixel(layout, hex);
            for (int i = 0; i < 6; i++)
            {
                Point offset = HexCornerOffset(layout, i);
                corners = corners + new Vector<Point>(offset);
            }
            return corners;
        }

    }
}
