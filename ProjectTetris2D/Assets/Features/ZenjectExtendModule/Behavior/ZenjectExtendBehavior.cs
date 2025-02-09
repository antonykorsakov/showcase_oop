using Features.ZenjectExtendModule.Core;
using Zenject;

namespace Features.ZenjectExtendModule.Behavior
{
    public class ZenjectExtendBehavior : IInitializable
    {
        [Inject] private IZenjectLastController ZenjectLastController { get; }

        public void Initialize() => ZenjectLastController.Initialize();
    }
}