namespace ArthurKnight.Core
{
    public static class NumberExtension
    {
        public static float Normalized(this int value, int maxValue, bool reverse = false)
        {
            if (maxValue <= 0)
                return 0f;

            var normalized = (float) value / (float)maxValue;
            return reverse ? (1 - normalized) : normalized;
        }
        
        //public static float Normalized(this float value, float maxValue) => (value / maxValue);
    }
}