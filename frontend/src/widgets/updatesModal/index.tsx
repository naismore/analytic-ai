import React, { useEffect, useState } from "react";
import { UpdateCard } from "../../features/updateCard";
import { MainPageButtons } from "../../ui/mainPageButtons";

type Props = {
  onClose: () => void;
};

type UpdateType = {
  version: string;
  type: string;
  description: string;
};

export const UpdatesModal: React.FC<Props> = ({ onClose }) => {
  const [updates, setUpdates] = useState<UpdateType[]>([]);

  useEffect(() => {
    fetch("/dataBase/updates.json")
      .then((res) => res.json())
      .then((data) => setUpdates(data.updates));
  }, []);

  return (
    <div className="fixed inset-0 bg-black/60 flex items-end justify-center">
      <div className="w-full max-w-lg bg-[#111827] rounded-t-3xl p-6 shadow-[0_-10px_40px_rgba(0,0,0,0.6)]">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-xl font-bold">Новые версии</h2>
        </div>

        <div className="space-y-4">
          {updates.map((u) => (
            <UpdateCard
              key={u.version}
              version={u.version}
              type={u.type}
              description={u.description}
            />
          ))}
        </div>

        <MainPageButtons
          variant="primary"
          onClick={onClose}
          className="mt-6"
        >
          Понятно
        </MainPageButtons>
      </div>
    </div>
  );
};
