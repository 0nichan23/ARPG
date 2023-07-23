using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalObjectHandler : MonoBehaviour
{
    [SerializeField] private List<ElementalObject> elementalPrefabs = new List<ElementalObject>();

    private void OnDisable()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void ElementalObjectsOff()
    {
        foreach (var item in elementalPrefabs)
        {
            item.obj.SetActive(false);
        }
    }

    public ElementalObject ElementalObjectOn(Element element)
    {
        foreach (var item in elementalPrefabs)
        {
            item.obj.SetActive(false);
            if (item.element == element)
            {
                item.obj.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public List<ElementalObject> ElementalPrefabs { get => elementalPrefabs; }
}
