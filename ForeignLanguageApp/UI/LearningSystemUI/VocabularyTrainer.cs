using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System.Collections.Generic;

namespace ForeignLanguageApp.UI.LearningSystemUI
{
    public class VocabularyTrainer
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly TrainerBehavior fieldOfDreams;

        private readonly TrainerBehavior rememberEverything;

        public VocabularyTrainer(IUserInterfaceProvider UIProvider, TrainerBehavior rememberEverything, TrainerBehavior fieldOfDreams)
        {
            this.UIProvider = UIProvider;

            this.fieldOfDreams = fieldOfDreams;

            this.rememberEverything = rememberEverything;
        }

        public void StartFieldOfDreamsExercise(List<Topic> topics)
        {
            if (!(topics.Count > 0))
            {
                ShowErrorMessage();

                return;
            }

            fieldOfDreams.StartTraining(topics);
        }

        public void StartRememberEverythingExercise(List<Topic> topics)
        {
            if (!(topics.Count > 0))
            {
                ShowErrorMessage();

                return;
            }

            rememberEverything.StartTraining(topics);
        }

        private void ShowErrorMessage()
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(Gui.EmptyTopicsError);

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }
    }
}
