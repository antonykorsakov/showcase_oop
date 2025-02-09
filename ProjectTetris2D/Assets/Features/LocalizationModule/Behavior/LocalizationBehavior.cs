using Features.LogModule.Core;
using Features.UiViewMainScreenModule.Core.Controller;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Features.LocalizationModule.Behavior
{
    public class LocalizationBehavior : IInitializable
    {
        [Inject] private IUiViewMainScreenController UiViewMainScreenController { get; }

        public void Initialize()
        {
            UiViewMainScreenController.LanguageButtonClickEvent += () =>
            {
                var locales = LocalizationSettings.AvailableLocales.Locales;
                if (locales.Count <= 1)
                    return;

                int currentIndex = locales.IndexOf(LocalizationSettings.SelectedLocale);
                int nextIndex = (currentIndex + 1) % locales.Count;

                Log.Print($"Change locale: {locales[currentIndex]} -> {locales[nextIndex]};");
                LocalizationSettings.SelectedLocale = locales[nextIndex];
            };
        }
    }
}