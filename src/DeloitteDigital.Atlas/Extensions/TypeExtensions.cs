using System;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    /// <summary>
    /// Extension methods for all objects
    /// </summary>
    [LegacyCode]
    public static class TypeExtensions
    {
        public static bool IsNullableOfT( this Type theType )
        {
            return theType.IsGenericType && theType.GetGenericTypeDefinition( ) == typeof( Nullable<> );
        }

        public static bool SetValue( this object theObject, string propertyName, object val )
        {
            try
            {
                var property = theObject.GetType( ).GetProperty( propertyName );
                if ( property == null )
                    return false;
                // convert the value to the expected type
                val = Convert.ChangeType( val, property.PropertyType );
                // attempt the assignment
                property.SetValue( theObject, val, null );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object GetPropertyValue( this object theObject, string propertyName )
        {
            try
            {
                var property = theObject.GetType( ).GetProperty( propertyName );
                return property == null ? null : property.GetValue( theObject, null );
            }
            catch
            {
                return null;
            }
        }
    }
}
