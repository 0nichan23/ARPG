using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DamageDealingCollider dmgCollider = other.GetComponent<DamageDealingCollider>();
        if (!ReferenceEquals(dmgCollider, null))
        {
            dmgCollider.blocked = true;
        }
    }

}
