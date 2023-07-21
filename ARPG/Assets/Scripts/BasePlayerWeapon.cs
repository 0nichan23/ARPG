using System.Collections.Generic;
using UnityEngine;

public class BasePlayerWeapon : MonoBehaviour
{
    [SerializeField] protected List<AttackData> primaryCombo = new List<AttackData>();
    [SerializeField] protected AttackData secondaryAttack;
    [SerializeField] protected AttackData tertiaryAttack;
    [SerializeField] protected RuntimeAnimatorController animController;

    public List<AttackData> PrimaryCombo { get => primaryCombo; }
    public AttackData SecondaryAttack { get => secondaryAttack; }
    public AttackData TertiaryAttack { get => tertiaryAttack; }
    public RuntimeAnimatorController AnimController { get => animController; }


    public virtual void Primary()
    {

    }


    public virtual void Secondary()
    {

    }

    //invoke tertiary
    public virtual void Tertiary()
    {

    }

    //invoke utility
    public virtual void Utility()
    {

    }

    public virtual void CacheWeaponOnHandlers()
    {

    }

}
