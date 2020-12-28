﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGames.Models.Blackjack
{
    public class Player
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public string ScoreDisplay
        {
            get
            {
                var score = Score(true);
                if (score > 21)
                    return "Busted!";
                else return score.ToString();
            }
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int Score(bool onlyVisible = false)
        {
            var cards = Cards;

            if(onlyVisible)
            {
                cards = Cards.Where(x => x.IsVisible).ToList();
            }

            //If the sum total of all cards is <= 21, return that value
            var totalScore = cards.Sum(x => x.Score);
            if (totalScore <= 21) return totalScore;

            //If there are no Aces and the sum total is > 21, we are busted
            bool hasAces = cards.Any(x => x.Value == Enums.CardValue.Ace);
            if (!hasAces && totalScore > 21) return totalScore;

            //By this point, the sum will be greater than 21 if all Aces are worth 11
            //So, make each Ace worth 1, until the sum is <= 21
            //There can only ever be four aces in one hand
            var acesCount = cards.Where(x => x.Value == Enums.CardValue.Ace).Count();
            var acesScore = cards.Sum(x => x.Score);

            while (acesCount > 0)
            {
                acesCount -= 1;
                acesScore -= 10;

                if (acesScore <= 21) return acesScore;
            }

            //If the score never gets returned, we are busted
            return cards.Sum(x => x.Score);
        }

        public bool IsBusted => Score() > 21;

        public void Reveal()
        {
            Cards.ForEach(x => x.IsVisible = true);
        }

        public void Clear()
        {
            Cards = new List<Card>();
        }
    }
}