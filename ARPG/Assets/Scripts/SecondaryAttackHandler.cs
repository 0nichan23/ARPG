using UnityEngine;
using UnityEngine.Events;

public class SecondaryAttackHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private AttackData refAttack;
    private DamageDealingCollider secondaryCollider;
    public UnityEvent<AttackData> OnSecondaryAttackPerformed;
    private float lastUsed;


    public void CacheWeaponData(AttackData attack, DamageDealingCollider collider = null)
    {
        refAttack = attack;
        secondaryCollider = collider;
        lastUsed = attack.CoolDown * -1;
    }


    private bool CheckCoolDown()
    {
        if (Time.time - lastUsed >= (refAttack.CoolDown * GameManager.Instance.PlayerWrapper.Stats.CDR()))
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (GameManager.Instance.PlayerWrapper.CanAttack && CheckCoolDown() && Input.GetMouseButtonDown(1))
        {
            Secondary();
        }
    }

    private void Secondary()
    {
        if (!GameManager.Instance.PlayerWrapper.ManaHandler.CheckManaAvailable
            (Mathf.RoundToInt(refAttack.ManaCost * GameManager.Instance.PlayerWrapper.Stats.ManaCostDiscount())))
        {
            return;
        }
        if (!ReferenceEquals(secondaryCollider, null))
        {
            secondaryCollider.CacheAttack(refAttack);
        }
        OnSecondaryAttackPerformed?.Invoke(refAttack);
        lastUsed = Time.time;
        anim.SetTrigger("Secondary");
    }
}
