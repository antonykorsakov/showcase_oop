using UnityEngine;

namespace Features.TetrisModule.Core.Controller
{
    public interface ITetrisManager
    {
        void GestureHandling(Vector2 value);
        void ReStartGame();
    }
}