// COPYRIGHT BY WAN MUHAMAD AMIR BIN WAN ISA - wanamirisa@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Pool;

namespace Wan
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] float speed = 100f;
        Rigidbody rb = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            // Shoot(transform.forward);
        }

        public void Shoot(Vector3 direction)
        {
            rb.AddForce(direction * speed, ForceMode.Force);
        }
    }
}