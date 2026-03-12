using System.ComponentModel;

namespace Domain.Entities
{
    public enum TechnicalBackground
    {
        [Description("Гуманитарное образование, минимальный технический опыт")]
        Humanitarian,

        [Description("Техническое образование или базовые навыки программирования")]
        Tech,

        [Description("Профессиональный разработчик или сильный технический бэкграунд")]
        Developer
    }
}