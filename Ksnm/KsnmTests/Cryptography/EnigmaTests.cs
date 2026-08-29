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
        [TestMethod()]
        public void ReflectorTest()
        {
            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");

            for (int i = 0; i < 26; i++)
            {
                int x = reflector.Reflect(i);
                int y = reflector.Reflect(x);

                Assert.AreEqual(i, y, $"Reflector inverse error: input {i}, reflect {x}, reflect {y}");
            }
        }
        [TestMethod()]
        public void EnigmaMachineTest()
        {
            var rotorI = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 'Q');

            var rotorII = new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", 'E');

            var rotorIII = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", 'V');

            var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");

            var plugboard = new Plugboard("AB CD EF");

            var machine = new EnigmaMachine(rotorI, rotorII, rotorIII, reflector, plugboard);

            var planeText = "HELLOWORLD";
            var encrypted = machine.Encrypt(planeText);
            var decrypted = machine.Decrypt(encrypted);

            Assert.AreEqual(planeText, decrypted);
        }
        [TestMethod()]
        public void Rotor8Test()
        {
            var wiring = new byte[Rotor8.Size];
            for (int i = 0; i < Rotor8.Size; i++)
            {
                wiring[i] = (byte)((i + 1) % 256);
            }
            var rotor = new Rotor8(wiring, 0);

            byte input = 0;

            byte output = rotor.Forward(input);
            Assert.AreEqual(1, output);

            byte output2 = rotor.Backward(output);
            Assert.AreEqual(0, output2);

            for (int i = 0; i < Rotor8.Size; i++)
            {
                byte x = rotor.Forward((byte)i);
                byte y = rotor.Backward(x);

                Assert.AreEqual(i, y, $"Rotor inverse error: input {i}, forward {x}, backward {y}");
            }
        }
        [TestMethod()]
        public void Reflector8Test()
        {
            Random random = new Random(123);
            var reflector = new Reflector8(random);

            for (int i = 0; i < Rotor8.Size; i++)
            {
                byte x = reflector.Reflect((byte)i);
                byte y = reflector.Reflect(x);

                Assert.AreEqual(i, y, $"Reflector inverse error: input {i}, reflect {x}, reflect {y}");
            }
        }
        [TestMethod()]
        public void EnigmaMachine8Test()
        {
            Random random = new Random(123);
            var machine = new EnigmaMachine8(random);

            var planeText = "HELLOWORLD";
            var encrypted = machine.Encrypt(planeText);
            var decrypted = machine.DecryptToText(encrypted);

            Assert.AreEqual(planeText, decrypted);
        }
    }
}
