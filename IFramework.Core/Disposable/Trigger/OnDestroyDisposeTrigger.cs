using System;
using System.Collections.Generic;
using UnityEngine;

namespace IFramework.Core.Trigger
{
    public class OnDestroyDisposeTrigger : MonoBehaviour
    {
        private HashSet<IDisposable> disposables = new HashSet<IDisposable>();

        public void AddDispose(IDisposable disposable)
        {
            if (!disposables.Contains(disposable)) {
                disposables.Add(disposable);
            }
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying) return;

            foreach (IDisposable disposable in disposables) {
                disposable.Dispose();
            }
            disposables.Clear();
            disposables = null;
        }
    }
}
