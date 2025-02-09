using System;
using Features.AnalyticsModule.Behavior;
using Features.AnalyticsModule.Core;
using UnityEngine;
using Zenject;

namespace Features.AnalyticsModule.Binding
{
    public sealed class AnalyticsInstall : MonoInstaller<AnalyticsInstall>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<AnalyticsBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo(GetControllerType()).AsSingle();
        }

        private Type GetControllerType()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WebGLPlayer:
                    return typeof(AnalyticsWebController);

                default:
                    return typeof(AnalyticsDeviceController);
            }
        }
    }
}