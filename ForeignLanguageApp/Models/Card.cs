using ForeignLanguageApp.UI;
using System;
using System.Text.Json.Serialization;

namespace ForeignLanguageApp.Models
{
    public class Card : Entity
    {

        private string backSide;

        private string frontSide;

        public string FrontSide 
        {
            get { return frontSide; }
            private set
            {
                if (!(value.Length >= 2))
                {
                    throw new ArgumentException(Gui.CardNotMeetRequirementsError);
                }
                
                frontSide = value;
            }
        }

        public string BackSide 
        { 
            get { return backSide; }
            private set
            {
                if (!(value.Length >= 2))
                {
                    throw new ArgumentException(Gui.CardNotMeetRequirementsError);
                }

                backSide = value;
            }
        }

        [JsonConstructor]
        public Card(DateTime creationTime, string frontSide, string backSide) : base(creationTime)
        {
            FrontSide = frontSide;

            BackSide = backSide;
        }

        public Card(string frontSide, string backSide) : this(DateTime.Now, frontSide, backSide)
        {}

        public void Update(string frontSide, string backSide)
        {
            FrontSide = frontSide;

            BackSide = backSide;
        }

        public override string ToString()
        {
            return $"{FrontSide, -15} -  {BackSide, -15} [{CreationTime:dd MMMM yyyy HH:mm}]";
        }
    }
}
