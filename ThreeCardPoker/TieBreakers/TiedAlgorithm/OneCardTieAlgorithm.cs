using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker.TieBreakers.TiedAlgorithm
{
    /// <summary>
    /// Solves a tie where only one card rank can be the same across hands
    /// </summary>
    public class OneCardTieAlgorithm : ITiedAlgorithm
    {
        public List<long> SolveTie(Player highScorePlayer, List<Player> players)
        {
            return players.Where(p => p.Hand.Score == highScorePlayer.Hand.Score).Select(p => p.Id).ToList();
        }
    }
}
