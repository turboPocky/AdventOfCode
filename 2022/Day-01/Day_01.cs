// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Pocky" file="Program.cs">
//   (c) 2022
// </copyright>
// <summary>
//   Advent of Code 2022, Day 1. 
//   https://adventofcode.com/2022/day/1
// </summary>
// --------------------------------------------------------------------------------------------------------------------/
// The Elves take turns writing down the number of Calories contained by the various meals, snacks, rations, etc.
// that they've brought with them, one item per line. Each Elf separates their own inventory from the previous Elf's
// inventory (if any) by a blank line.

using Advent.Common;

using Microsoft.Extensions.Logging;

namespace Advent.Day_01
{
    // ReSharper disable once UnusedType.Global
    public static class Day_01
    {
        private static ILogger Log { get; }

        static Day_01()
        {
            Log = Logging.SetupLogging(typeof(Day_01));
        }

        public static void Main(string[] args)
        {
            Puzzle_01();
            Puzzle_02();
        }

        // In case the Elves get hungry and need extra snacks, they need to know which Elf to ask:
        // they'd like to know how many Calories are being carried by the Elf carrying the most Calories.
        //
        // In [00-sample.txt], this is 24000 (carried by the fourth Elf).
        //
        // Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
        private static void Puzzle_01()
        {
            Log.LogInformation("Puzzle #1");
            Log.LogInformation("  Calculating highest calorie-packing elf from each input file");

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

                Log.LogInformation("  File {File}:", file);
                if (highestElfCalorieTotal > 0)
                {
                    Log.LogInformation("    Elf max calories: {HighestElfCalorieTotal}", highestElfCalorieTotal);
                }
                else
                {
                    Log.LogInformation("    Recorded 0 calories from all the elves. Was the file empty? Poor elves!");
                }
            }
        }

        // By the time you calculate the answer to the Elves' question, they've already realized that the Elf
        // carrying the most Calories of food might eventually run out of snacks.
        //
        // To avoid this unacceptable situation, the Elves would instead like to know the total Calories carried
        // by the top three Elves carrying the most Calories.That way, even if one of those Elves runs out of snacks,
        // they still have two backups.
        //  
        // In the [00-sample.txt] above, the top three Elves are the fourth Elf (with 24000 Calories), then the third
        // Elf(with 11000 Calories), then the fifth Elf(with 10000 Calories). The sum of the Calories carried by these
        // three elves is 45000.
        //
        // Find the top three Elves carrying the most Calories. How many Calories are those Elves carrying in total?
        private static void Puzzle_02()
        {
            Log.LogInformation("Puzzle #2");
            Log.LogInformation("  Calculating highest calorie-packing elf from each input file");

            foreach (var file in Directory.EnumerateFiles("./input"))
            {
                // this is a ranked accumulator array. not efficient for large topTierElves.length
                var topTierElves = new int[3];
                int currentElfCalorieTotal = default;
                int totalElves = default;

                foreach (var line in File.ReadLines(file))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        currentElfCalorieTotal += int.Parse(line);
                    }
                    else
                    {
                        totalElves++;

                        // first, find the weakest link 
                        int weakestTopTierElfIndex = 0;
                        for (var i = 0; i < topTierElves.Length; i++)
                        {
                            if (topTierElves[i] < topTierElves[weakestTopTierElfIndex])
                            {
                                weakestTopTierElfIndex = i;
                            }
                        }

                        // then, assert dominance
                        if (currentElfCalorieTotal > topTierElves[weakestTopTierElfIndex])
                        {
                            topTierElves[weakestTopTierElfIndex] = currentElfCalorieTotal;
                        }

                        currentElfCalorieTotal = 0;
                    }
                }

                Log.LogInformation("  File {File}:", file);
                Log.LogInformation("    Total elves read: {TotalElves}", totalElves);

                var total = topTierElves.Sum();

                if (total > 0)
                {
                    Log.LogInformation("    Top {Length} elves\' combined calories: {Total}", topTierElves.Length, total);
                }
                else
                {
                    Log.LogInformation("    Recorded 0 calories from all the elves. Was the file empty? Poor elves!");
                }
            }
        }
    }
}
