using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrimaryAttackHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private List<AttackData> primaryCombo = new List<AttackData>();
    private DamageDealingCollider primaryCollider;
    public UnityEvent<AttackData> OnPrimaryAttackPerformed;
    private int comboCounter;

    public int ComboCounter { get => comboCounter;  }

    public void CacheWeaponData(List<AttackData> combo, DamageDealingCollider collider = null)
    {
        primaryCombo = new List<AttackData>(combo);
        primaryCollider = collider;
    }

    void Update()
    {
        if (GameManager.Instance.PlayerWrapper.CanAttack && Input.GetMouseButton(0))
        {
            Primary();
        }
    }

    private void Primary()
    {
        if (comboCounter >= primaryCombo.Count)
        {
            comboCounter = 0;
        }
        if (!ReferenceEquals(primaryCollider, null))
        {
            primaryCollider.CacheAttack(primaryCombo[comboCounter]);
        }
        OnPrimaryAttackPerformed?.Invoke(primaryCombo[comboCounter]);
        anim.SetTrigger("Primary");
        anim.SetInteger("ComboIndex", comboCounter);
        comboCounter++;
        if (comboCounter >= primaryCombo.Count)
        {
            comboCounter = 0;
        }
    }
}


