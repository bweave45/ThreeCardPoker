using System;
using System.Collections.Generic;
using System.Linq;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker.TieBreakers.TiedAlgorithm
{
    /// <summary>
    /// Solves a tie where potentially all three different card ranks could be the same across hands
    /// </summary>
    public class ThreeCardTieAlgorithm : ITiedAlgorithm
    {
        public List<long> SolveTie(Player highScorePlayer, List<Player> players)
        {
            var winningPlayerIds = new List<long>(); 

            //Tied players ordered descending by card rank, of their highest card rank that is not the rank of their highest contributing card. 
            var tiedPlayers = players.Where(p => p.Hand.Score == highScorePlayer.Hand.Score)
                        .OrderByDescending(c => c.Hand.Cards.First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank).Rank);

            var groupedTwoCardTiePlayers = tiedPlayers.GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank).First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank).Rank);

            if (groupedTwoCardTiePlayers.First().Count() == 1)
                winningPlayerIds.Add(groupedTwoCardTiePlayers.First().First().Id);
            else
            {
                var groupedThreeCardTiePlayers = groupedTwoCardTiePlayers.First()
                    .GroupBy(p => p.Hand.Cards.OrderByDescending(c => c.Rank)
                    .First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank && c.Rank != groupedTwoCardTiePlayers.First().Key).Rank);

                winningPlayerIds.AddRange(groupedThreeCardTiePlayers.First().Select(p => p.Id));
            }

            return winningPlayerIds;
        }
    }
}
