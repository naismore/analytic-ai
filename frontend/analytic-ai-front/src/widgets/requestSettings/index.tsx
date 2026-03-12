import settings from "../../const/requestSettingsConst";
import RequestSettingCard from "../../features/requestSettingCard";

const RequestSettings: React.FC = () => {
  return (
    <div className="mt-4 grid grid-cols-2 gap-4">
      {settings.map((item) => (
        <RequestSettingCard key={item.label} item={item} />
      ))}
    </div>
  );
};

export default RequestSettings;
