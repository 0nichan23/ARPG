using UnityEngine;

[CreateAssetMenu(fileName = "StatSheet", menuName = "StatSheet")]
public class StatSheet : ScriptableObject
{
    [SerializeField] private BaseStat str;
    [SerializeField] private BaseStat dex;
    [SerializeField] private BaseStat inte;
    [SerializeField] private BaseStat fth;
    [SerializeField] private BaseStat wts;
    [SerializeField] private BaseStat rgn;

    public BaseStat Str { get => str; }
    public BaseStat Dex { get => dex; }
    public BaseStat Inte { get => inte; }
    public BaseStat Fth { get => fth; }
    public BaseStat Wts { get => wts; }
    public BaseStat Rgn { get => rgn; }
}

[System.Serializable]
public struct BaseStat
{
    public Attribute Attribute;
    [Range(1, 10)] public int Value;
}
