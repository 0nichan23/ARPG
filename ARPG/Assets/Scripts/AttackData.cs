using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{ 
    [SerializeField] private int baseDamage;

    public int BaseDamage { get => baseDamage;}
}
