using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private int activeFrames;
    private AttackData currentAttack;

    private void OnEnable()
    {
        StartCoroutine(TurnOff());
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
        Debug.Log("collider hit");
        Character target = other.GetComponent<Character>();
        if (ReferenceEquals(target, null) || ReferenceEquals(currentAttack, null))
        {
            return;
        }
        target.Damageable.GetHit(currentAttack, owner.DamageDealer);
    }

}
