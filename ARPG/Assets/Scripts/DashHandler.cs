using UnityEngine;
using UnityEngine.Events;

public class DashHandler : MonoBehaviour
{
    public UnityEvent<Vector3> DashPerformed;
    [SerializeField] private float dashCD;
    private float lastDashed;
    private void Start()
    {
        lastDashed = dashCD * -1;
    }
    private void Update()
    {
        if (GameManager.Instance.PlayerWrapper.CanDash && Input.GetKey(KeyCode.Space))
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (Time.time - lastDashed >= dashCD)
        {
            DashPerformed?.Invoke(GameManager.Instance.PlayerWrapper.Controller._characterController.velocity);
            lastDashed = Time.time;
        }
    }

}
