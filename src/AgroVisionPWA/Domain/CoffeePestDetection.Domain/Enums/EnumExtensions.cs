using CoffeePestDetection.Domain.Enums.Features.Auth;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace CoffeePestDetection.Domain.Enums;

// Método de extensión para leer el valor en texto fácilmente
public static class EnumExtensions
{
    public static string GetValue(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute == null ? value.ToString() : attribute.Description;
    }


    // --- Convierte el string del Frontend de vuelta a Enum ---
    public static T? GetFromDescription<T>(string description) where T : struct, Enum
    {
        // Validamos que el string de búsqueda no sea nulo o vacío
        if (string.IsNullOrWhiteSpace(description)) return null;

        // Obtenemos todos los campos del Enum tipo T pasado por parámetro
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            // Comparamos la descripción ignorando mayúsculas/minúsculas
            if (attribute != null && string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase))
            {
                // Retornamos el valor convertido al Enum correspondiente
                return (T)field.GetValue(null)!;
            }
        }

        return null; // Si no encuentra ninguna coincidencia, retorna null
    }

    public static string GetDescription(this Enum value)
    {
        var field =
            value.GetType().GetField(value.ToString());

        var attribute =
            field?
            .GetCustomAttributes(
                typeof(
                    DescriptionAttribute),
                false)
            .FirstOrDefault()
            as DescriptionAttribute;

        return attribute?.Description
            ?? value.ToString();
    }
}
