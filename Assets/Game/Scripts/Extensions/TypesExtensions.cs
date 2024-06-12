namespace Game.Scripts.Extensions
{
    using System;
    using System.Text;

    public static class TypesExtensions
    {
        public static string GetFriendlyTypeName(this Type type)
        {
            if (type.IsGenericParameter) return type.Name;
            if (!type.IsGenericType) return type.Name;

            var builder = new StringBuilder();
            var name = type.Name;
            var index = name.IndexOf("`", StringComparison.Ordinal);

            builder.AppendFormat("{0}.{1}", type.Namespace, name[..index]);
            builder.Append('<');

            var first = true;
            foreach (var arg in type.GetGenericArguments())
            {
                if (!first) builder.Append(',');
                builder.Append(arg.GetFriendlyTypeName());
                first = false;
            }

            builder.Append('>');
            return builder.ToString();
        }
    }
}