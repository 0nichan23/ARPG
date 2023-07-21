using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PrimaryAttackHandler playerPrimaryAttackHandler;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.CachePlayer(this);
    }

    public PrimaryAttackHandler PlayerPrimaryAttackHandler { get => playerPrimaryAttackHandler;}
}
