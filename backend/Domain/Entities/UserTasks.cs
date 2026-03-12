using System.ComponentModel;

namespace Domain.Entities
{
    public enum UserTasks
    {
        [Description("Создание визуализаций и дашбордов")]
        VisualizationAndDashboards,

        [Description("Статистический анализ данных")]
        StatisticAnalyze,

        [Description("Построение моделей машинного обучения")]
        MachineLearning,

        [Description("ETL процессы (извлечение, преобразование и загрузка данных)")]
        ETL,

        [Description("Работа с базами данных")]
        Database
    }
}