using Domain.Dtos;

namespace Domain.Entities
{
    public class RecommendationAttributes
    {
        public Guid Guid { get; private set; }
        public Guid SessionId { get; private set; }
        public SkillLevel SkillLevel { get; private set; }
        public DataVolume DataVolume { get; private set; }
        public List<UserTasks> UserTasks { get; private set; }
        public Budget Budget { get; private set; }
        public TeamSize TeamSize { get; private set; }
        public TechnicalBackground TechnicalBackground { get; private set; }
        public List<Integrations> Integrations { get; private set; }

        private RecommendationAttributes(
            Guid sessionId,
            SkillLevel skillLevel,
            DataVolume dataVolume,
            List<UserTasks> userTasks,
            Budget budget,
            TeamSize teamSize,
            TechnicalBackground technicalBackground,
            List<Integrations> integrations)
        {
            Guid = Guid.NewGuid();
            SessionId = sessionId;
            SkillLevel = skillLevel;
            DataVolume = dataVolume;
            UserTasks = userTasks ?? new List<UserTasks>();
            Budget = budget;
            TeamSize = teamSize;
            TechnicalBackground = technicalBackground;
            Integrations = integrations ?? new List<Integrations>();
        }

        public static RecommendationAttributes Create(
            Guid sessionId,
            SkillLevel skillLevel,
            DataVolume dataVolume,
            IEnumerable<UserTasks> userTasks,
            Budget budget,
            TeamSize teamSize,
            TechnicalBackground technicalBackground,
            IEnumerable<Integrations> integrations)
        {
            if (sessionId == Guid.Empty)
                throw new ArgumentException("SessionId не может быть пустым", nameof(sessionId));

            if (!userTasks.Any())
                throw new InvalidOperationException("Должны быть указаны хотя бы одни UserTasks");

            return new RecommendationAttributes(
                sessionId,
                skillLevel,
                dataVolume,
                userTasks.ToList(),
                budget,
                teamSize,
                technicalBackground,
                integrations.ToList()
            );
        }
    }
}
