using System.ComponentModel;

namespace Domain.Entities
{
    public enum DataVolume
    {
        [Description("Малый (до 1 ГБ)")]
        Small,

        [Description("Средний (1 - 100 ГБ)")]
        Medium,

        [Description("Большой (Более 100 ГБ)")]
        Large
    }
}