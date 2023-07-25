using System.Collections.Generic;
using UnityEngine;

public class BasePlayerWeapon : MonoBehaviour
{
    [SerializeField] protected List<AttackData> primaryCombo = new List<AttackData>();
    [SerializeField] protected AttackData secondaryAttack;
    [SerializeField] protected AttackData tertiaryAttack;
    [SerializeField] protected RuntimeAnimatorController animController;
    [SerializeField] protected Element element;
    [SerializeField] private bool imbueAttacks;


    public List<AttackData> PrimaryCombo { get => primaryCombo; }
    public AttackData SecondaryAttack { get => secondaryAttack; }
    public AttackData TertiaryAttack { get => tertiaryAttack; }
    public RuntimeAnimatorController AnimController { get => animController; }
    protected Element Element { get => element; set => element = value; }

    private void Start()
    {
        if (imbueAttacks)
        {
            GameManager.Instance.PlayerWrapper.DamageDealer.OnHit.AddListener(ImbueDamage);
        }
    }

    public virtual void Primary()
    {

    }


    public virtual void Secondary()
    {

    }

    //invoke tertiary
    public virtual void Tertiary()
    {

    }

    //invoke utility
    public virtual void Utility()
    {

    }

    public virtual void CacheWeaponOnHandlers()
    {
    }

    private void ImbueDamage(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.Imbue(Element);
    }

}
