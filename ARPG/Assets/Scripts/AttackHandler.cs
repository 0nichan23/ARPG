using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AttackHandler : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private List<AttackData> combo = new List<AttackData>();
    [SerializeField] private DamageDealingCollider primaryCollider;
    public UnityEvent<AttackData> OnAttackPerformed;
    private int comboCounter;
    private bool canAttack = true;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    void Update()
    {
        if (CanAttack && Input.GetMouseButtonDown(0))
        {
            Primary();
        }
    }

    private void Primary()
    {
        comboCounter++;
        if (comboCounter >= combo.Count)
        {
            comboCounter = 0;
        }
        primaryCollider.CacheAttack(combo[comboCounter]);
        OnAttackPerformed?.Invoke(combo[comboCounter]);
        anim.SetTrigger("Primary");
        anim.SetInteger("ComboIndex", comboCounter);
    }
}


