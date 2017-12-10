using System.Web.Http.Filters;

namespace MP.Framework.Web.Filters
{
    public interface IOrderedFilter : IFilter
    {
        int Order { get; }
    }
}
