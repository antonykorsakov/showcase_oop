using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Features.LogModule.Core
{
    public static class FactoryAsync
    {
        public static event Action ClonedItemErrorEvent;
        public static event Action CancelledEvent;
        public static event Action UnityOperationErrorEvent;

        public static async UniTask<T> InstantiateAsync<T>(T original, Transform parent, CancellationToken cancellationToken)
            where T : Component
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var zeroPos = Vector3.zero;
                var zeroTurn = Quaternion.identity;
                var result = await UObject.InstantiateAsync(original, 1, parent, zeroPos, zeroTurn, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (result == null)
                {
                    ClonedItemErrorEvent?.Invoke();
                    return null;
                }

                if (result.Length != 1)
                {
                    ClonedItemErrorEvent?.Invoke();
                    return null;
                }

                var item = result[0];
                if (item == null)
                {
                    ClonedItemErrorEvent?.Invoke();
                    return null;
                }

                item.transform.localPosition = original.transform.localPosition;
                item.transform.localRotation = original.transform.localRotation;
                return item;
            }
            catch (OperationCanceledException)
            {
                CancelledEvent?.Invoke();
                return null;
            }
            catch (Exception ex)
            {
                UnityOperationErrorEvent?.Invoke();
                return null;
            }
        }
    }
}