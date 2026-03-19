export type QuestionOption = {
  label: string
  value: number
}

export type Question = {
  id: number
  field: string
  question: string
  multi: boolean
  options: QuestionOption[]
}

// Значения соответствуют enum'ам на бэкенде
// SkillLevel: 0=Junior, 1=Middle, 2=Pro
// DataVolume: 0=Small, 1=Medium, 2=Large
// UserTasks: 0=VisualizationAndDashboards, 1=StatisticAnalyze, 2=MachineLearning, 3=ETL, 4=Database
// Budget: 0=Free, 1=Small, 2=NotLimited
// TechnicalBackground: 0=Humanitarian, 1=Tech, 2=Developer
// Integrations: 0=ExcelAndGoogleSheets, 1=SQLDatabase, 2=GoogleAnalytics, 3=APIServices

export const QUESTIONS: Question[] = [
  {
    id: 1,
    field: 'skillLevel',
    question: 'Какой ваш уровень опыта в работе с аналитическими инструментами?',
    multi: false,
    options: [
      { label: 'Начинающий (Junior)', value: 0 },
      { label: 'Средний (Middle)', value: 1 },
      { label: 'Продвинутый (Pro)', value: 2 }
    ]
  },
  {
    id: 2,
    field: 'dataVolume',
    question: 'Какой объём данных вы планируете анализировать?',
    multi: false,
    options: [
      { label: 'Малый (до 1 ГБ)', value: 0 },
      { label: 'Средний (1–100 ГБ)', value: 1 },
      { label: 'Большой (более 100 ГБ)', value: 2 }
    ]
  },
  {
    id: 3,
    field: 'userTasks',
    question: 'Какие задачи вы планируете решать? (можно выбрать несколько)',
    multi: true,
    options: [
      { label: 'Визуализация и дашборды', value: 0 },
      { label: 'Статистический анализ', value: 1 },
      { label: 'Машинное обучение', value: 2 },
      { label: 'ETL (обработка данных)', value: 3 },
      { label: 'Работа с базами данных', value: 4 }
    ]
  },
  {
    id: 4,
    field: 'budget',
    question: 'Какой у вас бюджет на инструменты?',
    multi: false,
    options: [
      { label: 'Бесплатно', value: 0 },
      { label: 'До 100$ в месяц', value: 1 },
      { label: 'Не ограничен', value: 2 }
    ]
  },
  {
    id: 5,
    field: 'technicalBackground',
    question: 'Какой у вас технический бэкграунд?',
    multi: false,
    options: [
      { label: 'Гуманитарный', value: 0 },
      { label: 'Технический (без программирования)', value: 1 },
      { label: 'Разработчик', value: 2 }
    ]
  },
  {
    id: 6,
    field: 'integrations',
    question: 'Какие интеграции вам нужны? (можно выбрать несколько)',
    multi: true,
    options: [
      { label: 'Excel / Google Sheets', value: 0 },
      { label: 'SQL базы данных', value: 1 },
      { label: 'Google Analytics', value: 2 },
      { label: 'API сервисы', value: 3 }
    ]
  }
]
