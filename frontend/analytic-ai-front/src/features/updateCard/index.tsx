type UpdateCardProps = {
  version: string;
  type: string;
  description: string;
};

export const UpdateCard = ({ version, type, description }: UpdateCardProps) => {
  return (
    <div className="p-4 rounded-2xl bg-[#0b0f1b] border border-[#2b2f3b]">
      <div className="flex items-center justify-between">
        <div className="text-sm text-[#b7b7c8]">{version}</div>
        <div className="text-xs text-[#8b8bc1]">{type}</div>
      </div>
      <div className="mt-2 text-sm text-white">{description}</div>
    </div>
  );
};
