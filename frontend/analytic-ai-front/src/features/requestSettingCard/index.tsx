type OptionType = {
  value: string;
  label: string;
};

type SettingType = {
  label: string;
  type: "input" | "select";
  placeholder?: string;
  value?: string;
  options?: OptionType[];
};

type RequestSettingCardProps = {
  item: SettingType;
};

const RequestSettingCard = ({ item }: RequestSettingCardProps) => {
  return (
    <div className="bg-[#0b0f1b] rounded-xl p-3 border border-[#2b2f3b]">
      <div className="text-xs text-[#b7b7c8]">{item.label}</div>

      {item.type === "input" ? (
        <input
          className="w-full bg-transparent pt-1  cursor-text outline-none text-white placeholder:text-[#6b6f7d]"
          placeholder={item.placeholder}
        />
      ) : (
        <select
          className="w-full bg-[#0b0f1b] pt-1  cursor-pointer text-white outline-none"
          defaultValue={item.value}
        >
          {item.options?.map((opt) => (
            <option
              key={opt.value}
              value={opt.value}
              className="bg-[#0b0f1b] text-white"
            >
              {opt.label}
            </option>
          ))}
        </select>
      )}
    </div>
  );
};

export default RequestSettingCard;
