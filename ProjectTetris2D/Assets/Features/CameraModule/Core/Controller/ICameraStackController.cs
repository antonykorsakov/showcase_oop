using UnityEngine;

namespace Features.CameraModule.Core.Controller
{
    public interface ICameraStackController
    {
        void SetGameCamera(Camera value);
        void SetUiCamera(Camera value);
    }
}