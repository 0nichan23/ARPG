using DamageNumbersPro;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumberMesh damagePopPrefab;
    [SerializeField] private DamageNumberMesh missPopup;
    [SerializeField] private DamageNumberMesh criticalDamagePopPrefab;
    [SerializeField] private List<ElementPopupColor> elementColors = new List<ElementPopupColor>();

    public void SpawnDamagePopup(Vector3 pos, int amount, Element element)
    {
        damagePopPrefab.Spawn(pos, amount, GetColorFromElement(element));
    }
    public void SpawnCritDamagePopup(Vector3 pos, int amount, Element element)
    {
        criticalDamagePopPrefab.Spawn(pos, amount.ToString() + "!", GetColorFromElement(element));
    }

    public void SpawnHealPopup(Vector3 pos, int amount)
    {
        damagePopPrefab.Spawn(pos, amount, Color.green);
    }

    public Color GetColorFromElement(Element element)
    {
        foreach (var item in elementColors)
        {
            if (item.element == element)
            {
                return item.color;
            }
        }
        return Color.white;
    }

}

[System.Serializable]
public struct ElementPopupColor
{
    public Element element;
    public Color color;
}
