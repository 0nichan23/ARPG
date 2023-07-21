using System.Collections;
using UnityEngine;

public class MaceAndShield : BasePlayerWeapon
{
    [SerializeField] private DamageDealingCollider PrimaryCollider;
    [SerializeField] private DamageDealingCollider SecondarCollider;
    [SerializeField] private ShieldCollider utilityCollider;
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
        GameManager.Instance.PlayerWrapper.PlayerUtilityHandler.CacheWeaponData();
        GameManager.Instance.PlayerWrapper.PlayerUtilityHandler.OnUtilityPerformed.AddListener(UtilityOn);
        GameManager.Instance.PlayerWrapper.PlayerUtilityHandler.OnUtilityUp.AddListener(UtilityOff);
        //add secondary later in this case 
    }

    private IEnumerator SecondaryEffect()
    {
        int counter = 0;
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
