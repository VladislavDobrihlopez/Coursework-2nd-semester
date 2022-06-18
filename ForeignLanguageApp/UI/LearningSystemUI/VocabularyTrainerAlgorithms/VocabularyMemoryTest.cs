using ForeignLanguageApp.Helpers;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.UI.LearningSystemUI.VocabularyTrainerAlgorithms
{
    public class VocabularyMemoryTest : TestBase, TrainerBehavior
    {
        public VocabularyMemoryTest(IUserInterfaceProvider UIProvider) : base(UIProvider)
        { }

        public void StartTraining(List<Topic> topics)
        {
            UIProvider.ClearPage();

            LearningSystem.ShowAllTopics(topics, UIProvider); 

            int topicId;

            do
            {
                topicId = UIProvider.GetInteger(Gui.AskForEnteringTopicNumber) - 1;
            }
            while (!Validators.IsIndexValid(topicId, topics.Count));

            Topic topic = topics[topicId];

            StartWordMemoryTraining(RandomizeHelper.GetShuffledItems<Card>(topic.Cards));
        }

        public void StartWordMemoryTraining(List<Card> cards)
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(string.Format(Gui.StartMessageBeforeTestStarts, cards.Count));

            foreach (string option in Gui.YesNoOptions)
            {
                UIProvider.PrintLine(option);
            }

            ConsoleKey button = UIProvider.GetPushedButton();

            switch (button)
            {
                case ConsoleKey.D2:
                    return;
            }

            int correctAnswers = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                UIProvider.ClearPage();

                UIProvider.PrintLine($"{i + 1}/{cards.Count}");

                UIProvider.PrintLine(string.Format(Gui.TaskMessageForVocabularyMemoryTest, cards[i].BackSide));

                int incorrectAnswersForNow = 0;

                bool isAlive = true;

                while (isAlive)
                {
                    string userAnswer = UIProvider.GetString().Trim();

                    if (userAnswer.ToLower() == cards[i].FrontSide.ToLower())
                    {
                        OutputPositiveAnswer();

                        correctAnswers++;

                        break;
                    }
                    else
                    {
                        OutputNegativeAnswer();

                        incorrectAnswersForNow++;
                    }

                    if (incorrectAnswersForNow >= UnsuccessfulAttemptsToAskForSurrender)
                    {
                        UIProvider.PrintLine(Gui.AskUserIfHeSurrenders);

                        foreach (string option in Gui.YesNoOptions)
                        {
                            UIProvider.PrintLine(option);
                        }

                        ConsoleKey decision = UIProvider.GetPushedButton();

                        switch (decision)
                        {
                            case ConsoleKey.D1:
                                isAlive = false;
                                break;
                            case ConsoleKey.D2:
                                UIProvider.ClearPage();
                                UIProvider.PrintLine(string.Format(Gui.TaskMessageForVocabularyMemoryTest, cards[i].BackSide));
                                break;
                        }
                    }
                }

                UIProvider.PrintLine(string.Format(Gui.CorrectAnswerIs, cards[i].FrontSide));

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();
            }

            UIProvider.PrintLine(string.Format(Gui.CorrectAnswersGiven, correctAnswers, cards.Count));

            UIProvider.RequestKeyPressToContinue();
        }
    }
}
