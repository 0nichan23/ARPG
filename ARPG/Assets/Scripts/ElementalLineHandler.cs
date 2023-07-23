using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalLineHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer rend;
    [SerializeField] private ElementalObjectHandler elementalObject;

    public LineRenderer Rend { get => rend;}
    public ElementalObjectHandler ElementalObject { get => elementalObject;}
}
