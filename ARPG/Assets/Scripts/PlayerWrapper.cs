using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private AttackHandler playerAttackHandler;

    public AttackHandler PlayerAttackHandler { get => playerAttackHandler;}
}
