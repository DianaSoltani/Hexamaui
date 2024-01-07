namespace HexaMaui
{
    public static class GridOrientationConsts
    {
        //Parallelogram
        public const string PointyTopLeft = nameof(PointyTopLeft);
        public const string PointyTop = nameof(PointyTop);
        public const string PointyTopRight = nameof(PointyTopRight);
        public const string FlatTopLeft = nameof(FlatTopLeft);
        public const string FlatTop = nameof(FlatTop);
        public const string FlatTopRight = nameof(FlatTopRight);

        //Triangle
        public const string PointyNorth = nameof(PointyNorth);
        public const string PointySouthWest = nameof(PointySouthWest);
        public const string PointySouthEast = nameof(PointySouthEast);
        public const string FlatWest = nameof(FlatWest);
        public const string FlatNorthEast = nameof(FlatNorthEast);
        public const string FlatSouthEast = nameof(FlatSouthEast);

        //Hexagon / Rectangle
        public const string PointyOrientation = nameof(PointyOrientation);
        public const string FlatOrientation = nameof(FlatOrientation);
    }
    /// <summary>
    /// Creates the grid shape by correctly filling in a 2 dimensional data structure of Hexes
    /// </summary>
    public static class HexagonalGridShapes
    {
        public const string Parallelogram = nameof(Parallelogram);
        public const string Triangle = nameof(Triangle);
        public const string Hexagon = nameof(Hexagon);
        public const string Rectangle = nameof(Rectangle);

        /// <summary>
        /// Todo:
        /// </summary>
        /// <param name="hexList"></param>
        /// <param name="orientation"></param>
        /// <param name="shape"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<Hex> GetMapShapes(List<Hex> hexList, string orientation, string shape = Hexagon)
        {
            if (string.Equals(Parallelogram, shape, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException();
            }
            else if (string.Equals(Triangle, shape, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException();
            }
            else if (string.Equals(Hexagon, shape, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(orientation))
                {
                    orientation = GridOrientationConsts.PointyOrientation;
                }

                if (string.Equals(GridOrientationConsts.PointyOrientation, orientation, StringComparison.OrdinalIgnoreCase))
                {
                    HexagonalGridFill(hexList);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (string.Equals(Rectangle, shape, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException();
            }
            return hexList;
        }

        internal static List<Hex> HexagonalGridFill(List<Hex> hexGrid)
        {
            int n = hexGrid.Capacity / 2;
            for (int q = -n; q <= n; q++)
            {
                int r1 = Math.Max(-n, -q - n);
                int r2 = Math.Min(n, -q + n);
                for (int r = r1; r <= r2; r++)
                {
                    hexGrid.Add(new Hex(q, r, -q - r));
                }
            }
            return hexGrid;
        }
    }
}
