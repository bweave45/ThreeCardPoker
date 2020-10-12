using System.Collections.Generic;
using System.Linq;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker
{
    public static class ThreeCardPokerMapper
    {
        public static Player ToPlayer(this string playerInputData) 
        {
            var inputStrings = playerInputData.Split().ToList();

            var player = new Player(long.Parse(inputStrings[0]));

            inputStrings.RemoveAt(0);


            var hand = inputStrings.ToHand();

            player.DealHand(hand);

            return player;
        }

        public static Hand ToHand(this List<string> cardStrings)
        {
            var cards = cardStrings.Select(s => s.ToCard()).ToList();

            var hand = new Hand(cards);

            return hand;
        }

        public static Card ToCard(this string cardString)
        {
            var card = new Card();

            card.SetRank(cardString[0]);
            card.SetSuit(cardString[1]);

            return card;
        }
    }
}
