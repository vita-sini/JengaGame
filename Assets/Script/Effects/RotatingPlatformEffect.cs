using GameRoot;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class RotatingPlatformEffect : BaseEffect
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _target;

        protected override IEnumerator EffectCoroutine()
        {
            PlayEffectSound(loop: true);

            while (true)
            {
                Camera.main.transform.RotateAround(_target.position, Vector3.up, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
