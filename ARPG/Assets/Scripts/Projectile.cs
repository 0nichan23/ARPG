using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [SerializeField] private DamageDealingCollider collider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private ElementalObjectHandler elementalObjectHandler;
    public DamageDealingCollider Collider { get => collider; }

    private void Awake()
    {
        collider.OnHit.AddListener(Blast);
    }
    private void OnEnable()
    {
        StartCoroutine(LifeTimeCount());
    }

    public void Fire(Vector3 direction)
    {
        rb.velocity = direction * speed;
        elementalObjectHandler.ElementalObjectOn(collider.CurrentAttack.Element);
    }

    public void Blast()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private IEnumerator LifeTimeCount()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
[System.Serializable]
public class ElementalObject
{
    public Element element;
    public GameObject obj;
}

