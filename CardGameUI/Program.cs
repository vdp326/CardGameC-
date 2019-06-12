using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameUI
{
    partial class Program
    {
        static void Main(string[] args)
        {
            BlackjackDeck deck = new BlackjackDeck();

            var hand = deck.DealCards();

            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Value.ToString()} {card.Suit.ToString()} ");
            }

            Console.ReadLine();
        }

        public abstract class Deck
        {
            protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
            protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
            protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();

            protected void CreateDeck()
            {
                fullDeck.Clear(); //clear the deck

                //create a deck with 4 suits and 13 cards
                for (int suit = 0;  suit < 4;  suit++)
                {
                    for (int val = 0; val < 13; val++)
                    {
                        fullDeck.Add(new PlayingCardModel { Suit = (CardSuit)suit, Value = (CardValue)val });
                    }
                }
            }

            public virtual void ShuffleDeck()
            {
                //var rand = new Random();
                //var randomList = imagesEasy.OrderBy(x => rand.Next()).ToList();
                var rnd = new Random();
                drawPile = fullDeck.OrderBy(x => rnd.Next()).ToList(); //order by sort the list by random numbers, convert to a list
            }

            public abstract List<PlayingCardModel> DealCards();
            
            //take 1 card from the drawPile, remove it and return to the user
            protected virtual PlayingCardModel DrawOneCard()
            {
                PlayingCardModel output = drawPile.Take(1).First(); //Take() take a card but does not remove
                drawPile.Remove(output);
                return output;
            }
            
        }

        public class BlackjackDeck : Deck
        {
            public BlackjackDeck()
            {
                CreateDeck();
                ShuffleDeck();
            }
            public override List<PlayingCardModel> DealCards()
            {
                List<PlayingCardModel> output = new List<PlayingCardModel>();

                for (int i = 0; i < 2; i++)
                {
                    output.Add(DrawOneCard());
                }

                return output; //player's hand
            }

            public PlayingCardModel RequestCard()
            {
                return DrawOneCard();
            }
        }

        public class PokerDeck : Deck
        {
            public PokerDeck()
            {
                CreateDeck();
                ShuffleDeck();
            }   
            public override List<PlayingCardModel> DealCards()
            {
                List<PlayingCardModel> output = new List<PlayingCardModel>();

                for (int i = 0; i < 5; i++)
                {
                    output.Add(DrawOneCard());
                }

                return output; //player's hand
             
            }

            public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
            {
                List<PlayingCardModel> output = new List<PlayingCardModel>();

                foreach (var card in cardsToDiscard)
                {
                    output.Add(DrawOneCard());
                    discardPile.Add(card); 
                }

                return output;
            }
        }
    }
}
