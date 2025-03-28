using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour, IEffect
{
    [SerializeField] private int _numberOfMagneticBlocks; // Количество магнитных блоков
    [SerializeField] private int _strengthGap; // сила разрыва
    [SerializeField] private AudioClip _magneticSound;

    private List<FixedJoint> _magneticJoints = new List<FixedJoint>();
    private Coroutine _magneticEffectCoroutine;
    private AudioSource _audioSource;

    private void Awake()
    {
        GameEvents.OnTurnEnd += Stop;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        GameEvents.OnTurnEnd -= Stop;
    }

    public void Execute()
    {
        if (_magneticEffectCoroutine != null)
        {
            StopCoroutine(_magneticEffectCoroutine);
        }

        _magneticEffectCoroutine = StartCoroutine(MagneticEffectCoroutine());
    }

    public void Stop()
    {
        if (_magneticEffectCoroutine != null)
        {
            StopCoroutine(_magneticEffectCoroutine);
            _magneticEffectCoroutine = null;
        }

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }

        ResetBlocks();
    }

    private IEnumerator MagneticEffectCoroutine()
    {
        if (_audioSource != null && _magneticSound != null)
        {
            _audioSource.clip = _magneticSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length > 0)
        {
            for (int i = 0; i < _numberOfMagneticBlocks; i++)
            {
                int randomIndex = Random.Range(0, blocks.Length);
                GameObject randomBlock = blocks[randomIndex];
                Debug.Log(blocks[randomIndex]);
                Rigidbody rb = randomBlock.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    CreateMagneticJoint(rb);
                }
            }
        }

        yield return null;
    }

    private void CreateMagneticJoint(Rigidbody rb)
    {
        Vector3 direction = GetRandomDirection();
        RaycastHit hit;

        if (Physics.Raycast(rb.transform.position, direction, out hit, 10f))
        {
            Rigidbody otherRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (otherRigidbody != null && hit.collider.CompareTag("Block"))
            {
                FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRigidbody;
                joint.breakForce = _strengthGap; // сила разрыва
                _magneticJoints.Add(joint);
            }
        }
    }

    private Vector3 GetRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            case 0:
                return Vector3.left;
            case 1:
                return Vector3.right;
            case 2:
                return Vector3.up;
            case 3:
                return Vector3.down;
            default:
                return Vector3.zero;
        }
    }

    private void ResetBlocks()
    {
        foreach (var joint in _magneticJoints)
        {
            if (joint != null)
            {
                Destroy(joint);
            }
        }

        _magneticJoints.Clear();
    }
}
