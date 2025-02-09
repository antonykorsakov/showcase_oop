using System;
using Cysharp.Threading.Tasks;

namespace Features.JsonModule.Core
{
    public sealed class JsonWebController : JsonController
    {
        public JsonWebController(IJsonModuleConfig config) : base(config)
        { }

        protected override async UniTask<string> LoadFromFileAsync(string fileName)
        {
            try
            {
                string json = LoadFromLocalStorage(fileName);
                return await UniTask.FromResult(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override async UniTask SaveToFileAsync(string fileName, string json)
        {
            try
            {
                SaveToLocalStorage(fileName, json);
                await UniTask.CompletedTask;
            }
            catch (Exception)
            {
                // nothing
            }
        }

#if UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern string LoadFromLocalStorage(string key);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void SaveToLocalStorage(string key, string jsonData);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void MyLog(string value);
#else
        private string LoadFromLocalStorage(string key) => null;
        private void SaveToLocalStorage(string key, string jsonData)
        { }
        private void MyLog(string value)
        { }
#endif
    }
}