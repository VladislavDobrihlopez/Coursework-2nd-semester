using ForeignLanguageApp.Data;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.UI.LearningSystemUI
{
    public class LearningSystem
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly ITopicService topicService;

        private readonly VocabularyTrainer vocabularyTrainer;

        private readonly Searcher searcher;

        public LearningSystem(IUserInterfaceProvider UIProvider, ITopicService topicService, VocabularyTrainer vocabularyTrainer, Searcher searcher)
        {
            this.UIProvider = UIProvider;

            this.topicService = topicService;

            this.vocabularyTrainer = vocabularyTrainer;

            this.searcher = searcher;
        }

        private void ShowTopicCards(Topic topic)
        {
            UIProvider.PrintLine(string.Format(Gui.TopicOutputForm, topic.Title, topic.CreationTime));

            for (int i = 0; i < topic.GetNumberOfCards(); i++)
            {
                UIProvider.PrintLine(string.Format(Gui.CardOutputForm, i + 1, topic.GetCardById(i)));
            }
        }

        public static void ShowAllTopics(List<Topic> topics, IUserInterfaceProvider UIProvider)
        {
            UIProvider.ClearPage();

            for (int i = 0; i < topics.Count; i++)
            {
                UIProvider.PrintLine(string.Format(Gui.TopicOutputFormWithoutCreationTime, i + 1, topics[i]));
            }
        }

        private void ShowAllTopicsIncludingCards(List<Topic> topics)
        {
            UIProvider.ClearPage();

            for (int i = 0; i < topics.Count; i++)
            {
                ShowTopicCards(topics[i]);

                UIProvider.PrintLine();
            }
        }

        private void ShowTopicContentPage(List<Topic> topics)
        {
            OrderOfSorting topicCreationTimeSortingOrder = OrderOfSorting.Ascending;

            OrderOfSorting cardCreationTimeSortingOrder = OrderOfSorting.Ascending;

            List<Topic> topicsToBeShown = new List<Topic>(topics);

            while (true)
            {
                UIProvider.ClearPage();

                topicService.SortByCreationTime(topicsToBeShown, topicCreationTimeSortingOrder);

                for(int i = 0; i < topicsToBeShown.Count; i++)
                {
                    topicService.SortTopicCardsByCreationTime(topicsToBeShown[i].Cards, cardCreationTimeSortingOrder);
                }

                ShowAllTopicsIncludingCards(topicsToBeShown);

                for (int i = 0; i < Gui.PresentingTopicsWithCardsOptions.Length; i++)
                {
                    switch(i)
                    {
                        case 0:
                            UIProvider.PrintLine(string.Format(Gui.PresentingTopicsWithCardsOptions[i], (topicCreationTimeSortingOrder == OrderOfSorting.Ascending) ? Gui.Ascending: Gui.Descending));
                            break;
                        case 1:
                            UIProvider.PrintLine(string.Format(Gui.PresentingTopicsWithCardsOptions[i], (cardCreationTimeSortingOrder == OrderOfSorting.Ascending) ? Gui.Ascending : Gui.Descending));
                            break;
                        case 2:
                            UIProvider.PrintLine(string.Format(Gui.PresentingTopicsWithCardsOptions[i]));
                            break;
                    }
                }

                ConsoleKey button = UIProvider.GetPushedButton();

                switch(button)
                {
                    case ConsoleKey.D1:
                        topicCreationTimeSortingOrder = (OrderOfSorting)Enum.Parse(typeof(OrderOfSorting), Convert.ToString(-(int)topicCreationTimeSortingOrder));
                        break;
                    case ConsoleKey.D2:
                        cardCreationTimeSortingOrder = (OrderOfSorting)Enum.Parse(typeof(OrderOfSorting), Convert.ToString(-(int)cardCreationTimeSortingOrder));
                        break;
                    case ConsoleKey.D3:
                        return;
                }
            }
        }

        private void ShowAddingNewTopic()
        {
            UIProvider.ClearPage();

            try
            {
                UIProvider.PrintLine(Gui.AskForTopicData);

                string topicTitle = UIProvider.GetString().Trim();

                topicService.AddTopic(new Topic(topicTitle));

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (Exception ex)
            {
                UIProvider.PrintLine(ex.Message);
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowFrontSideEditing(Card card)
        {
            UIProvider.PrintLine(Gui.AskForFrontSideOfCard);

            string frontSide = UIProvider.GetString().Trim();

            try
            {
                card.Update(frontSide, card.BackSide);

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (ArgumentException ex)
            {
                UIProvider.PrintLine(ex.Message);             
            }
        }

        private void ShowBackSideEditing(Card card)
        {
            UIProvider.PrintLine(Gui.AskForBackSideOfCard);

            string backSide = UIProvider.GetString().Trim();

            try
            {
                card.Update(card.FrontSide, backSide);

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (ArgumentException ex)
            {
                UIProvider.PrintLine(ex.Message);
            }
        }

        private void ShowCardToBeEdited(Card card)
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(card.FrontSide + " - " + card.BackSide);

            foreach(string option in Gui.CardEditingOptions)
            {
                UIProvider.PrintLine(option);
            }

            ConsoleKey button = UIProvider.GetPushedButton();

            switch (button)
            {
                case ConsoleKey.D1:
                    ShowFrontSideEditing(card);
                    break;
                case ConsoleKey.D2:
                    ShowBackSideEditing(card);
                    break;
            }
        }

        private void ShowCardAdding(Topic topic)
        {
            UIProvider.ClearPage();

            string frontSide = UIProvider.GetString(Gui.AskForFrontSideOfCard).Trim();

            string backSide = UIProvider.GetString(Gui.AskForBackSideOfCard).Trim();

            try
            {
                Card card = new Card(frontSide, backSide);

                topic.AddCard(card);

                topicService.UpdateExistingTopicBy(topic);

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch(ArgumentException ex)
            {
                UIProvider.PrintLine(ex.Message);
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowCardEditing(Topic topic)
        {
            UIProvider.ClearPage();

            if (!(topic.GetNumberOfCards() > 0))
            {
                UIProvider.PrintLine(Gui.EmptyTopicCardsError);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                return;
            }

            ShowTopicCards(topic);

            int cardId = GetValidIndex(topic.GetNumberOfCards(), Gui.AskForEnteringCardNumber);

            ShowCardToBeEdited(topic.GetCardById(cardId));

            topicService.UpdateExistingTopicBy(topic);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowCardDeletion(Topic topic)
        {
            UIProvider.ClearPage();

            int numberOfCards = topic.GetNumberOfCards();

            if (!(numberOfCards > 0))
            {
                UIProvider.PrintLine(Gui.EmptyTopicCardsError);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                return;
            }

            ShowTopicCards(topic);

            int cardId = GetValidIndex(numberOfCards, Gui.AskForEnteringCardNumber);

            UIProvider.PrintLine(Gui.CardDeletionWarning);

            ConsoleKey button = UIProvider.GetPushedButton();

            switch (button)
            {
                case ConsoleKey.D1:
                    topic.RemoveCard(cardId);
                    topicService.UpdateExistingTopicBy(topic);
                    break;
                default:
                    return;
            }

            UIProvider.PrintLine(Gui.Succeed);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowTopicEditingPage(List<Topic> topics)
        {
            bool isAlive = true;

            while (isAlive)
            {
                UIProvider.ClearPage();

                if (!(topics.Count > 0))
                {
                    UIProvider.PrintLine(Gui.EmptyTopicsError);

                    UIProvider.PrintLine(Gui.PressAnyKey);

                    UIProvider.RequestKeyPressToContinue();

                    return;
                }

                ShowAllTopics(topics, UIProvider);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                int topicId = GetValidIndex(topics.Count, Gui.AskForEnteringTopicNumber);

                Topic topic = topics[topicId];

                ShowTopicCards(topic);

                UIProvider.PrintLine();

                foreach (string option in Gui.TopicEditingOptions)
                {
                    UIProvider.PrintLine(option);
                }

                ConsoleKey button = UIProvider.GetPushedButton();

                switch (button)
                {
                    case ConsoleKey.D1:
                        ShowCardAdding(topic);
                        break;
                    case ConsoleKey.D2:
                        ShowCardEditing(topic);
                        break;
                    case ConsoleKey.D3:
                        ShowCardDeletion(topic);
                        break;
                    case ConsoleKey.D4:
                        isAlive = false;
                        break;
                }
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private int GetValidIndex(int numberOfItems, string message)
        {
            int itemId;

            do
            {
                itemId = UIProvider.GetInteger(message) - 1;
            }
            while (!Validators.IsIndexValid(itemId, numberOfItems));

            return itemId;
        }

        private void ShowDeletionTopic(List<Topic> topics)
        {
            UIProvider.ClearPage();

            if (!(topics.Count > 0))
            {
                UIProvider.PrintLine(Gui.EmptyTopicsError);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                return;
            }

            ShowAllTopics(topics, UIProvider);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();

            int topicId = GetValidIndex(topics.Count, Gui.AskForEnteringTopicNumber);

            UIProvider.PrintLine(Gui.TopicDeletionWarning);

            ConsoleKey button = UIProvider.GetPushedButton();

            switch (button)
            {
                case ConsoleKey.D1:
                    topicService.DeleteTopic(topics[topicId]);
                    break;
                default:
                    return;
            }

            UIProvider.PrintLine(Gui.Succeed);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void RenderHeader()
        {
            foreach (string option in Gui.OptionsForVocabularyTrainingPage)
            {
                UIProvider.PrintLine(option);
            }
        }

        public void HandlePageActivities()
        {
            List<Topic> topics = topicService.GetAllTopics();

            while (true)
            {
                UIProvider.ClearPage();

                UIProvider.PrintLine(Gui.UserMenuHeader);

                RenderHeader();

                ConsoleKey button = UIProvider.GetPushedButton();

                switch (button)
                {
                    case ConsoleKey.D1:
                        ShowTopicContentPage(topics);
                        break;
                    case ConsoleKey.D2:
                        searcher.HandlePageActivities(topics);
                        break;
                    case ConsoleKey.D3:
                        ShowAddingNewTopic();
                        topics = topicService.GetAllTopics();
                        break;
                    case ConsoleKey.D4:
                        ShowTopicEditingPage(topics);
                        topics = topicService.GetAllTopics();
                        break;
                    case ConsoleKey.D5:
                        ShowDeletionTopic(topics);
                        topics = topicService.GetAllTopics();
                        break;
                    case ConsoleKey.D6:
                        vocabularyTrainer.StartRememberEverythingExercise(topics);
                        break;
                    case ConsoleKey.D7:
                        vocabularyTrainer.StartFieldOfDreamsExercise(topics);
                        break;
                    case ConsoleKey.D8:
                        return;
                }
            }
        }
    }
}
