using UnityEngine;

public class ObjectPoolsHandler : MonoBehaviour
{
    [SerializeField] private ObjectPoolBlank paladinSecondaryVFXPool;

    public ObjectPoolBlank PaladinSecondaryVFXPool { get => paladinSecondaryVFXPool; }
}
