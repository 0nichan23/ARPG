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
        OnHit.AddListener(AddPen);
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

    private void AddPen(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {//penetration is detemined by the scaling attribute of the attack, if the attack scales with either str or dex apply armor pen else apply magic pen
        if (attack.ScalingAttribute == Attribute.Dexterity || attack.ScalingAttribute == Attribute.Strengh)
        {
            dmg.AddMod(refCharacter.Stats.ArmorPen(target.RefCharacter));
        }
        else
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
