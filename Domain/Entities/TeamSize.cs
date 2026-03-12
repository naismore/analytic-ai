using System.ComponentModel;

namespace Domain.Entities
{
    public enum TeamSize
    {
        [Description("Работа в одиночку (1 человек)")]
        Solo,

        [Description("Небольшая команда (2–5 человек)")]
        Small,

        [Description("Крупная команда (6 и более человек)")]
        Large
    }
}