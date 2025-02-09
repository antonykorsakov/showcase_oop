using System;
using Features.Common.Core;

namespace Features.UiViewResultScreenModule.Core
{
    public interface IUiViewResultScreenController
    {
        UiViewResultScreen UiViewScreen { get; }

        event Action ContinueButtonClickEvent;
        event Action RestartButtonClickEvent;
        event Action ExitButtonClickEvent;

        void SetStats(IGameSessionStats stats);
    }
}