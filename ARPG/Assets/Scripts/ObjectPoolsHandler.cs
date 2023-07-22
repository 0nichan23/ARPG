using UnityEngine;

public class ObjectPoolsHandler : MonoBehaviour
{
    [SerializeField] private ObjectPoolBlank paladinSecondaryVFXPool;
    [SerializeField] private ObjectPoolBlank paladinPassiveVFXPool;

    public ObjectPoolBlank PaladinSecondaryVFXPool { get => paladinSecondaryVFXPool; }
    public ObjectPoolBlank PaladinPassiveVFXPool { get => paladinPassiveVFXPool; }
}
