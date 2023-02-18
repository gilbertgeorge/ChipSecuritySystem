using System;
using System.IO;
using ChipSecuritySystem;
using NUnit.Framework;

namespace ChipSecuritySystemTests
{
    [TestFixture]
    public class ChipSecuritySystemProgramTests
    {
        [Test]
        public void ChipsSecuritySystem_ProvidedExample()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new string[] {"[Blue," ,"Yellow]", "[Red,", "Green]", "[Yellow,", "Red]", "[Orange,", "Purple]" });

                string expected = $"[Blue, Yellow] [Yellow, Red] [Red, Green]{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
        [Test]
        public void ChipsSecuritySystem_NoCommasAreHandled()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new string[] {"[Blue", "Yellow]", "[Yellow", "Green]" });

                string expected = $"[Blue, Yellow] [Yellow, Green]{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
        [Test]
        public void ChipsSecuritySystem_InvalidColorIgnored()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(new string[] {"[Blue", "Yellow]", "[Red", "Green]", "[Yellow", "Red]", "[Brown", "Purple]" });

                string expected = $"[Blue, Yellow] [Yellow, Red] [Red, Green]{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
        [Test]
        public void ChipsSecuritySystem_InvalidStringBrackets()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new string[] {"]Blue,", "Yellow[", "]Red,", "Green[" });

                string expected = $"{Constants.ErrorMessage}{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
        [Test]
        public void ChipsSecuritySystem_OddNumberOfArguments()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new string[] {"[Blue," ,"Yellow]", "[Red,", "Green]", "[Yellow,", "Red]", "[Orange," });

                string expected = $"{Constants.ErrorMessage}{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
        [Test]
        public void ChipsSecuritySystem_ProgramEmptyString()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new string[] { });

                string expected = $"{Constants.ErrorMessage}{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        
    }
}