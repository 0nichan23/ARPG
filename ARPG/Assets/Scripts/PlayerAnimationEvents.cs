using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    [SerializeField] private GameObject primaryCollider;
    public void PrimaryColldierOn()
    {
        primaryCollider.SetActive(true);
    }

}
