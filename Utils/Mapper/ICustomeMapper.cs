namespace BackendService.Utils.Mapper
{
    public interface ICustomeMapper<TSource,TDestination>
    {
        TDestination Map(TSource source);
        TSource ReverseMap(TDestination destination);
    }
}
