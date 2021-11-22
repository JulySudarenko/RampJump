using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public class TriggerContacts : MonoBehaviour
    {
        public event Action<int> IsStayContact;

        private void OnTriggerStay(Collider other)
        {
            IsStayContact?.Invoke(other.gameObject.GetInstanceID());
        }
    }
}
