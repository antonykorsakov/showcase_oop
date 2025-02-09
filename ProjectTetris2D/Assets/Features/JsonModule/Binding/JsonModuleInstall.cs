using System;
using Features.JsonModule.Behavior;
using Features.JsonModule.Core;
using UnityEngine;
using Zenject;

namespace Features.JsonModule.Binding
{
    public sealed class JsonModuleInstall : MonoInstaller<JsonModuleInstall>
    {
        [SerializeField] private JsonModuleConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<JsonModuleBehavior>().AsSingle().WithArguments(_config);

            // main controllers
            Container.BindInterfacesTo(GetControllerType()).AsSingle().WithArguments(_config);
        }

        private Type GetControllerType()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WebGLPlayer:
                    return typeof(JsonWebController);

                default:
                    return typeof(JsonDeviceController);
            }
        }
    }
}