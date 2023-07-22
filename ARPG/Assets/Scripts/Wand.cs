using UnityEngine;

public class Wand : BasePlayerWeapon
{
    [SerializeField] private Element element;
    [SerializeField] private DamageDealingCollider secondaryAttackCollider;
    [SerializeField] private Transform blastPoint;
    public override void Primary()
    {
        base.Primary();
        Projectile projectile = GameManager.Instance.ObjectPoolsHandler.WizardSmallWandProjectilePool.GetPooledObject();
        projectile.transform.position = blastPoint.position;
        projectile.Collider.CacheOwner(GameManager.Instance.PlayerWrapper);
        projectile.Collider.CacheAttack(primaryCombo[GameManager.Instance.PlayerWrapper.PlayerPrimaryAttackHandler.ComboCounter]);
        projectile.gameObject.SetActive(true);
        projectile.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y -90, 0);
        Vector3 direction = GameManager.Instance.PlayerWrapper.Controller.GetPoint() - transform.position;
        projectile.Fire(direction.normalized);
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
