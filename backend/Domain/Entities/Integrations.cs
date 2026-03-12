using System.ComponentModel;

namespace Domain.Entities
{
    public enum Integrations
    {
        [Description("Интеграция с Excel или Google Sheets")]
        ExcelAndGoogleSheets,

        [Description("Интеграция с SQL базой данных")]
        SQLDatabase,

        [Description("Интеграция с Google Analytics")]
        GoogleAnalytics,

        [Description("Интеграция с внешними API сервисами")]
        APIServices
    }
}