export type MessageType = {
  id: number;
  author: "user" | "bot";
  text: string;
  time: string;
};

export type ChatType = {
  chatId: string;
  title: string;
  messages: MessageType[];
};
