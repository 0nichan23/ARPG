using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField, Range(1, 4)] private int comboLength;
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
        if (comboCounter >= comboLength)
        {
            comboCounter = 0;
        }
        anim.SetTrigger("Primary");
        anim.SetInteger("ComboIndex", comboCounter);
    }
}
