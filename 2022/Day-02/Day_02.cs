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
            Puzzle_01();
            Puzzle_02();
        }

        // Since you can't be sure if the Elf is trying to help you or trick you, you should calculate
        // the score you would get if you were to follow the strategy guide.
        //
        // This [00-sample.txt] strategy guide predicts and recommends the following:
        //
        // In the first round, your opponent will choose Rock(A), and you should choose Paper(Y).
        // This ends in a win for you with a score of 8 (2 because you chose Paper + 6 because you won).
        //
        // In the second round, your opponent will choose Paper(B), and you should choose Rock(X). This
        // ends in a loss for you with a score of 1 (1 + 0). The third round is a draw with both players
        // choosing Scissors, giving you a score of 3 + 3 = 6.
        //
        // In this example, if you were to follow the strategy guide, you would get a total score of 15
        // (8 + 1 + 6).
        // 
        // What would your total score be if everything goes exactly according to [01-real.txt]?
        private static void Puzzle_01()
        {
            Log.LogInformation("Puzzle #1");
            // new up puzzle1 and .Run() it instead of this 

            foreach (var file in Directory.EnumerateFiles("./input"))
            {
                Log.LogInformation(" File {File}:", file);

                var totalScore = 0;

                foreach (var line in File.ReadLines(file))
                {
                    var round = line.Split(' ');
                    var elfMove = Move.GetMove(round[0]);
                    var playerMove = Move.GetMove(round[1]);
                    var outcome = Helpers.GetOutcome(elfMove, playerMove);

                    var roundScore = (int)outcome + (int)playerMove.MoveShape;
                    totalScore += roundScore;

                    Log.LogDebug(
                        "    Elf played {ElfMove}, Player played {PlayerMove}. Score: {RoundScore}, Total: {TotalScore}",
                        elfMove.MoveShape,
                        playerMove.MoveShape,
                        roundScore,
                        totalScore);
                }

                Log.LogInformation("   Total score: {TotalScore}", totalScore);
            }
        }

        private static void Puzzle_02()
        {
            Log.LogInformation("Puzzle #2");
        }

        private static Outcome GetOutcome(Move elfMove, Move playerMove)
        {
            var outcome = Outcome.Draw;

            if (elfMove.LosesAgainst == playerMove.MoveShape)
            {
                outcome = Outcome.PlayerWins;
            }
            else if (elfMove.WinsAgainst == playerMove.MoveShape)
            {
                outcome = Outcome.ElfWins;
            }

            return outcome;
        }
    }

    public enum MoveShape
    {
        Nothing = 0,
        Rock,
        Paper,
        Scissors
    }

    public enum Outcome
    {
        ElfWins = 0,
        Draw = 3,
        PlayerWins = 6
    }

    // ReSharper disable once StyleCop.SA1402
    public static class Helpers
    {
        public static Outcome GetOutcome(Move elfMove, Move playerMove)
        {
            var outcome = Outcome.Draw;

            if (elfMove.LosesAgainst == playerMove.MoveShape)
            {
                outcome = Outcome.PlayerWins;
            }
            else if (elfMove.WinsAgainst == playerMove.MoveShape)
            {
                outcome = Outcome.ElfWins;
            }

            return outcome;
        }
    }


    public class Move
    {
        public static Dictionary<MoveShape, Move> AllowedMoves { get; private set; }

        public MoveShape MoveShape { get; set; }

        public MoveShape WinsAgainst { get; set; }

        public MoveShape LosesAgainst { get; set; }

        public static Move GetMove(string input)
        {
            switch (input)
            {
                case "A":
                case "X":
                    return AllowedMoves[MoveShape.Rock];
                case "B":
                case "Y":
                    return AllowedMoves[MoveShape.Paper];
                case "C":
                case "Z":
                    return AllowedMoves[MoveShape.Scissors];
                default:
                    return AllowedMoves[MoveShape.Nothing];
            }
        }

        static Move()
        {
            AllowedMoves = new Dictionary<MoveShape, Move>
                {
                    {
                        MoveShape.Rock,
                        new Move
                            {
                                MoveShape = MoveShape.Rock,
                                WinsAgainst = MoveShape.Scissors,
                                LosesAgainst = MoveShape.Paper
                            }
                    },
                    {
                        MoveShape.Paper,
                        new Move
                            {
                                MoveShape = MoveShape.Paper,
                                WinsAgainst = MoveShape.Rock,
                                LosesAgainst = MoveShape.Scissors
                            }
                    },
                    {
                        MoveShape.Scissors,
                        new Move
                            {
                                MoveShape = MoveShape.Scissors,
                                WinsAgainst = MoveShape.Paper,
                                LosesAgainst = MoveShape.Rock
                            }
                    }
                };
        }
    }
}
