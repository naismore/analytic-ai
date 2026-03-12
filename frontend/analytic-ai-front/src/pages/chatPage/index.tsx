import { ChatModule } from "../../widgets/chatModule";
import { SideBarModule } from "../../widgets/sideBarModule";

const ChatPage = () => {
  return (
    <div className="min-h-screen flex bg-gradient-to-b from-[#0b0f1b] to-[#0a0c14] text-white">
      <SideBarModule />
      <ChatModule />
    </div>
  );
};

export default ChatPage;
