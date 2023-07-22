using System.Collections;
using UnityEngine;

public class DamageBuff : StatusEffect
{
    private float amount;
    //increases all damage done
    public DamageBuff(float duration, float extraDamage)
    {
        effectOri = EffectOrientation.POS;
        this.duration = duration;
        amount = extraDamage;
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        host.StartCoroutine(Timer());
        host.DamageDealer.OnHit.AddListener(DamageIncrease);
    }

    protected override void UnSubscribe()
    {
        base.UnSubscribe();
        host.DamageDealer.OnHit.RemoveListener(DamageIncrease);
    }

    public override void Reset()
    {
        base.Reset();
        counter = 0;
    }

    private IEnumerator Timer()
    {
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.Effectable.RemoveStatus(this);
    }

    protected virtual void DamageIncrease(Damageable target, AttackData attack, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(1 + amount);
    }
}
