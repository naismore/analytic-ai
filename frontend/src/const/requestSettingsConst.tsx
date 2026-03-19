const settings = [
  {
    label: "Опыт",
    type: "select",
    value: "middle",
    options: [
      { value: "junior", label: "Junior" },
      { value: "middle", label: "Средний" },
      { value: "senior", label: "Senior" },
      { value: "lead", label: "Lead" },
    ],
  },
  {
    label: "Объём памяти в ГБ",
    type: "input",
    placeholder: "...",
  },
  {
    label: "Задача",
    type: "select",
    value: "ml",
    options: [
      { value: "eda", label: "EDA" },
      { value: "ml", label: "ML модель" },
      { value: "dashboards", label: "Дашборды" },
      { value: "etl", label: "ETL" },
    ],
  },
  {
    label: "Бюджет в ₽",
    type: "input",
    placeholder: "...",
  },
  {
    label: "Командная работа",
    type: "select",
    value: "team",
    options: [
      { value: "solo", label: "Solo" },
      { value: "pair", label: "Парная" },
      { value: "team", label: "Команда" },
    ],
  },
  {
    label: "Real-Time аналитика",
    type: "select",
    value: "yes",
    options: [
      { value: "no", label: "Нет" },
      { value: "yes", label: "Да" },
    ],
  },
] 
export default settings