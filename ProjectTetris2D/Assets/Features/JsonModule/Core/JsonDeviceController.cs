using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Features.LogModule.Core;
using UnityEngine;

namespace Features.JsonModule.Core
{
    public sealed class JsonDeviceController : JsonController
    {
        public JsonDeviceController(IJsonModuleConfig config) : base(config)
        { }

        protected override async UniTask<string> LoadFromFileAsync(string fileName)
        {
            var filePath = GetFilePath(fileName);

            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                Log.Print($"Success loaded by path: {filePath};");
                return json;
            }
            catch (Exception)
            {
                // LoadErrorEvent?.Invoke(ErrorType.FileReadException, filePath, ex.Message);
                return null;
            }
        }

        protected override async UniTask SaveToFileAsync(string fileName, string json)
        {
            var filePath = GetFilePath(fileName);

            try
            {
                await File.WriteAllTextAsync(filePath, json);
                Log.Print($"Success saved by path: {filePath};");
            }
            catch (Exception)
            {
                // SaveErrorEvent?.Invoke(ErrorType.FileWriteException, filePath, ex.Message);
            }
        }

        private string GetFilePath(string fileName) => Path.Combine(Application.persistentDataPath, fileName);
    }
}