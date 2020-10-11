using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeCardPoker.Domain.Enums
{
    public enum HandType
    {
        StraightFlush = 600,
        ThreeOfAKind = 500,
        Straight = 400,
        Flush = 300,
        Pair = 200,
        HighCard = 100
    }
}
