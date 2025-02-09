using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Features.JsonModule.Core
{
    /// <summary>
    /// To avoid data loss, saving is only allowed after reading.
    /// If multiple save operations are received simultaneously, only the first and last will be executed
    /// (to avoid excess).
    /// </summary>
    public abstract class JsonController : IJsonController
    {
        private readonly IJsonModuleConfig _config;
        private readonly HashSet<string> _loadedFileNames = new();
        private readonly HashSet<string> _savingFilePaths = new();
        private readonly Dictionary<string, Delegate> _requests = new();

        protected JsonController(IJsonModuleConfig config)
        {
            // TODO: Assert not Null;
            _config = config;
        }

        public event Action<ErrorType, string, string> LoadErrorEvent;
        public event Action<ErrorType, string, string> SaveErrorEvent;

        public async UniTask<TData> TryLoad<TData>(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                LoadErrorEvent?.Invoke(ErrorType.InvalidFilePath, fileName, null);
                return default;
            }

            if (fileName.Contains("/") || fileName.Contains("\\"))
            {
                LoadErrorEvent?.Invoke(ErrorType.InvalidFilePath, fileName, null);
                return default;
            }

            if (_loadedFileNames.Contains(fileName))
            {
                LoadErrorEvent?.Invoke(ErrorType.TwiceLoaded, fileName, null);
                return default;
            }

            return await LoadAsync<TData>(fileName);
        }

        /// <summary>
        /// Can work only after call <see cref="TryLoad{TData}"/> with this <see cref="fileName"/>
        /// </summary>
        public void TrySave<TData>(string fileName, Func<TData> getter)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                SaveErrorEvent?.Invoke(ErrorType.InvalidFilePath, fileName, null);
                return;
            }

            if (fileName.Contains("/") || fileName.Contains("\\"))
            {
                SaveErrorEvent?.Invoke(ErrorType.InvalidFilePath, fileName, null);
                return;
            }

            if (!_loadedFileNames.Contains(fileName))
            {
                SaveErrorEvent?.Invoke(ErrorType.WasNoFileReading, fileName, null);
                return;
            }

            _requests[fileName] = getter;
            bool executingSave = _savingFilePaths.Contains(fileName);
            if (executingSave)
                return;

            SaveAsync<TData>(fileName).Forget();
        }

        private async UniTask<TData> LoadAsync<TData>(string fileName)
        {
            string json = await LoadFromFileAsync(fileName);
            TData data = !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<TData>(json)
                : default;

            _loadedFileNames.Add(fileName);
            return data;
        }

        private async UniTaskVoid SaveAsync<T>(string fileName)
        {
            _savingFilePaths.Add(fileName);

            int loopIteration = 0;
            while (_requests.ContainsKey(fileName))
            {
                var request = _requests[fileName] as Func<T>;
                _requests.Remove(fileName);

                if (loopIteration >= _config.EndlessLoopProtection)
                {
                    SaveErrorEvent?.Invoke(ErrorType.PotentialLoopOverflow, fileName, null);
                    break;
                }

                if (request == null)
                {
                    SaveErrorEvent?.Invoke(ErrorType.TODO, fileName, null);
                    break;
                }

                var data = request.Invoke();
                var json = JsonConvert.SerializeObject(data);

                await SaveToFileAsync(fileName, json);

                loopIteration++;
            }

            _savingFilePaths.Remove(fileName);
        }

        protected abstract UniTask<string> LoadFromFileAsync(string fileName);
        protected abstract UniTask SaveToFileAsync(string fileName, string json);
    }
}