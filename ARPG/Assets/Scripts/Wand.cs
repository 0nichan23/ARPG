using System.Collections;
using UnityEngine;

public class Wand : BasePlayerWeapon
{
    [SerializeField] private Element element;
    [SerializeField] private DamageDealingCollider secondaryAttackCollider;
    [SerializeField] private ElementalObjectHandler secondaryEffect;
    [SerializeField] private float tertiaryRange;
    [SerializeField] private Transform blastPoint;
    [SerializeField] private LayerMask tertiaryLayer;
    [SerializeField] private float tertiaryIntervals;
    private float lastTertiaryTick;
    private bool tertiaryDown;
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
        GameManager.Instance.PlayerWrapper.PlayerTertiaryAttackHandler.OnTeritiaryCanceled.AddListener(() => tertiaryDown = false);
        GameManager.Instance.PlayerWrapper.PlayerUtilityHandler.CacheWeaponData();
    }

    public override void Tertiary()
    {
        base.Tertiary();
        tertiaryDown = true;
        StartCoroutine(StartTertiaryBeam(TertiaryAttack));
    }

    private IEnumerator StartTertiaryBeam(AttackData attack)
    {
        while (tertiaryDown)
        {
            Vector3 direction = GameManager.Instance.PlayerWrapper.Controller.GetPoint() - transform.position;
            RaycastHit hit;
            Physics.Raycast(blastPoint.position, direction.normalized, out hit, tertiaryRange, tertiaryLayer, QueryTriggerInteraction.Ignore);
            if (!ReferenceEquals(hit.collider, null))
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                if (!ReferenceEquals(target, null))
                {
                    target.Damageable.GetHit(attack, GameManager.Instance.PlayerWrapper.DamageDealer);
                }
            }
            yield return new WaitForSeconds(tertiaryIntervals);
        }
    }
}
