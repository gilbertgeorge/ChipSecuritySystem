using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class ChipOrganizer
    {
        private Color START_COLOR = Color.Blue;
        private Color END_COLOR = Color.Green;
        
        private readonly List<ColorChip> _colorChips;
        private readonly List<List<ColorChip>> _validSolutions = new List<List<ColorChip>>();

        public ChipOrganizer(List<ColorChip> colorChips)
        {
            _colorChips = colorChips;
        }
        
        public List<ColorChip> GetLongestSolution()
        {
            if (_validSolutions.Count == 0)
                throw new Exception(Constants.ErrorMessage);
            
            var longestValidList = new List<ColorChip>();
            foreach (var colorChipList in _validSolutions.Where(colorChipList =>
                         colorChipList.Count > longestValidList.Count))
            {
                longestValidList = colorChipList;
            }
            return longestValidList;
        }
        
        public void GenerateAllValidSolutions()
        {
            //Get all possible combinations of chips
            //Poor time complexity - other solutions might involve using graphs
            var powerSet = SetManipulation.GetPowerSet(_colorChips);
            foreach (var colorChipList in powerSet)
            {
                var permutations = SetManipulation.GetPermutations(colorChipList);
                foreach (var permutation in permutations)
                {
                    if (ValidateColorChipList(permutation.ToList()))
                        _validSolutions.Add(permutation.ToList());
                }
            }
        }
        
        private bool ValidateColorChipList(List<ColorChip> colorChipList)
        {
            if (colorChipList == null || colorChipList.Count == 0)
                return false;
            
            //Check if the first and last chip are valid
            if (colorChipList.FirstOrDefault().StartColor != START_COLOR ||
                colorChipList.LastOrDefault().EndColor != END_COLOR)
                return false;
            
            //Check if the chips are connected
            for (var i = 0; i < colorChipList.Count - 1; i++)
            {
                if (colorChipList[i].EndColor != colorChipList[i + 1].StartColor)
                    return false;
            }
            
            return true;
        }

    }
}