using System;

namespace Features.ZenjectExtendModule.Core
{
    public interface IZenjectLastController
    {
        bool Initialized { get; }

        event Action InitializedEvent;

        void Initialize();
    }
}