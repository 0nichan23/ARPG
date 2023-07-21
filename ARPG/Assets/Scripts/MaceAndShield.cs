using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceAndShield : BasePlayerWeapon
{
    [SerializeField] private DamageDealingCollider PrimaryCollider;
    [SerializeField] private DamageDealingCollider SecondarCollider;
    [SerializeField] private RuntimeAnimatorController shieldUpAnim;
    [SerializeField] private int secondaryEffectDuration;
    public override void Primary()
    {
        base.Primary();
        PrimaryColliderOn();
    }

    public override void Secondary()
    {
        base.Secondary();
        StartCoroutine(SecondaryEffect());
        //secondary effect, add damage redcution buff
    }

    private void PrimaryColliderOn()
    {
        PrimaryCollider.gameObject.SetActive(true);
    }

    private void SecondaryColliderOn()
    {
        SecondarCollider.gameObject.SetActive(true);
    }

    public override void CacheWeaponOnHandlers()
    {
        GameManager.Instance.PlayerWrapper.PlayerPrimaryAttackHandler.CacheWeaponData(PrimaryCombo, PrimaryCollider);
        GameManager.Instance.PlayerWrapper.PlayerSecondaryAttackHandler.CacheWeaponData(secondaryAttack, SecondarCollider);
        //add secondary later in this case 
    }

    private IEnumerator SecondaryEffect()
    {
        int counter = 0;
        while (counter < secondaryEffectDuration)
        {
            SecondaryColliderOn();
            Transform newvfx =  GameManager.Instance.ObjectPoolsHandler.PaladinSecondaryVFXPool.GetPooledObject();
            newvfx.position = transform.position;
            Transform parent = newvfx.parent;
            newvfx.SetParent(transform);
            newvfx.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            newvfx.SetParent(parent);
            counter++;
        }
    }
}
