using ForeignLanguageApp.Data;
using ForeignLanguageApp.Models;
using System.Collections.Generic;

namespace ForeignLanguageApp.Interfaces
{
    public interface ITopicService
    {
        public List<Topic> GetAllTopics();

        public void AddTopic(Topic topic);

        public void DeleteTopic(Topic topic);

        public void UpdateExistingTopicBy(Topic topic);

        public List<Entity> GetAllTopicCards(List<Topic> topics);

        public void SortByCreationTime(List<Topic> topics, OrderOfSorting orderOfSorting = OrderOfSorting.Ascending);

        public void SortTopicCardsByCreationTime(List<Card> cards, OrderOfSorting orderOfSorting = OrderOfSorting.Descending);
    }
}
