using System;
using System.Collections.Generic;
using System.Linq;
using ChipSecuritySystem;
using NUnit.Framework;

namespace ChipSecuritySystemTests
{
    [TestFixture]
    public class ChipOrganizerTests
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
        public void ChipOrganizer_ProvidedExample()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
            };
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
            };
        
            // Act
            var chipOrganizer = new ChipOrganizer(input);
            chipOrganizer.GenerateAllValidSolutions();
            var actual = chipOrganizer.GetLongestSolution();
        
            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipOrganizer_SingleChip()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
            };
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green)
            };
        
            // Act
            var chipOrganizer = new ChipOrganizer(input);
            chipOrganizer.GenerateAllValidSolutions();
            var actual = chipOrganizer.GetLongestSolution();
        
            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipOrganizer_RepeatedPath()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
            };
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Green, Color.Blue),
                new ColorChip(Color.Blue, Color.Green),
            };
        
            // Act
            var chipOrganizer = new ChipOrganizer(input);
            chipOrganizer.GenerateAllValidSolutions();
            var actual = chipOrganizer.GetLongestSolution();
        
            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipOrganizer_MultipleSolutions()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue),
                new ColorChip(Color.Red, Color.Green),
            };
            var expected = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue),
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Green),
            };
        
            // Act
            var chipOrganizer = new ChipOrganizer(input);
            chipOrganizer.GenerateAllValidSolutions();
            var actual = chipOrganizer.GetLongestSolution();
        
            // Assert
            Assert.True(CompareColorChips(expected, actual));
        }
        
        [Test]
        public void ChipOrganizer_EmptyTest()
        {
            // Arrange
            var input = new List<ColorChip>();
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                // Act
                var chipOrganizer = new ChipOrganizer(input);
                chipOrganizer.GenerateAllValidSolutions();
                actual = chipOrganizer.GetLongestSolution();
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
        public void ChipOrganizer_NoExpectedEndColor()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
            };
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                // Act
                var chipOrganizer = new ChipOrganizer(input);
                chipOrganizer.GenerateAllValidSolutions();
                actual = chipOrganizer.GetLongestSolution();
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
        public void ChipOrganizer_NoExpectedStartColor()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Green, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Purple, Color.Orange),
            };
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                // Act
                var chipOrganizer = new ChipOrganizer(input);
                chipOrganizer.GenerateAllValidSolutions();
                actual = chipOrganizer.GetLongestSolution();
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
        public void ChipOrganizer_NoLinkingColors()
        {
            // Arrange
            var input = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Purple, Color.Green),
            };
            
            // Act
            var actual = new List<ColorChip>();
            Exception exception = null;
            try 
            {
                // Act
                var chipOrganizer = new ChipOrganizer(input);
                chipOrganizer.GenerateAllValidSolutions();
                actual = chipOrganizer.GetLongestSolution();
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