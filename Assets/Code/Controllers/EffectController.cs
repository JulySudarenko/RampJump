using Code.Assistant;
using Code.Interfaces;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class EffectController : IInitialize, ICleanup
    {
        private readonly Transform _ball;
        private readonly Transform _particle;
        private Hit _hit;

        public EffectController(Transform ball, Transform particle)
        {
            _ball = ball;
            _particle = particle;
            _particle.gameObject.SetActive(false);
        }


        public void Initialize()
        {
            var colliderChild = _ball.GetComponentInChildren<SphereCollider>();
            _hit = colliderChild.gameObject.GetOrAddComponent<Hit>();

            _hit.OnSlime += ActivateEffect;
            //_hit.OnSlime += DeactivateEffect;
        }

        private void ActivateEffect(Vector3 vector)
        {
            _particle.transform.position = vector;
            _particle.gameObject.SetActive(true);
            Debug.Log("Collision");
        }

        // private void DeactivateEffect(Vector3 vector)
        // {
        //     _particle.gameObject.SetActive(false);
        //     Debug.Log("Collision Finish");
        // }

        public void Cleanup()
        {
            _hit.OnSlime -= ActivateEffect;
            //_hit.OnHitFinish -= DeactivateEffect;
        }
    }
}
