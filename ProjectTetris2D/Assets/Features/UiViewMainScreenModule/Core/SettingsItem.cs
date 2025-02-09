using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Features.UiViewMainScreenModule.Core
{
    public class SettingsItem : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent _titleTComp;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private TMP_Text _countTComp;
        [SerializeField] private TMP_Text _comingSoonTComp;
        [SerializeField] private Button _increaseButton;

        public TMP_Text CountTComp => _countTComp;

        public void Setup(LocalizedString title)
        {
            _titleTComp.StringReference = title;
            _countTComp.text = "X";

            _decreaseButton.onClick.RemoveAllListeners();
            _increaseButton.onClick.RemoveAllListeners();

            Switch(false);
        }

        public void Setup(LocalizedString title, int startValue, Action decreaseAction, Action increaseAction)
        {
            _titleTComp.StringReference = title;
            _countTComp.text = startValue.ToString();

            _decreaseButton.onClick.RemoveAllListeners();
            _increaseButton.onClick.RemoveAllListeners();
            _decreaseButton.onClick.AddListener(() => decreaseAction?.Invoke());
            _increaseButton.onClick.AddListener(() => increaseAction?.Invoke());

            Switch(true);
        }

        private void Switch(bool value)
        {
            _decreaseButton.gameObject.SetActive(value);
            _countTComp.gameObject.SetActive(value);
            _increaseButton.gameObject.SetActive(value);

            _comingSoonTComp.gameObject.SetActive(!value);
        }
    }
}