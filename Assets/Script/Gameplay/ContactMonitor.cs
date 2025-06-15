using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ContactMonitor : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private List<Collider> _contacts = new List<Collider>();

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public bool IsOnValidSurface()
        {
            foreach (var contact in _contacts)
                if (contact.gameObject.CompareTag("Block") || contact.gameObject.CompareTag("Base"))
                    return true;

            return false;
        }

        public void ClearContacts()
        {
            _contacts.Clear();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_contacts.Contains(collision.collider))
                _contacts.Add(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            _contacts.Remove(collision.collider);
        }
    }
}
