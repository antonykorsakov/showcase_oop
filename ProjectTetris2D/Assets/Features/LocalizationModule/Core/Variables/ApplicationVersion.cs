using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.Core.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Features.LocalizationModule.Core.Variables
{
    [DisplayName("Tony - Application version")]
    public class ApplicationVersion : IVariable
    {
        public object GetSourceValue(ISelectorInfo selector) => Application.version;
    }
}