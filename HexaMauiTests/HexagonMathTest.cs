using static HexaMauiTests.TestHelpers;
using HexaMaui;
using static System.Net.Mime.MediaTypeNames;
namespace HexaMauiTests
{
    [TestClass]
    public class HexagonMathTest
    {
        [TestMethod]
        public void TestHexArithmetic()
        {
            EqualHex("hex_add", new Hex(4, -10, 6), new Hex(1, -3, 2).Add(new Hex(3, -7, 4)));
            EqualHex("hex_subtract", new Hex(-2, 4, -2), new Hex(1, -3, 2).Subtract(new Hex(3, -7, 4)));
        }


        [TestMethod]
        public void TestHexDirection()
        {
            EqualHex("hex_direction", new Hex(0, -1, 1), Hex.Direction(2));
        }

        [TestMethod]
        public void TestHexNeighbor()
        {
            EqualHex("hex_neighbor", new Hex(1, -3, 2), new Hex(1, -2, 1).Neighbor(2));
        }

        [TestMethod]
        public void TestHexDiagonal()
        {
            EqualHex("hex_diagonal", new Hex(-1, -1, 2), new Hex(1, -2, 1).DiagonalNeighbor(3));
        }


        [TestMethod]
        public void TestHexDistance()
        {
            EqualInt("hex_distance", 7, new Hex(3, -7, 4).DistanceTo(new Hex(0, 0, 0)));
        }


        [TestMethod]
        public void TestHexRotateRight()
        {
            EqualHex("hex_rotate_right", new Hex(1, -3, 2).RotateRight(), new Hex(3, -2, -1));
        }


        [TestMethod]
        public void TestHexRotateLeft()
        {
            EqualHex("hex_rotate_left", new Hex(1, -3, 2).RotateLeft(), new Hex(-2, -1, 3));
        }


        [TestMethod]
        public void TestHexRound()
        {
            FractionalHex a = new FractionalHex(0.0, 0.0, 0.0);
            FractionalHex b = new FractionalHex(1.0, -1.0, 0.0);
            FractionalHex c = new FractionalHex(0.0, -1.0, 1.0);
            EqualHex("hex_round 1", new Hex(5, -10, 5), Hex.HexRound(Hexagon.HexLinearInterpolation(new FractionalHex(0.0, 0.0, 0.0),new FractionalHex(10.0, -20.0, 10.0), 0.5)));
            EqualHex("hex_round 2", Hex.HexRound(a), Hex.HexRound(Hexagon.HexLinearInterpolation(a, b, 0.499)));
            EqualHex("hex_round 3", Hex.HexRound(b), Hex.HexRound(Hexagon.HexLinearInterpolation(a, b, 0.501)));
            EqualHex("hex_round 4", Hex.HexRound(a), Hex.HexRound(new FractionalHex(a.q * 0.4 + b.q * 0.3 + c.q * 0.3, a.r * 0.4 + b.r * 0.3 + c.r * 0.3, a.s * 0.4 + b.s * 0.3 + c.s * 0.3)));
            EqualHex("hex_round 5", Hex.HexRound(c), Hex.HexRound(new FractionalHex(a.q * 0.3 + b.q * 0.3 + c.q * 0.4, a.r * 0.3 + b.r * 0.3 + c.r * 0.4, a.s * 0.3 + b.s * 0.3 + c.s * 0.4)));
        }


        [TestMethod]
        public void TestHexLinedraw()
        {
            EqualHexArray("hex_linedraw", new List<Hex> { new Hex(0, 0, 0), new Hex(0, -1, 1), new Hex(0, -2, 2), new Hex(1, -3, 2), new Hex(1, -4, 3), new Hex(1, -5, 4) }, Hexagon.HexLinedraw(new Hex(0, 0, 0), new Hex(1, -5, 4)));
        }


