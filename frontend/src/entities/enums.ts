// src/entities/enums.ts

// ----------------- Budget -----------------
export const BudgetMap: Record<string, string> = {
  Free: "Бесплатно",
  Small: "100$/мес",
  NotLimited: "Неограничен",
};

export type Budget = keyof typeof BudgetMap;

// ----------------- DataVolume -----------------
export const DataVolumeMap: Record<string, string> = {
  Small: "Малый (до 1 ГБ)",
  Medium: "Средний (1 - 100 ГБ)",
  Large: "Большой (Более 100 ГБ)",
};

export type DataVolume = keyof typeof DataVolumeMap;

// ----------------- Integrations -----------------
export const IntegrationsMap: Record<string, string> = {
  ExcelAndGoogleSheets: "Интеграция с Excel или Google Sheets",
  SQLDatabase: "Интеграция с SQL базой данных",
  GoogleAnalytics: "Интеграция с Google Analytics",
  APIServices: "Интеграция с внешними API сервисами",
};

export type Integrations = keyof typeof IntegrationsMap;

// ----------------- SkillLevel -----------------
export const SkillLevelMap: Record<string, string> = {
  Junior: "Начальный уровень навыков",
  Middle: "Средний уровень навыков",
  Pro: "Продвинутый или профессиональный уровень",
};

export type SkillLevel = keyof typeof SkillLevelMap;

// ----------------- TeamSize -----------------
export const TeamSizeMap: Record<string, string> = {
  Solo: "Работа в одиночку (1 человек)",
  Small: "Небольшая команда (2–5 человек)",
  Large: "Крупная команда (6 и более человек)",
};

export type TeamSize = keyof typeof TeamSizeMap;

// ----------------- TechnicalBackground -----------------
export const TechnicalBackgroundMap: Record<string, string> = {
  Humanitarian: "Гуманитарное образование, минимальный технический опыт",
  Tech: "Техническое образование или базовые навыки программирования",
  Developer: "Профессиональный разработчик или сильный технический бэкграунд",
};

export type TechnicalBackground = keyof typeof TechnicalBackgroundMap;

// ----------------- UserTasks -----------------
export const UserTasksMap: Record<string, string> = {
  VisualizationAndDashboards: "Создание визуализаций и дашбордов",
  StatisticAnalyze: "Статистический анализ данных",
  MachineLearning: "Построение моделей машинного обучения",
  ETL: "ETL процессы (извлечение, преобразование и загрузка данных)",
  Database: "Работа с базами данных",
};

export type UserTasks = keyof typeof UserTasksMap;