using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PrimaryAttackHandler playerPrimaryAttackHandler;
    [SerializeField] private SecondaryAttackHandler playerSecondaryAttackHandler;
    [SerializeField] private UtilityHandler playerUtilityHandler;
    [SerializeField] private BasePlayerClass currentClass;
    [SerializeField] private Animator playerAnim;
    public bool CanAttack = true;
    public PrimaryAttackHandler PlayerPrimaryAttackHandler { get => playerPrimaryAttackHandler;}
    public SecondaryAttackHandler PlayerSecondaryAttackHandler { get => playerSecondaryAttackHandler; }
    public UtilityHandler PlayerUtilityHandler { get => playerUtilityHandler; }
    public BasePlayerClass CurrentClass { get => currentClass;  }
    public Animator PlayerAnim { get => playerAnim; }

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.CachePlayer(this);
    }

}
