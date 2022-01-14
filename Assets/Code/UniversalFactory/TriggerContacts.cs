using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public sealed class TriggerContacts : MonoBehaviour
    {
        public event Action<int, int> IsContact;

        private void OnTriggerEnter(Collider other)
        {
            IsContact?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
        }
    }
}
