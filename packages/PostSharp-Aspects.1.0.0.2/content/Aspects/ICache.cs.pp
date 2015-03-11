namespace $rootnamespace$.Aspects
{
    public interface ICache
    {
        object this[string key] { get; set; }
    }
}