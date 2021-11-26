using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public class Hit : MonoBehaviour
    {
        public event Action<int> OnHit;

        private void OnCollisionEnter(Collision other)
        {
            OnHit?.Invoke(other.gameObject.GetInstanceID());

            Debug.Log(other.gameObject.GetInstanceID());
            Debug.Log(other.gameObject.name);
            Debug.Log("***");
        }
    }
}
