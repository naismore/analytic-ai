export const MainLogo = () => {
  return (
    <div className="flex flex-col items-center gap-3">
      <div className="w-20 h-20 rounded-2xl bg-gradient-to-br from-[#3a3fff] to-[#7c4dff] flex items-center justify-center shadow-[0_10px_30px_rgba(124,77,255,0.35)]">
        <svg width="36" height="36" viewBox="0 0 24 24" fill="none">
          <path
            d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2Z"
            stroke="white"
            strokeWidth="2"
          />
          <path
            d="M8 12l2 2 4-4"
            stroke="white"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          />
        </svg>
      </div>

      <div className="text-center">
        <h1 className="text-3xl font-bold tracking-tight">DataTools AI</h1>
        <p className="text-sm text-[#b7b7c8] mt-1">
          AI ассистент для аналитиков
        </p>
      </div>
    </div>
  );
};
