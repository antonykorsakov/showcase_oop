namespace Features.JsonModule.Core
{
    public interface IJsonModuleConfig
    {
        string StatisticsFileName { get; }
        int EndlessLoopProtection { get; }
    }
}