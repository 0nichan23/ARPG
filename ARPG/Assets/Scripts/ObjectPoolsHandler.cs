using UnityEngine;

public class ObjectPoolsHandler : MonoBehaviour
{
    [SerializeField] private ObjectPoolBlank paladinSecondaryVFXPool;
    [SerializeField] private ObjectPoolBlank paladinPassiveVFXPool;
    [SerializeField] private ProjectilePool wizardSmallWandProjectilePool;

    public ObjectPoolBlank PaladinSecondaryVFXPool { get => paladinSecondaryVFXPool; }
    public ObjectPoolBlank PaladinPassiveVFXPool { get => paladinPassiveVFXPool; }
    public ProjectilePool WizardSmallWandProjectilePool { get => wizardSmallWandProjectilePool; }
}