        [TestMethod]
        public void TestLayout()
        {
            Hex h = new Hex(3, 4, -7);
            HexagonLayout flat = new HexagonLayout(Orientation.FlatLayout, new Point(10.0, 15.0), new Point(35.0, 71.0));
            EqualHex("layout", h, Hex.HexRound(flat.PixelToHex(flat.HexToPixel(h))));
            HexagonLayout pointy = new HexagonLayout(Orientation.PointyLayout, new Point(10.0, 15.0), new Point(35.0, 71.0));
            EqualHex("layout", h, Hex.HexRound(pointy.PixelToHex(pointy.HexToPixel(h))));
        }
        [TestMethod]
        public void TestOffsetRoundtrip()
        {
            Hex a = new Hex(3, 4, -7);
            OffsetCoord b = new OffsetCoord(1, -3);
            EqualHex("conversion_roundtrip even-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, a)));
            EqualOffsetcoord("conversion_roundtrip even-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, b)));
            EqualHex("conversion_roundtrip odd-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.ODD, OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, a)));
            EqualOffsetcoord("conversion_roundtrip odd-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, OffsetCoord.QoffsetToCube(OffsetCoord.ODD, b)));
            EqualHex("conversion_roundtrip even-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.EVEN, OffsetCoord.RoffsetFromCube(OffsetCoord.EVEN, a)));
            EqualOffsetcoord("conversion_roundtrip even-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.EVEN, OffsetCoord.RoffsetToCube(OffsetCoord.EVEN, b)));
            EqualHex("conversion_roundtrip odd-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.ODD, OffsetCoord.RoffsetFromCube(OffsetCoord.ODD, a)));
            EqualOffsetcoord("conversion_roundtrip odd-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.ODD, OffsetCoord.RoffsetToCube(OffsetCoord.ODD, b)));
        }


        [TestMethod]
        public void TestOffsetFromCube()
        {
            EqualOffsetcoord("offset_from_cube even-q", new OffsetCoord(1, 3), OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, new Hex(1, 2, -3)));
            EqualOffsetcoord("offset_from_cube odd-q", new OffsetCoord(1, 2), OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, new Hex(1, 2, -3)));
        }


        [TestMethod]
        public void TestOffsetToCube()
        {
            EqualHex("offset_to_cube even-", new Hex(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(1, 3)));
            EqualHex("offset_to_cube odd-q", new Hex(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.ODD, new OffsetCoord(1, 2)));
        }


        [TestMethod]
        public void TestDoubledRoundtrip()
        {
            Hex a = new Hex(3, 4, -7);
            DoubledCoord b = new DoubledCoord(1, -3);
            EqualHex("conversion_roundtrip doubled-q", a, DoubledCoord.QdoubledFromCube(a).QdoubledToCube());
            EqualDoubledcoord("conversion_roundtrip doubled-q", b, DoubledCoord.QdoubledFromCube(b.QdoubledToCube()));
            EqualHex("conversion_roundtrip doubled-r", a, DoubledCoord.RdoubledFromCube(a).RdoubledToCube());
            EqualDoubledcoord("conversion_roundtrip doubled-r", b, DoubledCoord.RdoubledFromCube(b.RdoubledToCube()));
        }


        [TestMethod]
        public void TestDoubledFromCube()
        {
            EqualDoubledcoord("doubled_from_cube doubled-q", new DoubledCoord(1, 5), DoubledCoord.QdoubledFromCube(new Hex(1, 2, -3)));
            EqualDoubledcoord("doubled_from_cube doubled-r", new DoubledCoord(4, 2), DoubledCoord.RdoubledFromCube(new Hex(1, 2, -3)));
        }


        [TestMethod]
        public void TestDoubledToCube()
        {
            EqualHex("doubled_to_cube doubled-q", new Hex(1, 2, -3), new DoubledCoord(1, 5).QdoubledToCube());
            EqualHex("doubled_to_cube doubled-r", new Hex(1, 2, -3), new DoubledCoord(4, 2).RdoubledToCube());
        }

    }
}