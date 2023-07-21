using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public void InvokePrimary()
    {
        GameManager.Instance.PlayerWrapper.CurrentClass.PrimaryUsed?.Invoke();
    }

    public void InvokeSecondary()
    {
        GameManager.Instance.PlayerWrapper.CurrentClass.SecondaryUsed?.Invoke();
    }

    public void InvokeTertiary()
    {
        GameManager.Instance.PlayerWrapper.CurrentClass.TertiaryUsed?.Invoke();
    }

    public void InvokeUtility()
    {
        GameManager.Instance.PlayerWrapper.CurrentClass.UtilityUsed?.Invoke();
    }
}
