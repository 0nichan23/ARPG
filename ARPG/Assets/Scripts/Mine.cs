using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour
{
    [SerializeField] private DamageDealingCollider damageCollider;

    public UnityEvent<Mine> OnDetonate;
    public DamageDealingCollider DamageCollider { get => damageCollider;  }


    private void OnDisable()
    {
        OnDetonate.RemoveAllListeners();
    }

    public void CacheOwner(Character owner, AttackData attack)
    {
        damageCollider.CacheOwner(owner);
        damageCollider.CacheAttack(attack);
    }

    [ContextMenu("Detonate")]
    public void Detonate()
    {
        damageCollider.gameObject.SetActive(true);
        OnDetonate?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        damageCollider.gameObject.SetActive(true);
        OnDetonate?.Invoke(this);
    }




}
