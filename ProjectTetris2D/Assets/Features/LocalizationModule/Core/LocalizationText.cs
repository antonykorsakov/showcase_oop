using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Features.LocalizationModule.Editor
{
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(LocalizeStringEvent))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    [DisallowMultipleComponent]
    public class LocalizationText : MonoBehaviour
    {
        // see code in UnityEditor
    }
}