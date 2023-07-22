using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPassive : ClassPassive
{
    [SerializeField] private AttackData secondaryAttack;
    [SerializeField] private AttackData tertiaryAttack;
    [SerializeField] private int manaRestored;
    public override void SubscribePassive()
    {
        owner.DamageDealer.OnKill.AddListener(RestoreMana);
    }

    public override void UnSubscribePassive()
    {
        owner.DamageDealer.OnKill.RemoveListener(RestoreMana);
    }

    private void RestoreMana(Damageable target, DamageDealer dealer, AttackData attack)
    {
        if (!ReferenceEquals(attack, null))
        {
            if (ReferenceEquals(attack, secondaryAttack) || ReferenceEquals(attack, tertiaryAttack))
            {
                owner.ManaHandler.RestoreMana(manaRestored);
            }
        }
    }

}
