
import { ChatHistory } from "../../features/chatHistory";
import { SideBarHeader } from "../../features/sideBarHeader";
import { AccountSideBarDisplay } from "../../features/accountSideBarDisplay";

export const SideBarModule = () => {
    return(
        <>
            <aside className="w-[400px] bg-[#111827] rounded-r-3xl p-6 flex flex-col gap-6">
                <SideBarHeader />
                <ChatHistory />
                <div className="bg-[#0b0f1b] rounded-2xl p-4 border border-[#2b2f3b] mt-auto">          
                    <AccountSideBarDisplay />
                </div>
            </aside>
        </>
     )
}