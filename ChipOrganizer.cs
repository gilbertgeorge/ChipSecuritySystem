using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChipSecuritySystem
{
    public class ChipOrganizer
    {
        private Color START_COLOR = Color.Blue;
        private Color END_COLOR = Color.Green;
        
        private readonly List<ColorChip> _colorChips = new List<ColorChip>();

        public ChipOrganizer(List<ColorChip> colorChips)
        {
            this._colorChips = colorChips;
        }

        public ChipOrganizer(string[] chipString)
        {
            try
            {
                foreach (var chip in chipString)
                {
                    var chipArray = chip.Split(',');
                    var startColor = chipArray[0].Replace("[", string.Empty);
                    var endColor = chipArray[1].Replace("]", string.Empty);

                    //Bypass invalid chips and proceed with remaining chips?
                    var startChip = (Color)System.Enum.Parse(typeof(Color), startColor);
                    var endChip = (Color)System.Enum.Parse(typeof(Color), endColor);

                    var colorChips = this._colorChips;
                    colorChips?.Add(new ColorChip(startChip, endChip));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(Constants.ErrorMessage);
            }
        }

        public void AddColorChip(ColorChip colorChip)
        {
            this._colorChips.Add(colorChip);
        }
        
        public List<ColorChip> GetColorChips()
        {
            return this._colorChips;
        }
        
        public string PrintColorChips()
        {
            var sb = new StringBuilder();
            foreach (var colorChip in this._colorChips)
            {
                sb.AppendLine(colorChip.ToString());
            }
            return sb.ToString();
        }
        
        public List<ColorChip> OrganizeColorChips()
        {
            try
            {
                var colorChips = this._colorChips;
                var largestColorChipCollection = new List<ColorChip>();
                var potentialStartChips = this._colorChips.Where(x => x.StartColor == START_COLOR).ToList();
                var potentialEndChips = this._colorChips.Where(x => x.EndColor == END_COLOR).ToList();
                
                if(potentialStartChips.Count == 0 || potentialEndChips.Count == 0)
                    throw new Exception(Constants.ErrorMessage);
                
                //Need to find another way to solve this problem -- need to refactor with checking visitation
                foreach (var potentialStartChip in potentialStartChips)
                {
                    var colorChipCollection = new List<ColorChip> { potentialStartChip };
                    var currentColorChip = potentialStartChip;
                    while (currentColorChip.EndColor != END_COLOR)
                    {
                        var nextColorChip = colorChips.FirstOrDefault(x => x.StartColor == currentColorChip.EndColor);
                        if (nextColorChip == null)
                            break;
                        colorChipCollection.Add(nextColorChip);
                        currentColorChip = nextColorChip;
                    }
                    if (colorChipCollection.Count > largestColorChipCollection.Count)
                        largestColorChipCollection = colorChipCollection;
                }

                if(largestColorChipCollection != null &&
                   (largestColorChipCollection.FirstOrDefault().StartColor != START_COLOR ||
                    largestColorChipCollection.LastOrDefault().EndColor != END_COLOR))
                    throw new Exception(Constants.ErrorMessage);
                
                return largestColorChipCollection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(Constants.ErrorMessage);
            }
        }

    }
}