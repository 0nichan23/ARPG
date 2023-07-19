using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    [SerializeField] private AttackHandler playerAttackHandler;

    public AttackHandler PlayerAttackHandler { get => playerAttackHandler;}
}
