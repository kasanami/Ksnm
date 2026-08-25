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
    }
}
