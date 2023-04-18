// COPYRIGHT BY WAN MUHAMAD AMIR BIN WAN ISA - wanamirisa@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using Lean.Pool;

namespace Wan.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] Transform firePoint = null;
        [SerializeField] LeanGameObjectPool bulletPool = null;
        LineRenderer laser = null;
        [SerializeField] LayerMask layerMask;

        XRGrabInteractable grabInteractable = null;

        private void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
            laser = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            grabInteractable.selectEntered.AddListener(OnSelectWeapon);
            grabInteractable.selectExited.AddListener(OnDeselectWeapon);
            grabInteractable.activated.AddListener(OnTriggerWeapon);
        }

        private void Start()
        {
            laser.enabled = false;
            laser.positionCount = 2;
        }

        public void OnSelectWeapon(SelectEnterEventArgs eventArgs)
        {
            laser.enabled = true;
        }

        void OnDeselectWeapon(SelectExitEventArgs eventArgs)
        {
            laser.enabled = false;
        }

        public void OnTriggerWeapon(ActivateEventArgs eventArgs)
        {
            bulletPool.Spawn(firePoint.position, Quaternion.Euler(firePoint.forward));
        }

        private void Update()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hitInfo, 100f, layerMask))
            {
                laser.SetPosition(0, firePoint.position);
                laser.SetPosition(1, hitInfo.point);
            }
            else
            {
                laser.SetPosition(0, firePoint.position);
                laser.SetPosition(1, firePoint.position + (firePoint.forward * 100f));
            }
        }
    }
}