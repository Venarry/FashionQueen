using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private Character _player;
    [SerializeField] private LevelSpawner _levelSpawner;

    private void Awake()
    {
        _clothPanelHandler.Enable();

        _levelSpawner.Spawn();
        _player.CharacterMover.Go();
    }
}
