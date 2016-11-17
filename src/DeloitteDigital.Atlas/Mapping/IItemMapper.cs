using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping
{
    public interface IItemMapper
    {
        TModel Map<TModel>(TModel model, Item item);

        TModel Map<TModel>(Item item);
    }
}
