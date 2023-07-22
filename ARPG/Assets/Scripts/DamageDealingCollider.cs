using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private int activeFrames;
    private AttackData currentAttack;
    public bool blocked;
    private void OnEnable()
    {
        StartCoroutine(TurnOff());
    }
    private void OnDisable()
    {
        blocked = false;
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
        if (blocked|| ReferenceEquals(target, null) || ReferenceEquals(currentAttack, null))
        {
            return;
        }
        target.Damageable.GetHit(currentAttack, owner.DamageDealer);
    }

}
