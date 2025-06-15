using Gameplay;
using GameRoot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class ExplosiveEffect : BaseEffect
    {
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDelay = 2f;

        private Rigidbody _targetBlock;

        public override void Execute()
        {
            base.Execute();

            List<Rigidbody> candidates = new();

            foreach (var block in GetBlocks())
            {
                if (block.TryGetComponent(out BlockState state) &&
                    block.TryGetComponent(out Rigidbody rb) &&
                    state.CurrentState == BlockState.State.Placed)
                {
                    candidates.Add(rb);
                }
            }

            if (candidates.Count > 0)
            {
                _targetBlock = candidates[Random.Range(0, candidates.Count)];
                StartCoroutine(ExplodeBlock());
            }
        }

        private IEnumerator ExplodeBlock()
        {
            yield return new WaitForSeconds(_explosionDelay);

            if (_targetBlock)
            {
                Collider[] affected = Physics.OverlapSphere(_targetBlock.transform.position, _explosionRadius);

                foreach (var col in affected)
                {
                    if (col.TryGetComponent(out Rigidbody rb) && rb != _targetBlock)
                        rb.AddExplosionForce(_explosionForce, _targetBlock.position, _explosionRadius);
                }

                if (_explosionPrefab)
                {
                    Instantiate(_explosionPrefab, _targetBlock.transform.position, Quaternion.identity);
                }

                if (audioSource && effectSound)
                    audioSource.PlayOneShot(effectSound);

                Destroy(_targetBlock.gameObject);
            }
        }

        protected override IEnumerator EffectCoroutine()
        {
            yield return null;
        }
    }
}
