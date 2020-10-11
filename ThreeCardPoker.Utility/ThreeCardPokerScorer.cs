using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeCardPoker.Domain.Models;

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
                //Tied players ordered descending by card rank, which is not the card rank of the highest card of the highest scoring hand. 
                var tiedPlayers = players.Where(p => p.Hand.Score == topScoringPlayer.Hand.Score)
                            .OrderByDescending(c => c.Hand.Cards.First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank).Rank);

                switch (topScoringPlayer.Hand.Type)
                {
                    case Domain.Enums.HandType.StraightFlush:
                        winners.AddRange(players.Where(p => p.Hand.Score == topScoringPlayer.Hand.Score).Select(p => p.Id));
                        break;
                    case Domain.Enums.HandType.ThreeOfAKind:
                        winners.AddRange(players.Where(p => p.Hand.Score == topScoringPlayer.Hand.Score).Select(p => p.Id));
                        break;
                    case Domain.Enums.HandType.Straight:
                        winners.AddRange(players.Where(p => p.Hand.Score == topScoringPlayer.Hand.Score).Select(p => p.Id));
                        break;
                    case Domain.Enums.HandType.Flush:
                        var groupedPlayerss = tiedPlayers.GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank).First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank).Rank);

                        if (groupedPlayerss.First().Count() == 1)
                            winners.Add(groupedPlayerss.First().First().Id);
                        else
                        {
                            var test2122 = groupedPlayerss.First()
                                .GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank)
                                .First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank && c.Rank != groupedPlayerss.First().Key).Rank);

                            winners.AddRange(test2122.First().Select(p => p.Id));
                        }

                        break;
                    case Domain.Enums.HandType.Pair:
                        //Select the rank of the second highest card of all tied players, and add the players who also have that card next to their pair to the winners list
                        var secondHighestRank = tiedPlayers.First().Hand.Cards.First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank).Rank;
                        winners.AddRange(players.Where(p => p.Hand.Cards.First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank).Rank == secondHighestRank).Select(p => p.Id));
                        break;
                    case Domain.Enums.HandType.HighCard:
                        //Group tied players by their second highest card
                        var groupedPlayers = tiedPlayers.GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank).First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank).Rank);

                        if (groupedPlayers.First().Count() == 1)
                            winners.Add(groupedPlayers.First().First().Id);
                        else
                        {
                            var test2122 = groupedPlayers.First()
                                .GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank)
                                .First(c => c.Rank != topScoringPlayer.Hand.HighestContributingCard.Rank && c.Rank != groupedPlayers.First().Key).Rank);

                            winners.AddRange(test2122.First().Select(p => p.Id));
                        }

                        break;
                    default:
                        break;
                }
            }

            return winners;
        }
    }
}
