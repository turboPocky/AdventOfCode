// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Pocky" file="Program.cs">
// </copyright>
// <summary>
//   Advent of Code 2022, Day 1, Puzzle 1. 
//   https://adventofcode.com/2022/day/1
// </summary>
// --------------------------------------------------------------------------------------------------------------------/
// The Elves take turns writing down the number of Calories contained by the various meals, snacks, rations, etc.
// that they've brought with them, one item per line. Each Elf separates their own inventory from the previous Elf's
// inventory (if any) by a blank line.
//
// In case the Elves get hungry and need extra snacks, they need to know which Elf to ask:
// they'd like to know how many Calories are being carried by the Elf carrying the most Calories.
//
// In [00-sample.txt], this is 24000 (carried by the fourth Elf).
// Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?

namespace Advent.Day_01
{
    // ReSharper disable once UnusedType.Global
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Calculating highest calorie-packing elf from each input file.");

            foreach (var file in Directory.EnumerateFiles("./input"))
            {
                int highestElfCalorieTotal = default;
                int currentElfCalorieTotal = default;

                foreach (var line in File.ReadLines(file))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        currentElfCalorieTotal += int.Parse(line);
                    }
                    else
                    {
                        if (currentElfCalorieTotal > highestElfCalorieTotal)
                        {
                            highestElfCalorieTotal = currentElfCalorieTotal;
                        }

                        currentElfCalorieTotal = 0;
                    }
                }

                Console.Write($"File {file}:{System.Environment.NewLine}\t");
                if (highestElfCalorieTotal > 0)
                {
                    Console.WriteLine($"Elf max calories: {highestElfCalorieTotal}.");
                }
                else
                {
                    Console.WriteLine("Recorded 0 calories from all the elves. Was the file empty? Poor elves!");
                }
            }
        }
    }
}
