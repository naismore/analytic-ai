using System.ComponentModel;

namespace Domain.Entities
{
    public enum SkillLevel
    {
        [Description("Начальный уровень навыков")]
        Junior,

        [Description("Средний уровень навыков")]
        Middle,

        [Description("Продвинутый или профессиональный уровень")]
        Pro
    }
}