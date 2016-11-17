using System;
using System.Linq.Expressions;

namespace DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta
{
    public abstract class BasePropertyMeta<TPropertyType, TModel> : IPropertyMeta
    {
        public string PropertyName { get; set; }
        public string MappingName { get; set; }
        public IMapper Mapper { get; set; }

        private Action<TModel, TPropertyType> setProperty = null;
        public Action<TModel, TPropertyType> AssignValueToModelProperty
        {
            get
            {
                if (setProperty == null)
                {
                    setProperty = ConfigurePropertySetter();
                }

                return setProperty;
            }
        }

        public virtual string PropertyKey => typeof(TPropertyType).ToString();

        public Action<TModel, TPropertyType> ConfigurePropertySetter()
        {
            var objectExpression = Expression.Parameter(typeof(TModel));
            var propertyExpression = Expression.Parameter(typeof(TPropertyType), PropertyName);
            var propertyGetterExpression = Expression.Property(objectExpression, PropertyName);

            var result = Expression.Lambda<Action<TModel, TPropertyType>>
            (
                Expression.IfThen(Expression.Equal(propertyGetterExpression, Expression.Default(typeof(TPropertyType))),
                Expression.Assign(propertyGetterExpression, propertyExpression)), objectExpression, propertyExpression
            ).Compile();

            return result;
        }
    }
}
