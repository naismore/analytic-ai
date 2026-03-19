import { useState } from "react";

type Props = {
  question: string;
  options: string[];
  onSelect: (value: string) => void;
};

export const PollMessage = ({ question, options, onSelect }: Props) => {
  const [selected, setSelected] = useState<string | null>(null);

  const handleClick = (option: string) => {
    setSelected(option);
    onSelect(option);
  };

  return (
    <div className="bg-[#111827] border border-[#2b2f3b] text-[#d4d4e1] p-4 rounded-2xl max-w-[80%]">
      <p className="mb-3 font-medium">{question}</p>

      <div className="flex flex-col gap-2">
        {options.map((option) => (
          <button
            key={option}
            disabled={!!selected}
            onClick={() => handleClick(option)}
            className={`px-3 py-2 rounded-xl border text-left transition
            ${
              selected === option
                ? "bg-[#3a3fff] text-white"
                : "bg-[#0b0f1b] border-[#2b2f3b] hover:bg-[#1a1f2e]"
            }`}
          >
            {option}
          </button>
        ))}
      </div>
    </div>
  );
};