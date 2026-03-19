import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { MainCard } from "../../widgets/mainCard"
import { UpdatesModal } from "../../widgets/updatesModal";
const MainPage: React.FC = () => {
  const [isUpdatesOpen, setIsUpdatesOpen] = useState(false);
  const navigate = useNavigate();

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-b from-[#0b0f1b] to-[#0a0c14] text-white">
      <MainCard
        onStartChat={() => navigate("/chat")}
         onOpenUpdates={() => setIsUpdatesOpen(true)}
      />

      {/* Окно Новинок */}
      {isUpdatesOpen && (
        <UpdatesModal onClose={() => setIsUpdatesOpen(false)} />
      )}
    </div>
  );
};

export default MainPage;
