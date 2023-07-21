using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UtilityHandler : MonoBehaviour
{
    [SerializeField] private int coolDown;
    [SerializeField] private Animator anim;
    public UnityEvent OnUtilityPerformed;
    public UnityEvent OnUtilityUp;
    private float lastUsed;

    public void CacheWeaponData()
    {
        lastUsed = coolDown * -1;
    }

    private bool CheckCoolDown()
    {
        if (Time.time - lastUsed >= coolDown)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (CheckCoolDown() && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Utility();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            UtilityUp();
        }
    }

    private void Utility()
    {
        OnUtilityPerformed?.Invoke();
        lastUsed = Time.time;
        anim.SetTrigger("Utility");
    }
    private void UtilityUp()
    {
        OnUtilityUp?.Invoke();
    }
}
