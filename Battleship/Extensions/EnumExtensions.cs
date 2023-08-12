namespace Battleship.Extensions;

public static class EnumExtensions
{
    public static T? GetAttribute<T>(this Enum val) where T : System.Attribute
    {
        var memberInfos = val.GetType().GetMember(val.ToString());
        var attributes = memberInfos[0].GetCustomAttributes(typeof(T), false);

        return attributes.Length > 0 ? (T)attributes[0] : null;
    }
}