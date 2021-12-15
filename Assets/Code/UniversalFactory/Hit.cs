using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public sealed class Hit : MonoBehaviour
    {
        public event Action<int, int> OnHit;
        public event Action<Vector3> OnHitStay;
        public event Action<int, int> OnHitEnd;

        private void OnCollisionEnter(Collision other)
        {
            OnHit?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
        }

        private void OnCollisionStay(Collision other)
        {
            var contactPoints = other.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                var hitPoint = contactPoints[i].thisCollider.transform.position;
                OnHitStay?.Invoke(hitPoint);
                Debug.Log(hitPoint);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            OnHitEnd?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
        }
    }
}
