using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private DamageDealingCollider collider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
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
