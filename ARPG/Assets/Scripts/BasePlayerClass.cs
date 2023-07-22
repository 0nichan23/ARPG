using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasePlayerClass : MonoBehaviour
{
    [SerializeField] protected List<BasePlayerWeapon> weapons = new List<BasePlayerWeapon>();
    [SerializeField] private StatSheet baseStats;
    [SerializeField] private ClassPassive passive;
    protected BasePlayerWeapon currnetWeapon;

    public UnityEvent PrimaryUsed;
    public UnityEvent SecondaryUsed;
    public UnityEvent TertiaryUsed;
    public UnityEvent UtilityUsed;

    public StatSheet BaseStats { get => baseStats;}

    //field for passive -> another day

    private void Start()
    {
        passive.CacheOwner(GameManager.Instance.PlayerWrapper);
        passive.SubscribePassive();
        currnetWeapon = weapons[0];
        ResetEvents();
    }


  /*  public void SwitchWeapons()
    {
        if (weapons.Count <= 1)
        {
            return;
        }
        //current weapon switch
        //ubsub everything except passive
        //resub everything except passive
        //change controller on the playerwrapper
    }*/

    private void ResetEvents()
    {
        PrimaryUsed.RemoveAllListeners();
        SecondaryUsed.RemoveAllListeners();
        TertiaryUsed.RemoveAllListeners();
        UtilityUsed.RemoveAllListeners();
        PrimaryUsed.AddListener(currnetWeapon.Primary);
        SecondaryUsed.AddListener(currnetWeapon.Secondary);
        TertiaryUsed.AddListener(currnetWeapon.Tertiary);
        UtilityUsed.AddListener(currnetWeapon.Utility);
        GameManager.Instance.PlayerWrapper.PlayerAnim.runtimeAnimatorController = currnetWeapon.AnimController;
        currnetWeapon.CacheWeaponOnHandlers();
    }

}
