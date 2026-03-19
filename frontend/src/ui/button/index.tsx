import React from "react";

type ButtonProps = {
  onClick?: () => void;
  children: React.ReactNode;
  disabled?: boolean;
  type?: "button" | "submit" | "reset";
  className?: string;
};

export const Button = ({
  onClick,
  children,
  disabled,
  type = "button",
  className = ""
}: ButtonProps) => {
  return (
    <button
      type={type}
      disabled={disabled}
      onClick={onClick}
      className={`py-3 px-5 rounded-2xl bg-gradient-to-r from-[#3a3fff] to-[#7c4dff] font-semibold hover:brightness-105 transition disabled:opacity-50 disabled:cursor-not-allowed ${className}`}
    >
      {children}
    </button>
  );
};
