using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaladinPassive : ClassPassive
{
    [SerializeField] private DamageDealingCollider blastCollider;
    [SerializeField] private AttackData blastAttack;
    private void Start()
    {
        blastCollider.CacheAttack(blastAttack);
    }
    public override void SubscribePassive()
    {
        owner.Effectable.OnObtainEffect.AddListener(InvokePassive);
    }
    public override void UnSubscribePassive()
    {
        owner.Effectable.OnObtainEffect.RemoveListener(InvokePassive);
    }

    public void InvokePassive(StatusEffect effect, Effectable target)
    {
        if (effect.EffectOri == EffectOrientation.POS)
        {
            blastCollider.gameObject.SetActive(true);
            Transform newvfx = GameManager.Instance.ObjectPoolsHandler.PaladinPassiveVFXPool.GetPooledObject();
            newvfx.position = transform.position;
            newvfx.gameObject.SetActive(true);
        }
    }

}
