using System;

namespace ForeignLanguageApp.Models
{
    public abstract class Entity
    {
        public DateTime CreationTime { get; }

        public Entity(DateTime time)
        {
            CreationTime = time;
        }
    }
}
