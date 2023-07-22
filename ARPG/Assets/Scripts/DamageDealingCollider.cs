using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private bool blink = true;
    [SerializeField] private int activeFrames;
    public UnityEvent OnHit;
    private AttackData currentAttack;
    public bool blocked;

    public AttackData CurrentAttack { get => currentAttack; }

    private void OnEnable()
    {
        if (blink)
        {
            StartCoroutine(TurnOff());
        }
    }
    private void OnDisable()
    {
        blocked = false;
    }

    public void CacheOwner(Character givenCharacter)
    {
        owner = givenCharacter;
    }

    private IEnumerator TurnOff()
    {
        int counter = 0;
        while (counter < activeFrames)
        {
            counter++;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }


    public void CacheAttack(AttackData givenAttack)
    {
        currentAttack = givenAttack;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        if (blocked || ReferenceEquals(target, null) || ReferenceEquals(currentAttack, null))
        {
            return;
        }
        target.Damageable.GetHit(currentAttack, owner.DamageDealer);
        OnHit?.Invoke();
    }

}
