using System.ComponentModel;

namespace Domain.Entities
{
    public enum Budget
    {
        [Description("Бесплатно")]
        Free,

        [Description("100$/мес")]
        Small,

        [Description("Неограничен")]
        NotLimited
    }
}