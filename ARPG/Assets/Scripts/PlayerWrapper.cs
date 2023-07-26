using BLINK.Controller;
using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PrimaryAttackHandler playerPrimaryAttackHandler;
    [SerializeField] private PlayerActionHandler secondaryHandler;
    [SerializeField] private PlayerActionHandler tertiaryHandler;
    [SerializeField] private PlayerActionHandler utilityHandler;
    [SerializeField] private DashHandler playerDashHandler;
    [SerializeField] private BasePlayerClass currentClass;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private TopDownWASDController controller;
    [SerializeField] private PlayerDash playerDash;
    private PlayerHud playerHud;
    public bool CanAttack = true;
    public bool CanDash = true;
    public PrimaryAttackHandler PlayerPrimaryAttackHandler { get => playerPrimaryAttackHandler; }
    public BasePlayerClass CurrentClass { get => currentClass; }
    public Animator PlayerAnim { get => playerAnim; }
    public PlayerHud PlayerHud { get => playerHud; }
    public TopDownWASDController Controller { get => controller; }
    public DashHandler PlayerDashHandler { get => playerDashHandler; }
    public PlayerActionHandler SecondaryHandler { get => secondaryHandler; }
    public PlayerActionHandler TertiaryHandler { get => tertiaryHandler; }
    public PlayerActionHandler UtilityHandler { get => utilityHandler; }

    protected override void Awake()
    {
        GameManager.Instance.CachePlayer(this);
        Stats.SetBaseStats(currentClass.BaseStats);
        playerDashHandler.DashPerformed.AddListener(PlayerDash);
        playerDash.OnDashStart.AddListener(Lock);
        playerDash.OnDashEnd.AddListener(Unlock);
        base.Awake();
    }

    private void PlayerDash(Vector3 dir)
    {
        playerDash.StartDash(dir);
    }

    public void CachePlayerHud(PlayerHud hud)
    {
        playerHud = hud;
        ManaHandler.OnValueChanged.AddListener(playerHud.ManaBar.UpdateBar);
        Damageable.OnValueChanged.AddListener(playerHud.HealthBar.UpdateBar);
    }

    public void Lock()
    {
        CanAttack = false;
        controller.movementEnabled = false;
        CanDash = false;
    }

    public void Unlock()
    {
        CanAttack = true;
        controller.movementEnabled = true;
        CanDash = true;
    }

    [ContextMenu("test stats")]
    private void TestStats()
    {
        Stats.AddCritHit(150);
        Stats.AddCritDamage(150);
        Stats.AddCDR(200);
        Stats.AddManaCostDiscount(200);
    }

}
