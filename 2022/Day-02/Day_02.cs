// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Pocky" file="Program.cs">
//   (c) 2022
// </copyright>
// <summary>
//   Advent of Code 2022, Day 1.
//   https://adventofcode.com/2022/day/1
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// Appreciative of your help yesterday, one Elf gives you an encrypted strategy guide(your puzzle input) that they say
// will be sure to help you win. "The first column is what your opponent is going to play: A for Rock, B for Paper,
// and C for Scissors. The second column--" Suddenly, the Elf is called away to help with someone's tent.
//
// The second column, you reason, must be what you should play in response: X for Rock, Y for Paper,
// and Z for Scissors. Winning every time would be suspicious, so the responses must have been carefully chosen.
// 
// The winner of the whole tournament is the player with the highest score. Your total score is the sum of your scores
// for each round. The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and
// 3 for Scissors) plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).
// 

using Advent.Common;

using Microsoft.Extensions.Logging;

namespace Advent.Day_02
{
    // ReSharper disable once UnusedType.Global
    public static class Day_02
    {
        private static ILogger Log { get; }

        static Day_02()
        {
            Log = Logging.SetupLogging(typeof(Day_02));
        }

        public static void Main(string[] args)
        {
            new Puzzle_01().Run();
            // Puzzle_02();
        }
    }
}
