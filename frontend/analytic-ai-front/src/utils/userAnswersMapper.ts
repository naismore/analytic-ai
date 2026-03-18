// src/utils/userAnswersMapper.ts

import {
  BudgetMap,
  DataVolumeMap,
  IntegrationsMap,
  SkillLevelMap,
  TeamSizeMap,
  TechnicalBackgroundMap,
  UserTasksMap
} from '../entities/enums';

type EnumConfig = {
  map: Record<string, string>;
  enumArr: readonly string[];
  isArray?: boolean;
  label: string;
};

// Конфиг для всех полей
const mapConfig: Record<string, EnumConfig> = {
  skillLevel: { map: SkillLevelMap, enumArr: ['Junior', 'Middle', 'Pro'], label: 'Уровень навыков' },
  dataVolume: { map: DataVolumeMap, enumArr: ['Small', 'Medium', 'Large'], label: 'Объём данных' },
  budget: { map: BudgetMap, enumArr: ['Free', 'Small', 'NotLimited'], label: 'Бюджет' },
  teamSize: { map: TeamSizeMap, enumArr: ['Solo', 'Small', 'Large'], label: 'Размер команды' },
  technicalBackground: { map: TechnicalBackgroundMap, enumArr: ['Humanitarian', 'Tech', 'Developer'], label: 'Технический бэкграунд' },
  userTasks: { map: UserTasksMap, enumArr: ['VisualizationAndDashboards','StatisticAnalyze','MachineLearning','ETL','Database'], isArray: true, label: 'Задачи пользователя' },
  integrations: { map: IntegrationsMap, enumArr: ['ExcelAndGoogleSheets','SQLDatabase','GoogleAnalytics','APIServices'], isArray: true, label: 'Интеграции' },
};

export const mapEnum = (value: number | number[], enumArr: readonly string[]): string | string[] => {
  if (Array.isArray(value)) return value.map(v => enumArr[v]);
  return enumArr[value];
};

// Формирует HTML-блок информации о пользователе
export const renderUserAnswers = (answers: Record<string, any>): string => {
  const lines = Object.entries(mapConfig).flatMap(([field, cfg]) => {
    const value = answers[field];
    if (value === undefined || (Array.isArray(value) && value.length === 0)) return [];

    if (cfg.isArray) {
      const keys = mapEnum(value, cfg.enumArr) as string[];
      return `<p><strong>${cfg.label}:</strong> ${keys.map(k => cfg.map[k as keyof typeof cfg.map]).join(', ')}</p>`;
    } else {
      const key = mapEnum(value, cfg.enumArr) as string;
      return `<p><strong>${cfg.label}:</strong> ${cfg.map[key as keyof typeof cfg.map]}</p>`;
    }
  });

  if (!lines.length) return '';

  return `
    <div class="p-4 mb-2 rounded-xl border border-gray-600 bg-gray-700 text-gray-200">
      <h4 class="font-semibold mb-2">Информация о пользователе:</h4>
      ${lines.join('')}
    </div>
  `;
};