using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace ApiReperenceServer.Source.App.DapperUtils
{
    public class Section<T>
    {
        static Section()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                typeof(T), (type, columnName) =>
                type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }
    }
}
