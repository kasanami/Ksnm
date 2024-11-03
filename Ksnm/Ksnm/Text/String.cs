namespace Ksnm.Text
{
    public static class String
    {
        /// <summary>
        /// 2つの文字列のハミング距離を求める
        /// </summary>
        public static int HammingDistance(string text1, string text2)
        {
            var maxLength = Math.Max(text1.Length, text2.Length);
            var minLength = Math.Min(text1.Length, text2.Length);
            int distance = maxLength - minLength;
            for (int i = 0; i < minLength; i++)
            {
                if (text1[i] != text2[i])
                {
                    distance++;
                }
            }
            return distance;
        }
    }
}
