using System;
using System.Collections.Generic;
using System.Text;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker.TieBreakers.TiedAlgorithm
{
    public interface ITiedAlgorithm
    {
        List<long> SolveTie(Player highScorePlayer, List<Player> players);
    }
}
