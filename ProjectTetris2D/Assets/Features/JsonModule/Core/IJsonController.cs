using System;
using Cysharp.Threading.Tasks;

namespace Features.JsonModule.Core
{
    public interface IJsonController
    {
        event Action<ErrorType, string, string> LoadErrorEvent;
        event Action<ErrorType, string, string> SaveErrorEvent;

        UniTask<TData> TryLoad<TData>(string fileName);
        void TrySave<TData>(string fileName, Func<TData> getter);
    }
}