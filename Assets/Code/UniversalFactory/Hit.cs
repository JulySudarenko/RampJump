using System;
using UnityEngine;

namespace Code.UniversalFactory
{
    public sealed class Hit : MonoBehaviour
    {
        public event Action<int, int> OnHit;
        public event Action<int, int> OnHitStay;
        public event Action<int, int> OnHitEnd;

        private void OnCollisionEnter(Collision other)
        {
            OnHit?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
        }

        private void OnCollisionStay(Collision other)
        {
            OnHitStay?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());

            var contactPoints = other.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                var hitPoint = contactPoints[i].thisCollider.transform.position;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            OnHitEnd?.Invoke(other.gameObject.GetInstanceID(), gameObject.GetInstanceID());
        }
    }
}
