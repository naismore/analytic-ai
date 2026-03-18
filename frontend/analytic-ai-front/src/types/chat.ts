export type QuestionnaireData = {
  currentQuestion: number;
  answers: Record<string, number | number[]>;
  finished: boolean;
};

export type MessageType = {
  id: number;
  author: "user" | "bot";
  text: string;
  time: string;
  isHtml: boolean;
  sessionId: string | null;
};

export type ChatType = {
  chatId: string;
  title: string;
  messages: MessageType[];
  createdAt?: string;
  questionnaire?: QuestionnaireData;
  sessionCreated?: boolean; // <--- новый флаг
};
