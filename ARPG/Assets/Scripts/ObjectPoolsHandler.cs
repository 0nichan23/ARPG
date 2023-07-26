using UnityEngine;

public class ObjectPoolsHandler : MonoBehaviour
{
    [SerializeField] private ObjectPoolBlank paladinSecondaryVFXPool;
    [SerializeField] private ObjectPoolBlank paladinPassiveVFXPool;
    [SerializeField] private ProjectilePool wizardSmallWandProjectilePool;
    [SerializeField] private MinePool minePool;
    [SerializeField] private ElementalObjectPool explosionsPool;

    public ObjectPoolBlank PaladinSecondaryVFXPool { get => paladinSecondaryVFXPool; }
    public ObjectPoolBlank PaladinPassiveVFXPool { get => paladinPassiveVFXPool; }
    public ProjectilePool WizardSmallWandProjectilePool { get => wizardSmallWandProjectilePool; }
    public MinePool MinePool { get => minePool; }
    public ElementalObjectPool ExplosionsPool { get => explosionsPool; }
}
