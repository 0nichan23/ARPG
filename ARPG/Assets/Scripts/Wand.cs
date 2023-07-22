using UnityEngine;

public class Wand : BasePlayerWeapon
{
    [SerializeField] private Element element;
    [SerializeField] private DamageDealingCollider secondaryAttackCollider;
    public override void Primary()
    {
        base.Primary();
        //get projectile, cahce, fire, all that
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
