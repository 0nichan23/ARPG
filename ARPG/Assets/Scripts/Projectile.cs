using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [SerializeField] private DamageDealingCollider collider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private List<ProjectileElement> elementalPrefabs = new List<ProjectileElement>();
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
        foreach (var item in elementalPrefabs)
        {
            item.obj.SetActive(false);
            if (item.element == collider.CurrentAttack.Element)
            {
                item.obj.SetActive(true);
            }
        }
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
public class ProjectileElement
{
    public Element element;
    public GameObject obj;
}

