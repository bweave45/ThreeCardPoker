using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThreeCardPoker.Domain.Models;

namespace ThreeCardPoker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("After entering the game's data, on a new line press 'ctrl' and 'z', then 'enter'.");

            var players = new List<Player>();

            using (var sr = new StreamReader(Console.OpenStandardInput()))
            {
                var cardInputs = sr.ReadToEnd().Split("\r\n").ToList();

                cardInputs.RemoveAt(cardInputs.Count - 1);
                cardInputs.RemoveAt(0);

                players = cardInputs.Select(c => c.ToPlayer()).ToList();
            }

            var winners = ThreeCardPokerScorer.ScoreGame(players);

            winners.OrderBy(w => w).ToList().ForEach(w => Console.Write("{0} ", w));
        }
    }
}
