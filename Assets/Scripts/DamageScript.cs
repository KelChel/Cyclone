using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 10;
    Collider damageCollider;


    private void Start() {
        damageCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider hitTarget)
    {
        if (!hitTarget.transform.TryGetComponent<IDamagable>(out IDamagable target))
          return;
        target.TakeDamage(damageAmount);
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }
    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

}
