import { useChatStore } from "../../store/chat";

export const ChatHistory = () => {
  const { chats, activeChatId, setActiveChat } = useChatStore();

  return (
    <div>
      <div className="font-bold mb-2">История запросов</div>

      <div className="scroll-bar bg-[#0b0f1b] rounded-xl p-3 border border-[#2b2f3b] max-h-[360px] overflow-y-auto">

        {chats.map((chat) => {
          const lastMessage = chat.messages.at(-1);

          return (
            <div
              key={chat.chatId}
              onClick={() => setActiveChat(chat.chatId)}
              className={`cursor-pointer flex justify-between text-sm mt-1 p-2 rounded-xl transition-colors ${
                activeChatId === chat.chatId
                  ? "bg-[#1f2330]"
                  : "hover:bg-[#161a24]"
              }`}
            >
              <span>
                {lastMessage
                  ? new Date(lastMessage.time).toLocaleDateString("ru-RU", {
                      day: "2-digit",
                      month: "2-digit"
                    })
                  : "—"}
              </span>

              <span className="truncate max-w-[180px]">
                {chat.title}
              </span>
            </div>
          );
        })}
      </div>
    </div>
  );
};
