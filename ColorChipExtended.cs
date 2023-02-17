namespace ChipSecuritySystem
{
    public class ColorChipExtended : ColorChip
    {
        private bool _visited;
        
        public ColorChipExtended(Color startColor, Color endColor) : base(startColor, endColor)
        {
            _visited = false;
        }
        
        public bool Visited
        {
            get { return _visited; }
            set { _visited = value; }
        }
    }
}