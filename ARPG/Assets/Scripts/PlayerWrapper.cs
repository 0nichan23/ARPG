using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINK.Controller;

public class PlayerWrapper : Character
{
    [SerializeField] private PrimaryAttackHandler playerPrimaryAttackHandler;
    [SerializeField] private SecondaryAttackHandler playerSecondaryAttackHandler;
    [SerializeField] private TertiaryAttackHandler playerTertiaryAttackHandler;
    [SerializeField] private UtilityHandler playerUtilityHandler;
    [SerializeField] private BasePlayerClass currentClass;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private TopDownWASDController controller;
    private PlayerHud playerHud;
    public bool CanAttack = true;
    public PrimaryAttackHandler PlayerPrimaryAttackHandler { get => playerPrimaryAttackHandler;}
    public SecondaryAttackHandler PlayerSecondaryAttackHandler { get => playerSecondaryAttackHandler; }
    public TertiaryAttackHandler PlayerTertiaryAttackHandler { get => playerTertiaryAttackHandler; }
    public UtilityHandler PlayerUtilityHandler { get => playerUtilityHandler; }
    public BasePlayerClass CurrentClass { get => currentClass;  }
    public Animator PlayerAnim { get => playerAnim; }
    public PlayerHud PlayerHud { get => playerHud;}
    public TopDownWASDController Controller { get => controller; }

    protected override void Awake()
    {
        GameManager.Instance.CachePlayer(this);
        Stats.SetBaseStats(currentClass.BaseStats);
        base.Awake();
    }

    public void CachePlayerHud(PlayerHud hud)
    {
        playerHud = hud;
        ManaHandler.OnValueChanged.AddListener(playerHud.ManaBar.UpdateBar);
        Damageable.OnValueChanged.AddListener(playerHud.HealthBar.UpdateBar);
    }

    [ContextMenu("test stats")]
    private void TestStats()
    {
        Stats.AddCritHit(150);
        Stats.AddCritDamage(150);
    }

}
