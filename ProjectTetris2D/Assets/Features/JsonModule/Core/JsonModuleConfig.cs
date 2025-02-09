using UnityEngine;

namespace Features.JsonModule.Core
{
    [CreateAssetMenu(fileName = nameof(JsonModuleConfig),
        menuName = "Configs/" + nameof(JsonModuleConfig), order = 'J')]
    public sealed class JsonModuleConfig : ScriptableObject, IJsonModuleConfig
    {
        [SerializeField] private string _statisticsFileName = "statistics.json";
        [SerializeField] private int _endlessLoopProtection = 10;

        public string StatisticsFileName => _statisticsFileName;
        public int EndlessLoopProtection => _endlessLoopProtection;
    }
}