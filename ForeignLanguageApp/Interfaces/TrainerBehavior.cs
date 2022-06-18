using ForeignLanguageApp.Models;
using System.Collections.Generic;

namespace ForeignLanguageApp.Interfaces
{
    public interface TrainerBehavior
    {
        public void StartTraining(List<Topic> topics);
    }
}
