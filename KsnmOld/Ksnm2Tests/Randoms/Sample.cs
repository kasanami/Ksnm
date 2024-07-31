namespace Ksnm.Randoms.Tests
{
    /// <summary>
    /// テスト用乱数生成器
    /// </summary>
    class Sample : Ksnm.Randoms.RandomBase
    {
        static readonly ulong[] Values = new ulong[]
        {
            0,
            1,
            0xFFFFFFFFFFFFFFFE,
            0xFFFFFFFFFFFFFFFF,
        };
        int currentIndex;

        /// <summary>
        /// 0 以上で 0xFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFF 以下の32 ビット符号無し整数。</returns>
        public override uint GenerateUInt32()
        {
            return (uint)GenerateUInt64();
        }

        /// <summary>
        /// 0 以上で 0xFFFFFFFFFFFFFFFF 以下の乱数を返します。
        /// </summary>
        /// <returns>0 以上で 0xFFFFFFFFFFFFFFFF 以下の64 ビット符号無し整数。</returns>
        public override ulong GenerateUInt64()
        {
            var value = Values[currentIndex];
            currentIndex++;
            if (currentIndex >= Values.Length)
            {
                currentIndex = 0;
            }
            return value;
        }
    }
}
