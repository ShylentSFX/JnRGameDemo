namespace JnRGame
{
    public static class GameMath
    {
        public static byte[,] Reverse2DArrayAxis(byte[,] array)
        {
            int lengthY = array.GetLength(0);
            int lengthX = array.GetLength(1);
            byte[,] revArray = new byte[lengthX, lengthY];
            for (int i = 0; i < lengthX; i++)
            {
                for (int j = 0; j < lengthY; j++)
                {
                    revArray[i, j] = array[j, i];
                }
            }
            return revArray;
        }
    }
}
