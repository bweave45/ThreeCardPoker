using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeCardPoker.Domain.Models;
using ThreeCardPoker.TieBreakers;
using ThreeCardPoker.TieBreakers.TiedAlgorithm;

namespace ThreeCardPoker
{
    public static class ThreeCardPokerScorer
    {
        public static List<long> ScoreGame(List<Player> players)
        {
            var winners = new List<long>();

            foreach (var player in players)
            {
                player.Hand.ScoreHand();
            }

            var topScoringPlayer = players.OrderByDescending(p => p.Hand.Score).First();
            var otherPlayers = players.Where(p => p.Id != topScoringPlayer.Id);

            if (players.Count(p => p.Hand.Score == topScoringPlayer.Hand.Score) == 1)
            {
                winners.Add(topScoringPlayer.Id);
            }
            else
            {
                var tieBreaker = new TieBreaker();

                switch (topScoringPlayer.Hand.Type)
                {
                    case Domain.Enums.HandType.StraightFlush:
                        tieBreaker.SetTieBreaker(new OneCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    case Domain.Enums.HandType.ThreeOfAKind:
                        tieBreaker.SetTieBreaker(new OneCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    case Domain.Enums.HandType.Straight:
                        tieBreaker.SetTieBreaker(new OneCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    case Domain.Enums.HandType.Flush:
                        tieBreaker.SetTieBreaker(new ThreeCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    case Domain.Enums.HandType.Pair:
                        tieBreaker.SetTieBreaker(new TwoCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    case Domain.Enums.HandType.HighCard:

                        tieBreaker.SetTieBreaker(new TwoCardTieAlgorithm());
                        winners.AddRange(tieBreaker.BreakTie(topScoringPlayer, players));
                        break;
                    default:
                        break;
                }
            }
            return winners;
        }
    }
}
