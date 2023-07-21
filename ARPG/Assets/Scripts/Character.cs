using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;

    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer;}
    public Effectable Effectable { get => effectable;  }


    private void Awake()
    {
        
    }
}
