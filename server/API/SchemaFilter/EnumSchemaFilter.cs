using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.SchemaFilter;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumValues = Enum.GetValues(context.Type)
                .Cast<Enum>()
                .Select(e => new OpenApiString($"{Convert.ToInt32(e)} = {e.ToString()}"))
                .ToList();
            foreach (var enumValue in enumValues)
            {
                schema.Enum.Add(enumValue);
            }
        }
    }
}