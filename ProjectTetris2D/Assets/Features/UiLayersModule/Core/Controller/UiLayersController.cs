using System;
using UnityEngine;

namespace Features.UiLayersModule.Core.Controller
{
    public class UiLayersController : IUiLayersController
    {
        public Canvas Canvas { get; private set; }
        public Camera UiCamera { get; private set; }
        public Transform Container { get; private set; }
        public bool IsActivated { get; private set; }

        public event Action SetEvent;
        public event Action ActivatedEvent;

        public void SetupContainer(UiLayersContainer uiLayersContainer)
        {
            Canvas = uiLayersContainer.Canvas;
            UiCamera = uiLayersContainer.Camera;
            Container = Canvas.transform;
            SetEvent?.Invoke();
        }

        public void Activate()
        {
            IsActivated = true;
            ActivatedEvent?.Invoke();
        }
    }
}