using System.Threading.Tasks;
using UnityEngine;

public class StartPointHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private CameraMovement _targetFollower;
    [SerializeField] private EndLevelHandler _endLevelHandler;

    private readonly int _actionsDelay = 500;
    private Character _enemy;

    public void Enable()
    {
        _playerMover.ReachedStartPoint += OnStartPointReach;
    }

    public void Disable()
    {
        _playerMover.ReachedStartPoint -= OnStartPointReach;
    }

    private async void OnStartPointReach()
    {
        try
        {
            await Task.Delay(_actionsDelay);

            Vector3 enemySpawnPosition = new(_playerMover.Position.x * -1, _playerMover.Position.y, _playerMover.Position.z);
            _enemy = await _enemySpawner.SpawnWithProjection(enemySpawnPosition, Quaternion.identity, _playerMover.MoveSpeed);
            _clothPanelHandler.SetEnemy(_enemy);
            _endLevelHandler.SetEnemy(_enemy);
            _targetFollower.Add(_enemy.transform);

            await Task.Delay(_actionsDelay);

            _playerMover.Go();
            _enemy.CharacterMover.Go();
        }
        catch { }
    }
}