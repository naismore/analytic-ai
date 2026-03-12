using Application.Abstract;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Resolver;

public class EnumResolver : IEnumResolver
{
    public string Resolve(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?
            .GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}