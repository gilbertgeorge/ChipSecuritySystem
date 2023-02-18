using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    public class Program
    {
        public static List<ColorChip> GetOrganizedChips(string[] chips)
        {
            var chipOrganizer = new ChipOrganizer(chips);
            chipOrganizer.GenerateAllValidSolutions();
            var organizeColorChips = chipOrganizer.GetLongestSolution();
            return organizeColorChips;
        }
        
        static void Main(string[] args)
        {
            var organizeColorChips = GetOrganizedChips(args);
            foreach (var colorChip in organizeColorChips)
            {
                Console.WriteLine(colorChip.ToString());
            }
        }
    }
}
