using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UEditor = UnityEditor.Editor;

namespace Features.LocalizationModule.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LocalizationText))]
    public class LocalizationTextEditor : UEditor
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
        private const string DefaultLocaleKeyName = "UNDEFINED";
        private const string CommonLocaleText = "<{0}>";

        private static readonly List<Type> ComponentsOrder = new()
        {
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(CanvasGroup),
            typeof(LocalizationText),
            typeof(LocalizeStringEvent),
            typeof(TextMeshProUGUI),
        };

        private GUIStyle _fixButtonStyle;
        private GUIStyle FixButtonStyle => _fixButtonStyle ??= MakeGreenButtonStyle();


        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Fix text-object", FixButtonStyle))
            {
                foreach (var item in targets)
                    FixItem(item);
            }
        }

        private void FixItem(UnityEngine.Object item)
        {
            var targetComp = item as LocalizationText;
            if (targetComp == null)
                return;

            var localeComp = targetComp.GetComponent<LocalizeStringEvent>();
            if (localeComp == null)
                return;

            var textComp = targetComp.GetComponent<TextMeshProUGUI>();
            if (textComp == null)
                return;

            var componentsOrder = MakeCurrentComponentsOrder(targetComp.gameObject, ComponentsOrder);
            SortComponents(targetComp.gameObject, componentsOrder);
            SetEvents(localeComp, textComp);
            SetText(localeComp, textComp);
            SetTextSettings(textComp);
        }

        private List<Type> MakeCurrentComponentsOrder(GameObject targetObject, List<Type> exampleComponentsOrder)
        {
            var componentsOrder = new List<Type>();
            foreach (var selectType in exampleComponentsOrder)
            {
                var component = targetObject.GetComponent(selectType);
                if (component != null)
                    componentsOrder.Add(selectType);
            }

            return componentsOrder;
        }

        private void SortComponents(GameObject targetObject, List<Type> componentsOrder)
        {
            var components = targetObject.GetComponents<Component>();
            for (int index = 1; index < componentsOrder.Count; index++)
            {
                var targetType = componentsOrder[index];
                var targetComponent = Array.Find(components, item => item.GetType() == targetType);
                if (targetComponent == null)
                    continue;

                var currentIndex = Array.IndexOf(components, targetComponent);
                var delta = currentIndex - index;
                if (delta == 0)
                    continue;

                while (delta > 0)
                {
                    ComponentUtility.MoveComponentUp(targetComponent);
                    delta--;
                }

                while (delta < 0)
                {
                    ComponentUtility.MoveComponentDown(targetComponent);
                    delta++;
                }

                components = targetObject.GetComponents<Component>();
            }
        }

        // source method idea:
        // https://discussions.unity.com/t/attempting-to-set-the-update-string-of-the-localize-string-event-using-a-script/259495
        private void SetEvents(LocalizeStringEvent localeComp, TextMeshProUGUI textComp,
            UnityEventCallState eventState = UnityEventCallState.RuntimeOnly)
        {
            var textPropertyName = nameof(textComp.text);
            var textPropertyInfo = textComp.GetType().GetProperty(textPropertyName);
            if (textPropertyInfo == null)
                return;

            var setMethod = textPropertyInfo.GetSetMethod();
            var setMethodDelegate = Delegate.CreateDelegate(typeof(UnityAction<string>), textComp, setMethod);
            var setMethodUnityDelegate = setMethodDelegate as UnityAction<string>;

            // clear old
            var persistentEventsCount = localeComp.OnUpdateString.GetPersistentEventCount();
            for (int index = persistentEventsCount - 1; index >= 0; index--)
                UnityEventTools.RemovePersistentListener(localeComp.OnUpdateString, index);

            // add new
            UnityEventTools.AddPersistentListener(localeComp.OnUpdateString, setMethodUnityDelegate);
            localeComp.OnUpdateString.SetPersistentListenerState(0, eventState);
        }

        private void SetText(LocalizeStringEvent localeComp, TextMeshProUGUI textComp)
        {
            var newText = string.Format(CommonLocaleText, GetKey(localeComp.StringReference));
            if (textComp.text == newText)
                return;

            textComp.text = newText;
            EditorUtility.SetDirty(textComp);
        }

        private void SetTextSettings(TextMeshProUGUI textComp)
        {
            if (textComp.textWrappingMode == TextWrappingModes.NoWrap)
                return;

            textComp.textWrappingMode = TextWrappingModes.NoWrap;
            EditorUtility.SetDirty(textComp);
        }

        private string GetKey(LocalizedString stringReference)
        {
            var tableReference = stringReference.TableReference;
            var tableEntryReference = stringReference.TableEntryReference;

            // for internal property 'TableReference.SharedTableData'
            var propertyName = "SharedTableData";
            var propertyInfo = typeof(TableReference).GetProperty(propertyName, BindingAttr);
            if (propertyInfo == null)
                return DefaultLocaleKeyName;

            var sharedTableData = propertyInfo.GetValue(tableReference) as SharedTableData;
            if (sharedTableData == null)
                return DefaultLocaleKeyName;

            switch (tableEntryReference.ReferenceType)
            {
                case TableEntryReference.Type.Name:
                    return tableEntryReference.Key;

                case TableEntryReference.Type.Id:
                    var id = tableEntryReference.KeyId;
                    var key = sharedTableData.GetKey(id);
                    return key;

                case TableEntryReference.Type.Empty:
                default:
                    return DefaultLocaleKeyName;
            }
        }

        private static GUIStyle MakeGreenButtonStyle()
        {
            var style = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = EditorGUIUtility.singleLineHeight * 2,
                normal =
                {
                    background = MakeTex(2, 2, new Color(0.1f, 0.4f, 0.1f)),
                },
            };

            return style;
        }

        private static Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pixels);
            result.Apply();
            return result;
        }
    }
}