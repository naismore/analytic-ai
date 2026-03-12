import React from "react";

type MainPageButtonsProps = {
  variant?: "primary" | "secondary";
  onClick: () => void;
  children: React.ReactNode;
  className?: string;
};

export const MainPageButtons: React.FC<MainPageButtonsProps> = ({
  variant = "primary",
  onClick,
  children,
  className = ""
}) => {
  const base =
    "w-full rounded-2xl font-semibold transition";

  const styles =
    variant === "primary"
      ? "py-4 bg-gradient-to-r from-[#3a3fff] to-[#7c4dff] text-white text-lg shadow-[0_15px_35px_rgba(124,77,255,0.35)] hover:brightness-105"
      : "py-3 border border-[#3a3fff] bg-[#0b0f1b]/60 text-[#b7b7c8] font-medium hover:bg-[#0b0f1b]/80";

  return (
    <button className={`${base} ${styles} ${className}`} onClick={onClick}>
      {children}
    </button>
  );
};
