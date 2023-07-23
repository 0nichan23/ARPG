using UnityEngine;

public class Wand : BasePlayerWeapon
{
    [SerializeField] private Element element;
    [SerializeField] private DamageDealingCollider secondaryAttackCollider;
    [SerializeField] private ElementalObjectHandler secondaryEffect;
    [SerializeField] private Transform blastPoint;
    public override void Primary()
    {
        base.Primary();
        Projectile projectile = GameManager.Instance.ObjectPoolsHandler.WizardSmallWandProjectilePool.GetPooledObject();
        projectile.transform.position = blastPoint.position;
        projectile.Collider.CacheOwner(GameManager.Instance.PlayerWrapper);
        projectile.Collider.CacheAttack(primaryCombo[GameManager.Instance.PlayerWrapper.PlayerPrimaryAttackHandler.ComboCounter]);
        projectile.gameObject.SetActive(true);
        Vector3 point = GameManager.Instance.PlayerWrapper.Controller.GetPoint();
        projectile.transform.eulerAngles = transform.eulerAngles;
        Vector3 direction = point - transform.position;
        projectile.Fire(direction.normalized);
    }

    public override void Secondary()
    {
        base.Secondary();
        secondaryAttackCollider.gameObject.SetActive(true);
        secondaryEffect.ElementalObjectOn(secondaryAttack.Element);
    }


    public override void CacheWeaponOnHandlers()
    {
        foreach (var item in primaryCombo)
        {
            item.Imbue(element);
        }
        secondaryAttack.Imbue(element);
        tertiaryAttack.Imbue(element);
        GameManager.Instance.PlayerWrapper.PlayerPrimaryAttackHandler.CacheWeaponData(PrimaryCombo);
        GameManager.Instance.PlayerWrapper.PlayerSecondaryAttackHandler.CacheWeaponData(secondaryAttack, secondaryAttackCollider);
        GameManager.Instance.PlayerWrapper.PlayerTertiaryAttackHandler.CacheWeaponData(tertiaryAttack);
        GameManager.Instance.PlayerWrapper.PlayerUtilityHandler.CacheWeaponData();
    }

}
