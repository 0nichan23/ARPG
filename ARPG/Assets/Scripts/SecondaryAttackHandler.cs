using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondaryAttackHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private AttackData refAttack;
    private DamageDealingCollider secondaryCollider;
    public UnityEvent<AttackData> OnSecondaryAttackPerformed;
    private int comboCounter;

    public void CacheWeaponData(AttackData attack, DamageDealingCollider collider = null)
    {
        refAttack = attack;
        secondaryCollider = collider;
    }

    void Update()
    {
        if (GameManager.Instance.PlayerWrapper.CanAttack && Input.GetMouseButtonDown(1))
        {
            Secondary();
        }
    }

    private void Secondary()
    {
        if (!ReferenceEquals(secondaryCollider, null))
        {
            secondaryCollider.CacheAttack(refAttack);
        }
        OnSecondaryAttackPerformed?.Invoke(refAttack);
        anim.SetTrigger("Secondary");
    }
}
