using UnityEngine;
using UnityEngine.Events;
using System.Collections ;


[System.Serializable]
public class ManaHandler
{
    public UnityEvent<int, int> OnValueChanged;

    private Character owner;

    [SerializeField] private int maxMana;
    [SerializeField] private int currentMana;

    public int MaxMana { get => maxMana; }
    public int CurrentMana { get => currentMana; }

    public void Setup(Character character)
    {
        owner = character;
        maxMana = owner.Stats.BaseMaxMana + Mathf.RoundToInt(owner.Stats.MaxMana() * owner.Stats.BaseMaxMana);
        currentMana = maxMana;
        owner.StartCoroutine(RegenLoop());
        OnValueChanged?.Invoke(maxMana, currentMana);
    }

    private void ClampMana()
    {
        maxMana = owner.Stats.BaseMaxMana + Mathf.RoundToInt(owner.Stats.MaxMana() * owner.Stats.BaseMaxMana);
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        OnValueChanged?.Invoke(maxMana, currentMana);
    }

    public void IncreaseMaxMana(int amount, bool refill = false)
    {
        maxMana += amount;
        if (refill)
        {
            currentMana = maxMana;
        }
        ClampMana();
    }

    public bool CheckManaAvailable(int cost)
    {
        if (currentMana >= cost)
        {
            currentMana -= cost;
            ClampMana();
            return true;
        }
        return false;
    }


    private void RegenMana()
    {
        currentMana += Mathf.RoundToInt(maxMana * owner.Stats.ManaRegen());
        ClampMana();
    }

    private IEnumerator RegenLoop()
    {
        while (owner.gameObject.activeInHierarchy)
        {
            RegenMana();
            yield return new WaitForSeconds(1);
        }
    }

}
