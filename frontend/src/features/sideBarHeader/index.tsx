import { Button } from "../../ui/button";
import { useChatStore } from "../../store/chat";

export const SideBarHeader = () => {
  const createChat = useChatStore((state) => state.createChat);

  return (
    <div className="flex justify-between">
      <div className="flex items-center gap-3">
        <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-[#3a3fff] to-[#7c4dff] flex items-center justify-center">
          <span className="font-bold">AI</span>
        </div>
        <h1 className="text-xl font-bold">DataTools AI</h1>
      </div>

      <Button onClick={createChat}>
        Новый чат
      </Button>
    </div>
  );
};
