using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //stats are between 1 and 100 (with a base stat of 10 at level 10)

    [SerializeField] private int baseMaxHealth;
    [SerializeField] private int baseMaxMana;
    private BaseStat str;
    private BaseStat dex;
    private BaseStat inte;
    private BaseStat fth;
    private BaseStat wts;
    private BaseStat rgn;

    public int STR => str.Value;
    public int DEX => dex.Value;
    public int INT => inte.Value;
    public int FTH => fth.Value;
    public int WTS => wts.Value;
    public int RGN => rgn.Value;

    public int BaseMaxHealth { get => baseMaxHealth; }
    public int BaseMaxMana { get => baseMaxMana; }

    private int staggerChance;
    private int armorPen;//
    private int magicPen;//
    private int manaRegen;
    private int hpRegen;
    private int maxMana;
    private int maxHp;
    private int healPower;
    private int cdr;
    private int manaCost;
    private int armor;//
    private int magicResistance;//
    private int critHit;//
    private int critDamage;//

    public void SetBaseStats(StatSheet baseStats)
    {
        str = baseStats.Str;
        dex = baseStats.Dex;
        inte = baseStats.Inte;
        fth = baseStats.Fth;
        wts = baseStats.Wts;
        rgn = baseStats.Rgn;
    }

    //attacks recieve damage from attributes 
    //flat amount to add = atr value => value/base damage => base damage + ans
    //14  = 70% str, 80 base damage => 

    //adding damage in the usual way is still the same, will probably work with things like critical hits and other passive effects.

    public int GetAttributeAttackValue(float atrScale /* 0.5 would result in a damage increase equal to 50% of the atr*/, Attribute atr)
    {
        switch (atr)
        {
            case Attribute.Strengh:
                return Mathf.RoundToInt(STR * atrScale);
            case Attribute.Dexterity:
                return Mathf.RoundToInt(DEX * atrScale);
            case Attribute.Intelligence:
                return Mathf.RoundToInt(INT * atrScale);
            case Attribute.Faith:
                return Mathf.RoundToInt(FTH * atrScale);
            case Attribute.Wits:
                return Mathf.RoundToInt(WTS * atrScale);
            case Attribute.Regen:
                return Mathf.RoundToInt(RGN * atrScale);
        }
        return 0;
    }

    public float StaggerChance()
    {
        //100 str = 50% stagger
        return Mathf.Clamp((0.5f * (STR + staggerChance)), 0, 100);
    }

    public void AddStaggerChance(int amount)
    {
        staggerChance += amount;
    }

    public float ArmorPen(Character target)
    {
        //100 str shreds 50% armor => 40% damage reduction will result in a 20% damage increase
        return Mathf.Clamp(((1 - target.Stats.Armor()) / 2) * ((STR + armorPen) / 100) + 1, 1, 2f);
    }

    public void AddArmorPen(int amount)
    {
        armorPen += amount;
    }

    public float Armor()
    {
        //100 wits block 75% physical damage
        return Mathf.Clamp((100 - ((0.75f * (WTS + armor)))) / 100, 0.1f, 1);
    }

    public void AddArmor(int amount)
    {
        armor += amount;
    }


    public float MagicPen(Character target)
    {
        return Mathf.Clamp(((1 - target.Stats.MagicResist()) / 2) * ((INT + magicPen) / 100) + 1, 1, 2f);
    }

    public void AddMagicPen(int amount)
    {
        magicPen += amount;
    }

    public float MagicResist()
    {
        //100 regen block 75% magic damage
        return Mathf.Clamp((100 - ((0.75f * (RGN + magicResistance)))) / 100, 0.1f, 1);
    }

    public void AddMR(int amount)
    {
        magicResistance += amount;
    }

    public float CritHit()
    {
        //100 dex = 50% crit hit
        return Mathf.Clamp((0.5f * (DEX + critHit)), 0, 100);
    }

    public void AddCritHit(int amount)
    {
        critHit += amount;
    }

    public float CritDamage()
    {
        //100 dex + crit damage = +50% crit damage (starts at 150%)
        return Mathf.Clamp(((0.5f * (DEX + critDamage)) / 100) + 1.5f, 1.5f, 3);
    }

    public void AddCritDamage(int amount)
    {
        critDamage += amount;
    }

    public float CDR()
    {
        //100 wits + cdr = 40% shorter cooldowns
        return 1 - Mathf.Clamp((0.4f * (WTS + cdr)) / 100, 0, 0.75f);
    }
    public void AddCDR(int amount)
    {
        cdr += amount;
    }

    public float ManaCostDiscount()
    {
        //100 int + manacosts => 50% , 200 total = free of mana
        return 1 - Mathf.Clamp((0.5f * (INT + manaCost)) / 100, 0, 1);
    }

    public void AddManaCostDiscount(int amount)
    {
        manaCost += amount;
    }

    public float HealPower()
    {
        //100 faith + heal power = 50% more powerful heals
        return Mathf.Clamp(((0.5f * (FTH + healPower)) / 100) + 1, 1, 3);
    }

    public void AddHealPower(int amount)
    {
        healPower += amount;
    }

    public float HealthRegen()
    {
        //100 regen + hpregen = 5% hp per second? 
        return Mathf.Clamp((0.05f * (RGN + hpRegen) / 100), 0.01f, 0.1f);
    }

    public void AddHealthRegen(int amount)
    {
        hpRegen += amount;
    }

    public float ManaRegen()
    {
        //100 regen + hpregen = 5% hp per second? 
        return Mathf.Clamp((0.05f * (RGN + manaRegen) / 100), 0.01f, 0.1f);
    }

    public void AddManaRegen(int amount)
    {
        manaRegen += amount;
    }

    public float MaxHealth()
    {
        //100 faith + maxhp => 50% extra max health
        return Mathf.Clamp((0.5f * (FTH + maxHp) / 100), 0, 3);
    }

    public void AddMaxHealth(int amount)
    {
        maxHp += amount;
    }

    public float MaxMana()
    {
        //100 faith + maxhp => 50% extra max health
        return Mathf.Clamp((0.5f * (INT + maxMana) / 100), 0, 3);
    }

    public void AddMaxMana(int amount)
    {
        maxMana += amount;
    }
}
