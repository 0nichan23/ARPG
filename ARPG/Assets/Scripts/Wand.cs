using System.Collections;
using UnityEngine;

public class Wand : BasePlayerWeapon
{
    [SerializeField] private AttackData mineAttack;
    [SerializeField] private DamageDealingCollider secondaryAttackCollider;
    [SerializeField] private ElementalObjectHandler secondaryEffect;
    [SerializeField] private ElementalObjectHandler tertiaryEffect;
    [SerializeField] private float tertiaryRange;
    [SerializeField] private Transform blastPoint;
    [SerializeField] private LayerMask tertiaryLayer;
    [SerializeField] private float tertiaryIntervals;
    [SerializeField] private Transform minePlace;
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
        projectile.Fire(direction.normalized, element);
    }

    public override void Secondary()
    {
        base.Secondary();
        secondaryAttackCollider.gameObject.SetActive(true);
        secondaryEffect.ElementalObjectOn(Element);
    }

    public override void CacheWeaponOnHandlers()
    {
        GameManager.Instance.PlayerWrapper.PlayerPrimaryAttackHandler.CacheWeaponData(PrimaryCombo);
        GameManager.Instance.PlayerWrapper.SecondaryHandler.CacheWeaponData(secondaryAttack, secondaryAttackCollider);
        GameManager.Instance.PlayerWrapper.TertiaryHandler.CacheWeaponData(tertiaryAttack);
        GameManager.Instance.PlayerWrapper.TertiaryHandler.OnActionPerfomed.AddListener(() => tertiaryDown = true);
        GameManager.Instance.PlayerWrapper.TertiaryHandler.OnActionCancled.AddListener(() => tertiaryDown = false);
        GameManager.Instance.PlayerWrapper.UtilityHandler.CacheWeaponData(mineAttack);
    }

    public override void Tertiary()
    {
        base.Tertiary();
        StartCoroutine(TertiaryBeam(TertiaryAttack));
    }

    private IEnumerator TertiaryBeam(AttackData attack)
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 point = GameManager.Instance.PlayerWrapper.Controller.GetPoint();
        ElementalObject ej = tertiaryEffect.ElementalObjectOn(element);
        while (tertiaryDown)
        {
            GameManager.Instance.PlayerWrapper.Controller.movementEnabled = false;
            ej.renderer.SetPosition(0, blastPoint.position);
            Vector3 direction = new Vector3(point.x - transform.position.x, 0, point.z - transform.position.z).normalized;
            RaycastHit hit;
            Physics.Raycast(blastPoint.position, direction, out hit, tertiaryRange, tertiaryLayer, QueryTriggerInteraction.Ignore);
            if (!ReferenceEquals(hit.collider, null))
            {
                ej.renderer.SetPosition(1, hit.point);
                Character target = hit.collider.gameObject.GetComponent<Character>();
                if (!ReferenceEquals(target, null))
                {
                    target.Damageable.GetHit(attack, GameManager.Instance.PlayerWrapper.DamageDealer);
                }
            }
            else
            {
                Vector3 maxVector = blastPoint.position + (direction * tertiaryRange);
                ej.renderer.SetPosition(1, maxVector);
            }
            if (!GameManager.Instance.PlayerWrapper.ManaHandler.CheckManaAvailable(Mathf.RoundToInt(tertiaryAttack.ManaCost * GameManager.Instance.PlayerWrapper.Stats.ManaCostDiscount())))
            {
                break;
            }
            yield return new WaitForSeconds(tertiaryIntervals);
        }
        tertiaryEffect.ElementalObjectsOff();
        GameManager.Instance.PlayerWrapper.Controller.movementEnabled = true;
        GameManager.Instance.PlayerWrapper.PlayerAnim.SetTrigger("BeamDone");
    }

    public override void Utility()
    {
        base.Utility();
        Mine mine = GameManager.Instance.ObjectPoolsHandler.MinePool.GetPooledObject();
        mine.transform.position = minePlace.position;
        mine.CacheOwner(GameManager.Instance.PlayerWrapper, mineAttack);
        mine.OnDetonate.AddListener(ElementalExplosion);
        mine.gameObject.SetActive(true);
    }

    private void ElementalExplosion(Mine mine)
    {
        ElementalObjectHandler handler = GameManager.Instance.ObjectPoolsHandler.ExplosionsPool.GetPooledObject();
        handler.transform.position = mine.transform.position;
        handler.gameObject.SetActive(true);
        handler.ElementalObjectOn(Element);

    }

}
