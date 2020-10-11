using System;

namespace ThreeCardPoker.Domain.Models
{
    public class Player
    {
        public long Id { get; private set; }
        public Hand Hand { get; private set; }

        public Player(long id)
        {
            Id = id;
        }

        public void DealHand(Hand hand) 
        {
            Hand = hand;
        }
    }
}
