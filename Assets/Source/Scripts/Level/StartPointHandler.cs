using System.Threading.Tasks;
using UnityEngine;

public class StartPointHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private TargetFollower _targetFollower;

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

            Vector3 enemySpawnOffset = new(-2, 0, 0);
            _enemy = await _enemySpawner.SpawnWithProjection(_playerMover.Position + enemySpawnOffset, Quaternion.identity, _playerMover.MoveSpeed);
            _clothPanelHandler.SetEnemy(_enemy);
            _targetFollower.Add(_enemy.transform);

            await Task.Delay(_actionsDelay);

            _playerMover.Go();
            _enemy.CharacterMover.Go();
        }
        catch { }
    }
}
