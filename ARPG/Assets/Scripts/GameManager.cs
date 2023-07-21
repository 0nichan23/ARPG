using UnityEngine;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PlayerWrapper playerWrapper;
    [SerializeField] private PopupSpawner popupSpawner;

    public void CachePlayer(PlayerWrapper player)
    {
        playerWrapper = player;
    }

    public PlayerWrapper PlayerWrapper { get => playerWrapper; }
    public PopupSpawner PopupSpawner { get => popupSpawner; }
}
