using System.Collections;
using UnityEngine;

public class DefenseBuff : StatusEffect
{
    private float amount;
    //increases all damage done
    public DefenseBuff(float duration, float extraDamage)
    {
        effectOri = EffectOrientation.POS;
        this.duration = duration;
        amount = extraDamage;
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        host.StartCoroutine(Timer());
        host.Damageable.OnGetHit.AddListener(DamageDecrease);
    }

    protected override void UnSubscribe()
    {
        base.UnSubscribe();
        host.Damageable.OnGetHit.RemoveListener(DamageDecrease);
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

    protected virtual void DamageDecrease(AttackData attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(1 - amount);
    }
}
