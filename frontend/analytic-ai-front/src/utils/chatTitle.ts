// src/utils/chatTitle.ts
import { UserTasksMap } from '../entities/enums';

export const getChatTitle = (userTasks: number[] | undefined): string => {
  if (!userTasks || !userTasks.length) return `Сессия ${new Date().toLocaleDateString('ru-RU')}`;

  const tasks = userTasks
    .map((t) => {
      const key = Object.keys(UserTasksMap)[t];
      return key ? UserTasksMap[key as keyof typeof UserTasksMap] : null;
    })
    .filter(Boolean);

  const tasksStr = tasks.join(', ');

  return tasksStr ? tasksStr : `Сессия ${new Date().toLocaleDateString('ru-RU')}`;
};