using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassPassive : MonoBehaviour
{
    protected Character owner;
    public void CacheOwner(Character givenOwner)
    {
        owner = givenOwner;
    }

    public abstract void SubscribePassive();
    public abstract void UnSubscribePassive();

}
