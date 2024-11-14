using HotChocolate.Types;
using Juan.Models;

namespace Juan.GraphQL
{
    public class AutorType : ObjectType<Autor>
    {
        protected override void Configure(IObjectTypeDescriptor<Autor> descriptor)
        {
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.Nombre).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.Apellido).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.FechaNacimiento).Type<DateType>();
            descriptor.Field(a => a.Nacionalidad).Type<StringType>();
            descriptor.Field(a => a.Biografia).Type<StringType>();
        }
    }
}