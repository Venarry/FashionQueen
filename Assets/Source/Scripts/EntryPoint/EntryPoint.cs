using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private Character _player;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private StartPointHandler _startPointHandler;
    [SerializeField] private EndLevelHandler _endLevelHandler;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private RateSmilesDataSource _rateSmilesDataSource;
    [SerializeField] private Transform[] _playerMovePoints;
    [SerializeField] private Transform[] _enemyMovePoints;
    [SerializeField] private float _characterSpeed;

    private void Awake()
    {
        _player.Init(_playerMovePoints, _characterSpeed, _rateSmilesDataSource);
        _enemySpawner.Init(_rateSmilesDataSource, _enemyMovePoints);

        _clothPanelHandler.Enable();
        _startPointHandler.Enable();
        _endLevelHandler.Enable();

        _levelSpawner.Spawn();
        _player.Animator.ChangeAnimation(AnimationsName.GirlIdle);
        _player.CharacterMover.GoToStartPoint();
    }

    private void OnDestroy()
    {
        _clothPanelHandler.Disable();
        _startPointHandler.Disable();
        _endLevelHandler.Disable();
    }
}
