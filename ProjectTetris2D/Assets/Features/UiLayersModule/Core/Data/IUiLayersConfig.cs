using Features.Common.Core;

namespace Features.UiLayersModule.Core.Data
{
    public interface IUiLayersConfig
    {
        UiLayersContainer UiLayersContainer { get; }
        UiViewScreen[] Screens { get; }
    }
}