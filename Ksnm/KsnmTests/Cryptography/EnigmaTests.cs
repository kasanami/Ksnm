using Ksnm.Cryptography.Enigma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Cryptography.Tests
{
    [TestClass()]
    public class EnigmaTests
    {
        [TestMethod()]
        public void RotorTest()
        {
            var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 'Q');

            int input = 'A' - 'A';

            int output = rotor.Forward(input);

            Assert.AreEqual('E', 'A' + output);

            int output2 = rotor.Backward(output);

            Assert.AreEqual('A', 'A' + output2);

            for (int i = 0; i < 26; i++)
            {
                int x = rotor.Forward(i);
                int y = rotor.Backward(x);

                Assert.AreEqual(i, y, $"Rotor inverse error: input {i}, forward {x}, backward {y}");
            }
        }
    }
}
