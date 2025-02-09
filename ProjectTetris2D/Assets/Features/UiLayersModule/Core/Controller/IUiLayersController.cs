using System;
using UnityEngine;

namespace Features.UiLayersModule.Core.Controller
{
    public interface IUiLayersController
    {
        Canvas Canvas { get; }
        Camera UiCamera { get; }
        Transform Container { get; }
        bool IsActivated { get; }

        event Action SetEvent;
        event Action ActivatedEvent;

        void SetupContainer(UiLayersContainer uiLayersContainer);
        void Activate();
    }
}