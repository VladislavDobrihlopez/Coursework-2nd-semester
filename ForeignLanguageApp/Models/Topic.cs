using ForeignLanguageApp.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.Models
{
    public class Topic : Entity
    {
        private List<Card> cards;

        private string title;

        public int Id { get; }

        public string Title 
        { 
            get { return title; }
            private set
            {
                if (!(value.Length >= 4))
                {
                    throw new ArgumentException(Gui.TopicNotMeetRequirementsError);
                }

                title = value;
            }
        }

        public List<Card> Cards 
        { 
            get
            {
                return cards;
            }
            private set
            {
                if (value == null)
                {
                    cards = new List<Card>();
                }
                else
                {
                    cards = value;
                }
            }
        }

        [JsonConstructor]
        public Topic(string title, List<Card> cards, int id, DateTime creationTime) : base(creationTime)
        {
            Title = title;

            Cards = cards;

            Id = id;
        }

        public Topic(string title) : this(title, new List<Card>(), 0, DateTime.Now)
        {}

        public void RemoveCard(int cardId)
        {
            Cards.RemoveAt(cardId);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int GetNumberOfCards()
        {
            return cards.Count;
        }

        public Card GetCardById(int id)
        {
            return cards[id];
        }

        public override string ToString()
        {
            return $"{title}";
        }
    }
}
