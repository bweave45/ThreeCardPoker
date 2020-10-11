using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker.TieBreakers.TiedAlgorithm
{
    /// <summary>
    /// Solves a tie where potentially two different card ranks could be the same across hands
    /// </summary>
    public class TwoCardTieAlgorithm : ITiedAlgorithm
    {
        public List<long> SolveTie(Player highScorePlayer, List<Player> players)
        {
            var tiedPlayers = players.Where(p => p.Hand.Score == highScorePlayer.Hand.Score)
                        .OrderByDescending(c => c.Hand.Cards.First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank).Rank);

            //Select the rank of the second highest card of all tied players, and add the players who also have that card next to their pair to the winners list
            var secondHighestRank = tiedPlayers.First().Hand.Cards.First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank).Rank;
            return players.Where(p => p.Hand.Cards.First(c => c.Rank != highScorePlayer.Hand.HighestContributingCard.Rank).Rank == secondHighestRank).Select(p => p.Id).ToList();
        }
    }
}
