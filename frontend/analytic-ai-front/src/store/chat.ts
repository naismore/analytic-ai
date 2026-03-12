import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { ChatType } from "../types/chat";
import { useQuestionnaireStore } from "./questionnnaire";

const sortByLastMessage = (chats: ChatType[]) =>
  [...chats].sort((a, b) => {
    const aTime = a.messages.at(-1)?.time ?? "";
    const bTime = b.messages.at(-1)?.time ?? "";
    return bTime.localeCompare(aTime);
  });

type ChatStore = {
  chats: ChatType[];
  activeChatId: string | null;

  setChats: (chats: ChatType[]) => void;
  setActiveChat: (chatId: string) => void;
  updateChat: (chat: ChatType) => void;
  createChat: () => void;
};

export const useChatStore = create<ChatStore>()(
  persist(
    (set) => ({
      chats: [],
      activeChatId: null,

      setChats: (chats) =>
        set({
          chats: sortByLastMessage(chats),
          activeChatId: chats[0]?.chatId ?? null
        }),

      setActiveChat: (chatId) =>
        set({ activeChatId: chatId }),

      updateChat: (updatedChat) =>
        set((state) => {
          const updated = state.chats.map((chat) =>
            chat.chatId === updatedChat.chatId ? updatedChat : chat
          );

          return {
            chats: sortByLastMessage(updated)
          };
        }),

      createChat: () =>
        set((state) => {
          const newChatId = `chat_${Date.now()}`;

          // Сброс опроса при создании нового чата
          useQuestionnaireStore.getState().reset();

          const newChat: ChatType = {
            chatId: newChatId,
            title: "Новый чат",
            messages: [
              {
                id: 1,
                author: "bot",
                text: "✅ Вы ответили на все вопросы. Теперь мы можем начать анализ и подобрать подходящие инструменты. Опишите вашу задачу или отправьте данные для анализа.",
                time: new Date().toISOString()
              }
            ]
          };

          return {
            chats: sortByLastMessage([newChat, ...state.chats]),
            activeChatId: newChatId
          };
        })
    }),
    {
      name: "chat-storage"
    }
  )
);