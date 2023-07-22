using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private StatBar manaBar;
    [SerializeField] private StatBar healthBar;

    private void Start()
    {
        GameManager.Instance.PlayerWrapper.CachePlayerHud(this);
    }

    public StatBar ManaBar { get => manaBar; }
    public StatBar HealthBar { get => healthBar; }
}
