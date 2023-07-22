using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent<Damageable, AttackData, DamageDealer, DamageHandler> OnHit;

    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnDealCritDamage;

    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnDealDamageFinal;

    public UnityEvent<StatusEffect, Effectable, DamageDealer> OnApplyStatus;

    public UnityEvent<Damageable, DamageDealer, AttackData> OnKill;

    private Character refCharacter;

    public Character RefCharacter { get => refCharacter; }

    public void SetUp(Character givenCharacter)
    {
        refCharacter = givenCharacter;
        OnHit.AddListener(ScaleDamage);
        OnHit.AddListener(AddArmorPen);
        OnHit.AddListener(AddMagicPen);
        OnDealCritDamage.AddListener(AddCritDamage);
    }

    private void ScaleDamage(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {
        int flatMod = refCharacter.Stats.GetAttributeAttackValue(attack.ScalingFactor, attack.ScalingAttribute);
        dmg.AddFlat(flatMod);
    }

    private void AddCritDamage(AttackData attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(refCharacter.Stats.CritDamage());
    }

    private void AddArmorPen(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (attack.Element == Element.Physical)
        {
            dmg.AddMod(refCharacter.Stats.ArmorPen(target.RefCharacter));
        }
    }
    private void AddMagicPen(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (attack.Element != Element.Physical)
        {
            dmg.AddMod(refCharacter.Stats.MagicPen(target.RefCharacter));
        }
    }

    public bool CheckForCritHit()
    {
        if (Random.Range(0,100) <= refCharacter.Stats.CritHit())
        {
            return true;
        }
        return false;
    }
}
