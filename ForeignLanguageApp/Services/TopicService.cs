using ForeignLanguageApp.Data;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System.Collections.Generic;

namespace ForeignLanguageApp.Services
{
    public class TopicService : ITopicService
    {
        private readonly IBaseRepository<Topic> repository;

        public TopicService(IBaseRepository<Topic> repository)
        {
            this.repository = repository;
        }

        public List<Topic> GetAllTopics()
        {
            return repository.GetAll();
        }

        public void AddTopic(Topic topic)
        {
            repository.Add(topic);
        }

        public void DeleteTopic(Topic topic)
        {
            repository.Delete(topic);
        }

        public void UpdateExistingTopicBy(Topic topic)
        {
            repository.Update(topic);
        }

        public List<Entity> GetAllTopicCards(List<Topic> topics)
        {
            List<Entity> cards = new List<Entity>();

            foreach (Topic topic in topics)
            {
                cards.AddRange(topic.Cards);
            }

            return cards;
        }

        public void SortByCreationTime(List<Topic> topics, OrderOfSorting orderOfSorting = OrderOfSorting.Ascending)
        {
            topics.Sort(delegate (Topic firstTopic, Topic secondTopic)
            {
                bool result = (firstTopic.CreationTime < secondTopic.CreationTime);

                if (result)
                {
                    return (int)orderOfSorting;
                }
                else
                {
                    return -(int)orderOfSorting;
                }
            });
        }

        public void SortTopicCardsByCreationTime(List<Card> cards, OrderOfSorting orderOfSorting = OrderOfSorting.Descending)
        {
            cards.Sort(delegate (Card firstCard, Card SecondCard)
            {
                bool result = (firstCard.CreationTime < SecondCard.CreationTime);

                if (result)
                {
                    return (int)orderOfSorting;
                }
                else
                {
                    return -(int)orderOfSorting;
                }
            });
        }
    }
}
