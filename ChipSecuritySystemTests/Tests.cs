using System;
using System.Collections.Generic;
using System.Linq;
using ChipSecuritySystem;
using NUnit.Framework;

namespace ChipSecuritySystemTests
{
    [TestFixture]
    public class Tests
    {
        private bool CompareColorChips(List<ColorChip> expected, List<ColorChip> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            return !expected.Where((t, i) => t.StartColor != actual[i].StartColor || t.EndColor != actual[i].EndColor).Any();
        }
        
        [Test]
        public void ChipSecuritySystemExample()
        {
            // Arrange
            var input = new string []{"[Blue, Yellow]", "[Red, Green]", "[Yellow, Red]", "[Orange, Purple]"};
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
            };

            // Act
            var actual = Program.GetOrganizedChips(input);

            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipSecuritySystemSingleChip()
        {
            // Arrange
            var input = new string []{"[Blue, Green]"};
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green)
            };

            // Act
            var actual = Program.GetOrganizedChips(input);

            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipSecuritySystemRepeatedPath()
        {
            // Arrange
            var input = new string []{"[Blue, Green]", "[Green, Blue]", "[Blue, Green]", "[Green, Blue]", "[Blue, Green]", "[Orange, Purple]"};
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
            };

            // Act
            var actual = Program.GetOrganizedChips(input);

            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipSecuritySystemMultipleSolutions()
        {
            // Arrange
            var input = new string []{"[Blue, Red]", "[Blue, Yellow]", "[Yellow, Purple]", "[Purple, Orange]", "[Orange, Blue]", "[Red, Green]"};
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue),
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Blue, Color.Green),
            };

            // Act
            var actual = Program.GetOrganizedChips(input);

            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }

        [Test]
        public void ChipSecuritySystemEmptyTest()
        {
            // Arrange
            var input = new string []{""};
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                actual = Program.GetOrganizedChips(input);
            } 
            catch (Exception e)
            {
                exception = e;
            }
          
            // Assert
            Assert.True(actual.Count == 0);
            Assert.True(exception != null);
            Assert.True(exception.Message == Constants.ErrorMessage);
        }
        
        [Test]
        public void ChipSecuritySystemInvalidColor()
        {
            // Arrange
            var input = new string []{"[Blue, Yellow]", "[Red, Green]", "[Yellow, Red]", "[Brown, Purple]"};

            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                actual = Program.GetOrganizedChips(input);
            } 
            catch (Exception e)
            {
                exception = e;
            }
          
            // Assert
            Assert.True(actual.Count == 0);
            Assert.True(exception != null);
            Assert.True(exception.Message == Constants.ErrorMessage);
        }
        
        [Test]
        public void ChipSecuritySystemInvalidTest_NoExpectedEndColor()
        {
            // Arrange
            var input = new string []{"[Blue, Yellow]", "[Yellow, Red]", "[Orange, Purple]", "[Purple, Orange]"};
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                actual = Program.GetOrganizedChips(input);
            } 
            catch (Exception e)
            {
                exception = e;
            }
          
            // Assert
            Assert.True(actual.Count == 0);
            Assert.True(exception != null);
            Assert.True(exception.Message == Constants.ErrorMessage);
        }
        
        [Test]
        public void ChipSecuritySystemInvalidTest_NoExpectedStartColor()
        {
            // Arrange
            var input = new string []{"[Green, Yellow]", "[Yellow, Red]", "[Orange, Green]", "[Purple, Orange]"};
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                actual = Program.GetOrganizedChips(input);
            } 
            catch (Exception e)
            {
                exception = e;
            }
          
            // Assert
            Assert.True(actual.Count == 0);
            Assert.True(exception != null);
            Assert.True(exception.Message == Constants.ErrorMessage);
        }
        
        [Test]
        public void ChipSecuritySystemInvalidTest_NoLinkingColors()
        {
            // Arrange
            var input = new string []{"[Blue, Yellow]", "[Yellow, Red]", "[Orange, Green]", "[Purple, Green]"};
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                actual = Program.GetOrganizedChips(input);
            } 
            catch (Exception e)
            {
                exception = e;
            }
          
            // Assert
            Assert.True(actual.Count == 0);
            Assert.True(exception != null);
            Assert.True(exception.Message == Constants.ErrorMessage);
        }
    }
}