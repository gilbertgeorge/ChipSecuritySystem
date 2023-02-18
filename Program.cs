using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class Program
    {
        private static List<ColorChip> GetOrganizedChips(List<ColorChip> chips)
        {
            var chipOrganizer = new ChipOrganizer(chips);
            chipOrganizer.GenerateAllValidSolutions();
            var organizeColorChips = chipOrganizer.GetLongestSolution();
            return organizeColorChips;
        }
        
        private static List<ColorChip> FormatInputString(string[] args)
        {
            var colorChips = new List<ColorChip>();
            if (args.Length % 2 != 0)
            {
                throw new Exception(Constants.ErrorMessage);
            }

            for(int i=0; i<args.Length; i+=2)
            {
                var startColor = args[i].Replace("[", string.Empty).Replace(",", string.Empty);
                var endColor = args[i+1].Replace("]", string.Empty);
                var validColors = Enum.GetNames(typeof(Color)).ToList();
                if (validColors.Contains(startColor) && validColors.Contains(endColor))
                {
                    var startChip = (Color)Enum.Parse(typeof(Color), startColor);
                    var endChip = (Color)Enum.Parse(typeof(Color), endColor);
                    var colorChip = new ColorChip(startChip, endChip);
                    colorChips.Add(colorChip);
                }
            }
            
            return colorChips;
        }
        
        // [Expected program execution]
        // Each argument is a chip color in the format: "[startColor, endColor]" (without quotes, this represents the first two arguments)
        // If no commas are provided, the program will still function as expected e.g. [startColor endColor]
        // If an invalid color is provided, that chip will be ignored
        // If the number of arguments is odd, the program will print an error message
        // If the number of arguments is even, the program will print the longest valid solution
        // If there are no solutions, the program will print an error message
        // Example: .\ChipSecuritySystem.exe [Blue, Yellow] [Red, Green] [Yellow, Red] [Orange, Purple]
        // Output: [Blue, Yellow] [Yellow, Red] [Red, Green]
        public static void Main(string[] args)
        {
            try
            {
                var chips = FormatInputString(args);
                var organizeColorChips = GetOrganizedChips(chips);
                var result = organizeColorChips.Aggregate(string.Empty,
                    (current, colorChip) => current + $"[{colorChip.ToString()}] ");
                Console.WriteLine(result.Trim());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
