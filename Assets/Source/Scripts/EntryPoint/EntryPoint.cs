using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private Character _player;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private StartPointHandler _startPointHandler;
    [SerializeField] private EndLevelHandler _endLevelHandler;

    private void Awake()
    {
        _player.Animator.ChangeAnimation(AnimationsName.GirlIdle);

        _clothPanelHandler.Enable();
        _startPointHandler.Enable();
        _endLevelHandler.Enable();

        _levelSpawner.Spawn();
        _player.CharacterMover.GoToStartPoint();
    }

    private void OnDestroy()
    {
        _clothPanelHandler.Disable();
        _startPointHandler.Disable();
        _endLevelHandler.Disable();
    }
}
