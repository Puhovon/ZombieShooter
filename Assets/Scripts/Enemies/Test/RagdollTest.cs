using System;
using UnityEngine;

namespace Enemies.Test
{
    public class RagdollTest : MonoBehaviour
    {
        [SerializeField] private CharacterJoint head;
        [SerializeField] private Collider[] ragdollColliders;
        [SerializeField] private Rigidbody[] ragdollRigidbodies;
        [SerializeField] private Rigidbody mainRb;
        private void Awake()
        {
            // for (int i = 0; i < ragdollColliders.Length; i++)
            // {
            //     Physics.IgnoreCollision(ragdollColliders[i], mainCollider);
            //     ragdollRigidbodies[i].useGravity = false;
            //     ragdollColliders[i].enabled = false;
            // }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < ragdollColliders.Length; i++)
                {
                    Debug.Log("Active");
                    ragdollColliders[i].enabled = true;
                    ragdollRigidbodies[i].isKinematic = false;
                }
            }
        }
    }
}