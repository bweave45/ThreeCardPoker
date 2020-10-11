using System;
using System.Collections.Generic;
using System.Text;
using ThreeCardPoker.Domain.Enums;

namespace ThreeCardPoker.Domain.Models
{
    public class Card
    {
        public CardSuit Suit { get; private set; }

        public CardRank Rank { get; private set; }

        public void SetSuit(char suitChar)
        {

            switch (char.ToLower(suitChar))
            {
                case 'c':
                    Suit = CardSuit.Clovers;
                    break;
                case 'd':
                    Suit = CardSuit.Diamonds;
                    break;
                case 'h':
                    Suit = CardSuit.Hearts;
                    break;
                case 's':
                    Suit = CardSuit.Spades;
                    break;
                default:
                    throw new ApplicationException("Invalid Suit Input.");
            }
        }

        public void SetRank(char rankChar)
        {
            switch (char.ToUpper(rankChar))
            {
                case '2':
                    Rank = CardRank.Two;
                    break;
                case '3':
                    Rank = CardRank.Three;
                    break;
                case '4':
                    Rank = CardRank.Four;
                    break;
                case '5':
                    Rank = CardRank.Five;
                    break;
                case '6':
                    Rank = CardRank.Six;
                    break;
                case '7':
                    Rank = CardRank.Seven;
                    break;
                case '8':
                    Rank = CardRank.Eight;
                    break;
                case '9':
                    Rank = CardRank.Nine;
                    break;
                case 'T':
                    Rank = CardRank.Ten;
                    break;
                case 'J':
                    Rank = CardRank.Jack;
                    break;
                case 'Q':
                    Rank = CardRank.Queen;
                    break;
                case 'K':
                    Rank = CardRank.King;
                    break;
                case 'A':
                    Rank = CardRank.Ace;
                    break;
                default:
                    throw new ApplicationException("Invalid Card Type Input.");
            }
        }
    }
}
