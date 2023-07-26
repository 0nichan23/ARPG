using UnityEngine;
using UnityEngine.Events;

public class PlayerActionHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private KeyCode key;
    [SerializeField] private bool rightClick;
    [SerializeField] private string animTrigger;
    private AttackData refAttack;
    private DamageDealingCollider damageCollider;
    private float lastUsed;
    public UnityEvent OnActionPerfomed;
    public UnityEvent OnActionCancled;

    public void CacheWeaponData(AttackData attack = null, DamageDealingCollider collider = null)
    {
        if (!ReferenceEquals(attack, null))
        {
            refAttack = attack;
            lastUsed = attack.CoolDown * -1;
        }
        if (!ReferenceEquals(collider, null))
        {
            damageCollider = collider;
        }
    }

    private bool CheckCoolDown()
    {
        if (ReferenceEquals(refAttack, null))
        {
            return true;
        }
        if (Time.time - lastUsed >= (refAttack.CoolDown * GameManager.Instance.PlayerWrapper.Stats.CDR()))
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if (GameManager.Instance.PlayerWrapper.CanAttack && CheckCoolDown() && CheckInput())
        {
            Action();
        }
        else if (CheckInputUp())
        {
            OnActionCancled?.Invoke();
        }
    }
    private bool CheckInput()
    {
        if (rightClick && Input.GetMouseButton(1))
        {
            return true;
        }
        else if (Input.GetKey(key))
        {
            return true;
        }
        return false;
    }

    private bool CheckInputUp()
    {
        if (rightClick && Input.GetMouseButtonUp(1))
        {
            return true;
        }
        else if (Input.GetKeyUp(key))
        {
            return true;
        }
        return false;
    }

    private void Action()
    {
        if (!ReferenceEquals(refAttack, null) && !GameManager.Instance.PlayerWrapper.ManaHandler.CheckManaAvailable
           (Mathf.RoundToInt(refAttack.ManaCost * GameManager.Instance.PlayerWrapper.Stats.ManaCostDiscount())))
        {
            return;
        }
        if (!ReferenceEquals(damageCollider, null) && !ReferenceEquals(refAttack, null))
        {
            damageCollider.CacheAttack(refAttack);
        }
        OnActionPerfomed?.Invoke();
        lastUsed = Time.time;
        anim.SetTrigger(animTrigger);
    }
}
