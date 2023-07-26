using System.Collections;
using UnityEngine;

public class MaceAndShield : BasePlayerWeapon
{
    [SerializeField] private DamageDealingCollider PrimaryCollider;
    [SerializeField] private DamageDealingCollider SecondarCollider;
    [SerializeField] private ShieldCollider utilityCollider;
    [SerializeField, Range(1,10)] private int secondaryEffectDuration;
    [SerializeField, Range(0f, 1f)] private float SecondaryBuffValue;
    [SerializeField, Range(1,10)] private int TertiaryBuffDuration;
    [SerializeField, Range(0f,1f)] private float TertiaryBuffValue;
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
        GameManager.Instance.PlayerWrapper.SecondaryHandler.CacheWeaponData(secondaryAttack, SecondarCollider);
        GameManager.Instance.PlayerWrapper.TertiaryHandler.CacheWeaponData(tertiaryAttack);
        GameManager.Instance.PlayerWrapper.UtilityHandler.CacheWeaponData();
        GameManager.Instance.PlayerWrapper.UtilityHandler.OnActionPerfomed.AddListener(UtilityOn);
        GameManager.Instance.PlayerWrapper.UtilityHandler.OnActionCancled.AddListener(UtilityOff);
    }

    public override void Tertiary()
    {
        base.Tertiary();
        GameManager.Instance.PlayerWrapper.Damageable.Heal(new DamageHandler() { BaseAmount = tertiaryAttack.BaseDamage });
        GameManager.Instance.PlayerWrapper.Effectable.AddStatus(new DamageBuff(TertiaryBuffDuration, TertiaryBuffValue), GameManager.Instance.PlayerWrapper.DamageDealer);
    }

    private IEnumerator SecondaryEffect()
    {
        int counter = 0;
        GameManager.Instance.PlayerWrapper.Effectable.AddStatus(new DefenseBuff(secondaryEffectDuration, SecondaryBuffValue), GameManager.Instance.PlayerWrapper.DamageDealer);
        while (counter < secondaryEffectDuration)
        {
            SecondaryColliderOn();
            Transform newvfx = GameManager.Instance.ObjectPoolsHandler.PaladinSecondaryVFXPool.GetPooledObject();
            newvfx.position = transform.position;
            Transform parent = newvfx.parent;
            newvfx.SetParent(transform);
            newvfx.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            newvfx.SetParent(parent);
            counter++;
        }
    }

    private void UtilityOn()
    {
        GameManager.Instance.PlayerWrapper.PlayerAnim.SetBool("Block", true);
        GameManager.Instance.PlayerWrapper.CanAttack = false;
        utilityCollider.gameObject.SetActive(true);
    }
    private void UtilityOff()
    {
        GameManager.Instance.PlayerWrapper.PlayerAnim.SetBool("Block", false);
        GameManager.Instance.PlayerWrapper.CanAttack = true;
        utilityCollider.gameObject.SetActive(false);
    }
}
