using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public sealed class Hit : MonoBehaviour
    {
        public event Action<int, int> OnHit;
        public event Action<Vector3> OnSlime;

        private void OnCollisionEnter(Collision other)
        {
            OnHit?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
            
            var contactPoints = other.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                var hitPoint = contactPoints[i].thisCollider.transform.position;
                OnSlime?.Invoke(hitPoint);
                Debug.Log(hitPoint);
            }
        }
    }
}
