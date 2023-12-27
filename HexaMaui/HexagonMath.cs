namespace HexaMaui
{
    public class HexagonMath
    {
        /// <summary>
        /// The interior angle to a regular hexagon.
        /// </summary>
        public const int InteriorAngle = 120;
        public double Width;
        public double Height;
        /// <summary>
        /// This is considered the radius of an outer circle that would be touching the edges of the hexagon.
        /// </summary>
        public int Size;
        public bool IsFlatTop;

        public HexagonMath(int size, bool isFlatTop = true)
        {
            Size = size;
            CalculateWidthAndHeight();
            IsFlatTop = isFlatTop;
        }
        
        private void CalculateWidthAndHeight()
        {
            if(IsFlatTop)
            {
                Height = (3 / 2) * Size;
                Width = 2 * Size;
            }
            else
            {
                Height = 2 * Size;
                Width = (3 / 2) * Size;
            }

        }

    }
}
