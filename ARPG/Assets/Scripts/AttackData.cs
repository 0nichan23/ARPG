using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{
    [SerializeField] private int baseDamage;
    [SerializeField] private Element element;
    public int BaseDamage { get => baseDamage; }
    public Element Element { get => element; }
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