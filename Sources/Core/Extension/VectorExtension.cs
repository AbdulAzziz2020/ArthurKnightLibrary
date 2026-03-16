using UnityEngine;

namespace ArthurKnight.Core
{
    public static class VectorExtension
    {
        public static float GetRandom(this Vector2 vector) => vector.x > vector.y
            ? Random.Range(vector.x, vector.y)
            : Random.Range(vector.y, vector.x);
        
        
        public static int GetRandom(this Vector2Int vector) => vector.x > vector.y
            ? Random.Range(vector.x, vector.y)
            : Random.Range(vector.y, vector.x);

        public static bool IsInRange(this Vector2 vector, float value)
        {
            if (vector.x > vector.y)
            {
                return vector.x >= value && vector.y <= value;
            }
            
            return vector.y >= value && vector.x <= value;
        }

        public static bool IsInRange(this Vector2Int vector, int value)
        {
            if (vector.x > vector.y)
            {
                return vector.x >= value && vector.y <= value;
            }
            
            return vector.y >= value && vector.x <= value;
        }
    }
}