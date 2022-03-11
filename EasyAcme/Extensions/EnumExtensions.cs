using System.ComponentModel;
using System.Reflection;

namespace EasyAcme.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue) where T : struct, Enum
    {
        return EnumDescriptionHolder<T>.EnumDescriptions.GetValueOrDefault(enumValue) ?? enumValue.ToString();
    }

    private static class EnumDescriptionHolder<T> where T : struct, Enum
    {
        public static IReadOnlyDictionary<T, string?> EnumDescriptions { get; }

        static EnumDescriptionHolder()
        {
            EnumDescriptions = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                .ToDictionary(
                    x => (T) x.GetValue(null)!,
                    x => x.GetCustomAttribute<DescriptionAttribute>()?.Description);
        }
    }
}