using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Features.JsonModule.Behavior
{
    public class Qqqq : IDisposable
    {
        public Qqqq()
        {
            Application.quitting += Xxx;
        }

        ~Qqqq()
        {
            ReleaseUnmanagedResources();
        }

        private void Xxx()
        {
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }


    public class Xxxx : MonoBehaviour
    {
        [SerializeField] private RawImage _imageComponent;

        private CancellationTokenSource _ttt;
        private Action _action;
        private bool _actionState;

        private void Start()
        {
            Setup().Forget();
        }

        private bool _xxx;

        private async UniTask WaitEvent(CancellationToken token)
        {
            Application.quitting += SetXxx;

            try
            {
                while (!_xxx)
                {
                    await UniTask.Yield(token);
                }
            }
            finally
            {
                Application.quitting -= SetXxx;
                _xxx = false;
            }
        }

        private void SetXxx() => _xxx = true;


        private async UniTaskVoid Setup()
        {
            Texture image = null; // await LoadTextureAsync("...", _ttt.Token);
            if (image == null)
            {
                Debug.LogError("Problem");
                return;
            }

            _imageComponent.texture = image;
        }


        public class PoolManager<T> where T : new()
        {
            private readonly Dictionary<string, Stack<T>> _pools = new();

            public void ReturnToPool(string id, T poolObject)
            {
                if (string.IsNullOrWhiteSpace(id))
                    return;

                Stack<T> items;

                if (!_pools.ContainsKey(id))
                {
                    items = new Stack<T>();
                    _pools[id] = items;
                }
                else
                {
                    items = _pools[id];
                }

                items.Push(poolObject);
            }

            public T GetFromPool(string id)
            {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException();

                if (!_pools.ContainsKey(id))
                    return new T();

                var items = _pools[id];
                var item = items.Pop();
                return item;
            }
        }
    }
}