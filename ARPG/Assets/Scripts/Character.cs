using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;
    [SerializeField] private CharacterStats stats;

    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer;}
    public Effectable Effectable { get => effectable;  }
    public CharacterStats Stats { get => stats;  }

    protected virtual void Awake()
    {
        damageable.SetUp(this);
        DamageDealer.SetUp(this);
        effectable.CahceOwner(this);
    }
}
