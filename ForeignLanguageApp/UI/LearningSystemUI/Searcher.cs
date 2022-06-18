using ForeignLanguageApp.Helpers;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.UI.LearningSystemUI
{
    public class Searcher
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly ITopicService topicService;

        public Searcher(IUserInterfaceProvider UIProvider, ITopicService topicService)
        {
            this.UIProvider = UIProvider;

            this.topicService = topicService;
        }

        private void SearchByDate(List<Entity> topics)
        {
            UIProvider.ClearPage();

            DateTime date = Parsers.ParseDate(UIProvider.GetString(Gui.AskForEnteringDate));

            List<Entity> foundTopics = SearchHelper.FindAllMatchingDate(topics, date);

            UIProvider.PrintLine();

            UIProvider.PrintLine(Gui.SearchResults);

            ShowEntities(foundTopics);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowEntities(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                UIProvider.PrintLine(entity.ToString());
            }
        }

        private void SearchByDateGap(List<Entity> topics)
        {
            UIProvider.ClearPage();

            DateTime startDate = Parsers.ParseDate(UIProvider.GetString(Gui.AskForEnteringStartDate));

            DateTime endDate = Parsers.ParseDate(UIProvider.GetString(Gui.AskForEnteringEndDate));

            List<Entity> foundTopics = SearchHelper.FindAllMatchingInterval(topics, startDate, endDate);

            UIProvider.PrintLine(Gui.SearchResults);

            ShowEntities(foundTopics);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void RenderSearchModelHeader()
        {
            foreach (string option in Gui.OptionsForSearchPageFirst)
            {
                UIProvider.PrintLine(option);
            }
        }

        private void RenderSecondSearchModeHeader()
        {
            foreach (string option in Gui.OptionsForSearchPageSecond)
            {
                UIProvider.PrintLine(option);
            }
        }

        public void HandlePageActivities(List<Topic> topics)
        {
            if (!(topics.Count > 0))
            {
                UIProvider.ClearPage();

                UIProvider.PrintLine(Gui.EmptyTopicsError);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                return;
            }

            while (true)
            {
                UIProvider.ClearPage();

                UIProvider.PrintLine(Gui.AskForSearchModel);

                RenderSearchModelHeader();

                List<Entity> entities;

                ConsoleKey firstButton = UIProvider.GetPushedButton();

                switch (firstButton)
                {
                    case ConsoleKey.D1:
                        entities = new List<Entity>(topics);
                        break;
                    case ConsoleKey.D2:
                        entities = topicService.GetAllTopicCards(topics);
                        break;
                    default:
                        return;
                }

                UIProvider.PrintLine(Gui.AskForSearchMode);

                RenderSecondSearchModeHeader();

                ConsoleKey anotherButton = UIProvider.GetPushedButton();

                try
                {
                    switch (anotherButton)
                    {
                        case ConsoleKey.D1:
                            SearchByDate(entities);
                            break;

                        case ConsoleKey.D2:
                            SearchByDateGap(entities);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    UIProvider.PrintLine(ex.Message);

                    UIProvider.RequestKeyPressToContinue();
                }
            }
        }
    }
}
