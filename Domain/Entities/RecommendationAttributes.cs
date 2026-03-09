using Domain.DTO;
using Domain.Dtos;

namespace Domain.Entities
{
    public class RecommendationAttributes
    {
        public Guid Guid { get; set; }
        public Guid SessionId { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public DataVolume DataVolume { get; set; }
        public IEnumerable<UserTasks> UserTasks { get; set; }
        public Budget Budget { get; set; }
        public TechnicalBackground TechnicalBackground { get; set; }
        public IEnumerable<Integrations> Integrations { get; set; }

        public virtual RecommendationSession Session { get; set; }


        public static RecommendationAttributes Create(RecommendationAttributesDto dto)
        {
            return new RecommendationAttributes
            {
                SessionId = dto.SessionId,
                SkillLevel = dto.SkillLevel,
                DataVolume = dto.DataVolume,
                UserTasks = dto.UserTasks,
                Budget = dto.Budget,
                TechnicalBackground = dto.TechnicalBackground,
                Integrations = dto.Integrations,
            };
        }
    }
}
