using System.Runtime.Serialization;

namespace PowerPlant.Domain
{
    public enum PowerplantType
    {
        [EnumMember(Value = "windturbine")]
        Windturbine,
        [EnumMember(Value = "gasfired")]
        Gas,
        [EnumMember(Value = "turbojet")]
        Turbojet
    }
}
