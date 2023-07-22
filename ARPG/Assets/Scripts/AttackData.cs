using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int coolDown;
    [SerializeField] private Element element;
    [SerializeField] private Attribute scalingAttribute;
    [SerializeField, Range(0.1f, 10f)] private float scalingFactor;
    [SerializeField] private int manaCost;


    public void Imbue(Element givenElement)
    {
        element = givenElement;
    }

    public int BaseDamage { get => baseDamage; }
    public Element Element { get => element; }
    public int CoolDown { get => coolDown; }
    public Attribute ScalingAttribute { get => scalingAttribute; }
    public float ScalingFactor { get => scalingFactor; }
    public int ManaCost { get => manaCost; }
}


public enum Element
{
    Physical,
    Fire,
    Electric,
    Ice,
    Nature,
    Holy,
    Shadow
}

public enum Attribute
{
    Strengh,
    Dexterity,
    Intelligence,
    Faith,
    Wits,
    Regen
}