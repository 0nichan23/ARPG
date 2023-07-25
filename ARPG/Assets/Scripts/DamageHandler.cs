using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageHandler
{
    [SerializeField] private float baseAmount;
    private Element element;
    private List<float> mods = new List<float>();
    private List<int> flatMods = new List<int>();
    public List<float> Mods { get => mods; }
    public float BaseAmount { get => baseAmount; set => baseAmount = value; }
    public Element Element { get => element;}

    public void Imbue(Element givenElement)
    {
        element = givenElement;
    }

    public void AddMod(float mod)
    {
        mods.Add(mod);
    }
    public void AddFlat(int mod)
    {
        flatMods.Add(mod);
    }

    public void ClearMods()
    {
        mods.Clear();
        flatMods.Clear();
    }

    public float CalcFinalDamageMult()
    {
        float amount = baseAmount;
        foreach (var item in mods)
        {
            if (item == 0)
            {
                amount = 0;
                break;
            }
            else if (item >= 1)
            {
                amount += (item * BaseAmount) - BaseAmount;//add damage
            }
            else
            {
                amount -= BaseAmount - (item * BaseAmount);//reduce damage
            }
        }
        foreach (var item in flatMods)
        {
            if (item < 0)
            {
                Debug.LogError("ey");
            }
            amount += item;
        }
        return Mathf.Clamp(amount, 0 ,amount);
    }


}
