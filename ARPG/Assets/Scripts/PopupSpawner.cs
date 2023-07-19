using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumberMesh damagePopPrefab;
    [SerializeField] private DamageNumberMesh missPopup;
    [SerializeField] private DamageNumberMesh criticalDamagePopPrefab;

    public void SpawnDamagePopup(Vector3 pos, int amount)
    {
        damagePopPrefab.Spawn(pos, amount, Color.red);
    }
    public void SpawnMissPopup(Vector3 pos)
    {
        missPopup.Spawn(pos);
    }

    public void SpawnCritDamagePopup(Vector3 pos, int amount)
    {
        criticalDamagePopPrefab.Spawn(pos, amount, Color.yellow);
    }

    public void SpawnHealPopup(Vector3 pos, int amount)
    {
        damagePopPrefab.Spawn(pos, amount, Color.green);
    }
}
