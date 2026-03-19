export const ChatHeader = ({ title }: { title: string }) => {
  return (
    <div className="flex items-center justify-between p-5 border-b border-[#2b2f3b]">
      <div className="flex items-center gap-3">
        <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-[#3a3fff] to-[#7c4dff] flex items-center justify-center">
          <span className="text-white font-bold">AI</span>
        </div>
        <div>
          <div className="text-white font-bold">{title}</div>
          <div className="text-[#b7b7c8] text-sm">AI ассистент для аналитиков</div>
        </div>
      </div>
      <div className="flex items-center gap-2 text-sm text-[#b7b7c8]">
        <span className="w-2.5 h-2.5 rounded-full bg-[#2ecc71] shadow-[0_0_12px_rgba(46,204,113,0.65)]" />
        <span>Online</span>
      </div>
    </div>
  );
};
