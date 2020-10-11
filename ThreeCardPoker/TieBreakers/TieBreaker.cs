using System.Collections.Generic;
using ThreeCardPoker.Domain.Models;
using ThreeCardPoker.TieBreakers.TiedAlgorithm;

namespace ThreeCardPoker.TieBreakers
{
    public class TieBreaker
    {
        private ITiedAlgorithm algorithm;

        public List<long> BreakTie(Player highestScoringPlayer, List<Player> players) 
        {
            return algorithm.SolveTie(highestScoringPlayer, players);
        }

        public void SetTieBreaker(ITiedAlgorithm tiedAlgorithm) 
        {
            algorithm = tiedAlgorithm;
        }
    }
}
