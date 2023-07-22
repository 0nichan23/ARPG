using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    [SerializeField] private int currentHp;
    [SerializeField] private int maxHp;
    private Character refCharacter;

    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnGetHit;
    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnTakeCriticalDamage;
    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnTakeDamageFinal;
    public UnityEvent<int, int> OnValueChanged;
    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamageGFX;
    public UnityEvent<DamageHandler> OnHeal;
    public UnityEvent OnHealGFX;

    public bool EmitPopups;
    public int MaxHp { get => maxHp; }
    public int CurrentHp { get => currentHp; }
    public Character RefCharacter { get => refCharacter; }

    public void SetUp(Character givenCharacter)
    {
        refCharacter = givenCharacter;
        maxHp = givenCharacter.Stats.BaseMaxHealth + Mathf.RoundToInt(givenCharacter.Stats.MaxHealth() * givenCharacter.Stats.BaseMaxHealth);
        currentHp = maxHp;
        OnGetHit.AddListener(ApplyResistance);
        OnHeal.AddListener(ApplyHealPower);
        StartCoroutine(RegenHealth());
    }

    private void ApplyResistance(AttackData attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (attack.Element == Element.Physical)
        {
            dmg.AddMod(refCharacter.Stats.Armor());
        }
        else
        {
            dmg.AddMod(refCharacter.Stats.MagicResist());
        }
    }

    private void ApplyHealPower(DamageHandler dmg)
    {
        dmg.AddMod(RefCharacter.Stats.HealPower());
    }



    public void IncreaseMaxHp(int amount, bool heal = false)
    {
        maxHp += amount;
        if (heal)
        {
            currentHp = MaxHp;
        }
    }

    public void GetHit(AttackData attack, DamageDealer dealer)
    {
        DamageHandler dmg = new DamageHandler() { BaseAmount = attack.BaseDamage };
        OnGetHit?.Invoke(attack, this, dealer, dmg);
        dealer.OnHit?.Invoke(this, attack, dealer, dmg);
        if (dealer.CheckForCritHit())
        {
            TakeDamage(attack, dealer, dmg, true);
        }
        else
        {
            TakeDamage(attack, dealer, dmg);
        }

    }

    public void TakeDamage(AttackData attack, DamageDealer dealer, DamageHandler dmg, bool critHit = false)
    {
        if (critHit)
        {
            OnTakeCriticalDamage?.Invoke(attack, this, dealer, dmg);
            dealer.OnDealCritDamage?.Invoke(attack, this, dealer, dmg);
        }
        OnTakeDamageFinal?.Invoke(attack, this, dealer, dmg);
        dealer.OnDealDamageFinal?.Invoke(attack, this, dealer, dmg);
        if (EmitPopups)
        {
            if (critHit)
            {
                GameManager.Instance.PopupSpawner.SpawnCritDamagePopup(transform.position, Mathf.RoundToInt(dmg.CalcFinalDamageMult()), attack.Element);
            }
            else
            {
                GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, Mathf.RoundToInt(dmg.CalcFinalDamageMult()), attack.Element);
            }
        }
        currentHp -= Mathf.RoundToInt(dmg.CalcFinalDamageMult());
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            dealer.OnKill?.Invoke(this, dealer, attack);
        }
        dmg.ClearMods();
        ClampHp();
        OnTakeDamageGFX?.Invoke();

    }

    public void TakeTrueDamage(float fixedAmount, Element element, DamageDealer dealer = null)
    {
        currentHp -= Mathf.RoundToInt(fixedAmount);
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, Mathf.RoundToInt(fixedAmount), element);
        }
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            if (!ReferenceEquals(dealer, null))
            {
                dealer.OnKill?.Invoke(this, dealer, null);
            }
        }
        ClampHp();
        OnTakeDamageGFX?.Invoke();
    }

    public void HealTrueDamage(int fixedAmount)
    {
        currentHp += fixedAmount;
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnHealPopup(transform.position, Mathf.RoundToInt(fixedAmount));
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }
    private void ClampHp()
    {
        maxHp = refCharacter.Stats.BaseMaxHealth + Mathf.RoundToInt(refCharacter.Stats.MaxHealth() * refCharacter.Stats.BaseMaxHealth);
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
        OnValueChanged?.Invoke(maxHp, currentHp);
    }

    public void Heal(DamageHandler givenDamage)
    {
        OnHeal?.Invoke(givenDamage);
        currentHp += Mathf.RoundToInt(givenDamage.CalcFinalDamageMult());
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnHealPopup(transform.position, Mathf.RoundToInt(givenDamage.CalcFinalDamageMult()));
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }

    private void RegenHp()
    {
        currentHp += Mathf.RoundToInt(MaxHp * refCharacter.Stats.HealthRegen());
        ClampHp();
    }

    private IEnumerator RegenHealth()
    {
        while (gameObject.activeInHierarchy)
        {
            RegenHp();
            yield return new WaitForSeconds(1);
        }
    }
}

