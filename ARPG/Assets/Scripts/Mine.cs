using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private DamageDealingCollider damageCollider;

    public DamageDealingCollider DamageCollider { get => damageCollider;  }

    public void CacheOwner(Character owner, AttackData attack)
    {
        damageCollider.CacheOwner(owner);
        damageCollider.CacheAttack(attack);
    }
    private void OnTriggerEnter(Collider other)
    {
        damageCollider.gameObject.SetActive(true);
    }
}
