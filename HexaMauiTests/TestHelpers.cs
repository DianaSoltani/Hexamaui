using HexaMaui;
namespace HexaMauiTests
{
    internal static class TestHelpers
    {
        static public void EqualHex(String name, Hex a, Hex b)
        {
            if (!(a.q == b.q && a.s == b.s && a.r == b.r))
            {
                Complain(name);
            }
        }

        static public void EqualOffsetcoord(String name, OffsetCoord a, OffsetCoord b)
        {
            if (!(a.col == b.col && a.row == b.row))
            {
                Complain(name);
            }
        }

        static public void EqualDoubledcoord(String name, DoubledCoord a, DoubledCoord b)
        {
            if (!(a.col == b.col && a.row == b.row))
            {
                Complain(name);
            }
        }


        static public void EqualInt(String name, int a, int b)
        {
            if (!(a == b))
            {
                Complain(name);
            }
        }


        static public void EqualHexArray(String name, List<Hex> a, List<Hex> b)
        {
            EqualInt(name, a.Count, b.Count);
            for (int i = 0; i < a.Count; i++)
            {
                EqualHex(name, a[i], b[i]);
            }
        }
        public static void Complain(String name)
        {
            Console.WriteLine("FAILED " + name);
        }

    }
}
