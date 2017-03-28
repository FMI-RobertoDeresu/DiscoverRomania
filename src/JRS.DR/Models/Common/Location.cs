namespace JRS.DR.Models.Common
{
    public class Location : EntityComplexBase<int>
    {
        protected Location() { }

        public Location(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public decimal X { get; protected set; }

        public decimal Y { get; protected set; }
    }
}