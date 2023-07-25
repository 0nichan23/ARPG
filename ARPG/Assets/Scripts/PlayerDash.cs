using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    public UnityEvent OnDashStart;
    public UnityEvent OnDashEnd;
    private CharacterController controller => GameManager.Instance.PlayerWrapper.Controller._characterController;

    public void StartDash(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            StartCoroutine(Dash(direction));
        }
    }

    private IEnumerator Dash(Vector3 direction)
    {
        float counter = dashDuration;
        Vector3 dir = new Vector3(direction.x, 0, direction.z).normalized;
        OnDashStart?.Invoke();
        while (counter > 0)
        {
            controller.Move(dashSpeed * dir * Time.deltaTime);
            counter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        OnDashEnd?.Invoke();
    }

}
