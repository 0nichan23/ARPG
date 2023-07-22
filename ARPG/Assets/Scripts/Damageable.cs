using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    private Character refCharacter;

    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnGetHit;
    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnTakeCriticalDamage;
    public UnityEvent<AttackData, Damageable, DamageDealer, DamageHandler> OnTakeDamageFinal;
    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamageGFX;
    public UnityEvent<DamageHandler> OnHeal;
    public UnityEvent OnHealGFX;

    public bool EmitPopups;
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }
    public Character RefCharacter { get => refCharacter; }

    public void SetUp(Character givenCharacter)
    {
        refCharacter = givenCharacter;
        OnGetHit.AddListener(ApplyResistance);
        OnHeal.AddListener(ApplyHealPower);
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
            dealer.OnKill?.Invoke(this, dealer);
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
                dealer.OnKill?.Invoke(this, dealer);
            }
        }
        ClampHp();
        OnTakeDamageGFX?.Invoke();
    }

    public void HealTrueDamage(float fixedAmount)
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
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
    }

    public void Heal(DamageHandler givenDamage)
    {
        OnHeal?.Invoke(givenDamage);
        currentHp += givenDamage.CalcFinalDamageMult();
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnHealPopup(transform.position, Mathf.RoundToInt(givenDamage.CalcFinalDamageMult()));
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }


}

