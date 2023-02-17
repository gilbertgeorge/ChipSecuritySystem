using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class Program
    {
        public static List<ColorChipExtended> GetOrganizedChips(string[] chips)
        {
            var chipOrganizer = new ChipOrganizer(chips);
            var organizeColorChips = chipOrganizer.OrganizeColorChips();
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
