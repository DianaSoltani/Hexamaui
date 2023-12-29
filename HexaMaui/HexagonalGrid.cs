using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HexaMaui.HexagonalGridShapes;

namespace HexaMaui
{
    public class HexagonalGrid
    {
        int maxSize; 
        List<Hex> grid;
        
        public HexagonalGrid(int size=0) 
        { 
            grid = GetMapShapes(new List<Hex>(5), GridOrientationConsts.PointyOrientation, HexagonalGridShapes.Hexagon);
            maxSize = size;
        }
    }
}
