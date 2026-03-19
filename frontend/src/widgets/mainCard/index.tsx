import React from "react";
import { MainLogo } from "../mainLogo";
import { MainPageButtons } from "../../ui/mainPageButtons";
type Props = {
  onStartChat: () => void;
  onOpenUpdates: () => void;
};

export const MainCard: React.FC<Props> = ({
  onStartChat,
  onOpenUpdates
}) => {
  return (
    <div className="w-full max-w-md px-6 py-10 flex flex-col items-center gap-8">
      <MainLogo />
      {/* Кнопка Начать чат */}
      <MainPageButtons variant="primary" onClick={onStartChat}>
        Начать чат
      </MainPageButtons>
      <MainPageButtons variant="secondary" onClick={onOpenUpdates}>
        Что нового
      </MainPageButtons>
    </div>
  );
};
