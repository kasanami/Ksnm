/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using Ksnm.Numerics;
using Ksnm.Units.GS;

namespace Ksnm.Units
{
    /// <summary>
    /// 重力単位系の 1 を定義
    /// 使用例：
    /// var hoge = 123 * Hoge;
    /// </summary>
    public static class GSUnits<T> where T : IMath<T>
    {
        public static readonly KilogramForce<T> KilogramForce = new KilogramForce<T>(1);
        public static readonly StandardGravity<T> StandardGravity = new StandardGravity<T>(1);
    }
}