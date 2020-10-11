using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeCardPoker.Domain.Enums;

namespace ThreeCardPoker.Domain.Models
{
    public class Hand
    {
        public List<Card> Cards { get; private set; }
        public HandType Type { get; private set; }
        public Card HighestContributingCard { get; private set; }
        public int Score { get { return (int)HighestContributingCard?.Rank + (int)Type; }}

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }

        public void ScoreHand()
        {
            if (HasStraightFlush())
            {
                Type = HandType.StraightFlush;

                HighestContributingCard = IsLowAceStraight() ?
                    Cards.First(c => c.Rank == CardRank.Three) :
                    Cards.OrderByDescending(c => c.Rank).First();

            }
            else if (HasThreeOfKind())
            {
                Type = HandType.ThreeOfAKind;
                HighestContributingCard = Cards.First();
            }
            else if (HasStraight())
            {
                Type = HandType.Straight;

                HighestContributingCard = IsLowAceStraight() ?
                    Cards.First(c => c.Rank == CardRank.Three) :
                    Cards.OrderByDescending(c => c.Rank).First();
            }
            else if (HasFlush())
            {
                Type = HandType.Flush;
                HighestContributingCard = Cards.OrderByDescending(c => c.Rank).First();
            }
            else if (HasPair())
            {
                Type = HandType.Pair;
                var groupedCards = Cards.GroupBy(c => c.Rank);
                HighestContributingCard = groupedCards.First(g => g.Count() == 2).Select(c => c).First();
            }
            else 
            {
                Type = HandType.HighCard;
                HighestContributingCard = Cards.OrderByDescending(c => c.Rank).First();
            }
        }

        private bool HasStraightFlush() 
        {
            return HasStraight() && HasFlush();
        }

        private bool HasStraight()
        {
            var orderedCards = Cards.OrderBy(c => c.Rank).ToList();

            var hasStraight = false;

            if (IsLowAceStraight())
            {
                hasStraight = true;
            }
            else 
            {
                hasStraight = (int)orderedCards[1].Rank - 1 == (int)orderedCards[0].Rank && (int)orderedCards[1].Rank + 1 == (int)orderedCards[2].Rank;
            }

            return hasStraight;
        }

        private bool HasFlush()
        {
            return Cards.GroupBy(c => c.Suit).Count() == 1;
        }

        private bool HasThreeOfKind()
        {
            return Cards.GroupBy(c => c.Rank).Count() == 1;
        }

        private bool HasPair()
        {
            return Cards.GroupBy(c => c.Rank).Count() == 2;
        }

        private bool IsLowAceStraight() 
        {
            return Cards.Any(c => c.Rank == CardRank.Ace) && Cards.Any(c => c.Rank == CardRank.Two) && Cards.Any(c => c.Rank == CardRank.Three);
        }
    }
}
