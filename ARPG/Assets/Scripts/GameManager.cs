using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PlayerWrapper playerWrapper;
    [SerializeField] private PopupSpawner popupSpawner;
    public PlayerWrapper PlayerWrapper { get => playerWrapper; }
    public PopupSpawner PopupSpawner { get => popupSpawner; }
}
