using Mapster;

namespace GVZ.Task2BackendASPNETCore;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<QuestionMessageCreateDto, QuestionMessage>()
            .Map(dest => dest.SubmittedAt, _ => DateTime.UtcNow);

        config.NewConfig<AdministrativeChangeMessageCreateDto, AdministrativeChangeMessage>()
            .Map(dest => dest.SubmittedAt, _ => DateTime.UtcNow);
    }
}
