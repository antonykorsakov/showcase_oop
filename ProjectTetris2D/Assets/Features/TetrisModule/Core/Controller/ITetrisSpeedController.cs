using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Features.TetrisModule.Tests.EditMode")]

namespace Features.TetrisModule.Core.Controller
{
    public interface ITetrisSpeedController
    {
        GameParameterController Level { get; }

        void Pause();
        void Resume();

        void Tick(float deltaTime);

        internal void SetAction(Action callback);
        internal void TurnOnAccelerate();
        internal void TurnOffAccelerate();
    }
}