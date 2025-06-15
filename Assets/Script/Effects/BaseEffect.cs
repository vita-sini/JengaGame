using Gameplay;
using GameRoot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public abstract class BaseEffect : MonoBehaviour
    {
        [SerializeField] protected AudioClip effectSound;
        [SerializeField] private GameEvents _gameEvents;

        protected BlockRegistry _blockRegistry;
        protected Coroutine effectCoroutine;
        protected AudioSource audioSource;

        protected virtual void Awake()
        {
            _gameEvents.TurnEnd += Stop;
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void OnDisable()
        {
            _gameEvents.TurnEnd -= Stop;
        }

        protected virtual void OnDestroy()
        {
            _gameEvents.TurnEnd -= Stop;
        }

        public virtual void Execute()
        {
            if (effectCoroutine != null)
                StopCoroutine(effectCoroutine);

            effectCoroutine = StartCoroutine(EffectCoroutine());
        }

        public virtual void Stop()
        {
            if (effectCoroutine != null)
            {
                StopCoroutine(effectCoroutine);
                effectCoroutine = null;
            }

            if (audioSource != null)
                audioSource.Stop();
        }

        protected virtual void PlayEffectSound(bool loop = false)
        {
            if (audioSource != null && effectSound != null)
            {
                audioSource.clip = effectSound;
                audioSource.loop = loop;
                audioSource.Play();
            }
        }

        protected virtual IEnumerable<GameObject> GetBlocks()
        {
            return _blockRegistry?.PlacedBlocks ?? new List<GameObject>();
        }

        protected abstract IEnumerator EffectCoroutine();

        public void InitEffect(BlockRegistry blockRegistry)
        {
            _blockRegistry = blockRegistry;
        }
    }
}

