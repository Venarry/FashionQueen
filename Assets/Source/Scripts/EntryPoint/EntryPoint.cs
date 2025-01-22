using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private Character _player;
    [SerializeField] private StartPointHandler _startPointHandler;
    [SerializeField] private EndLevelHandler _endLevelHandler;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private RateSmilesDataSource _rateSmilesDataSource;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private Transform[] _playerMovePoints;
    [SerializeField] private Transform _playerAttackPoint;
    [SerializeField] private Transform[] _enemyMovePoints;
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private float _characterSpeed;

    private void Awake()
    {
        WalletModel walletModel = new();

        _walletView.Init(walletModel);
        _player.Init(_playerMovePoints, _playerAttackPoint, _characterSpeed, _rateSmilesDataSource);
        _enemySpawner.Init(_rateSmilesDataSource, _enemyMovePoints, _enemyAttackPoint);

        _walletView.Enable();
        _clothPanelHandler.Enable();
        _startPointHandler.Enable();
        _endLevelHandler.Enable();

        _player.Animator.ChangeAnimation(AnimationsName.GirlIdle);
    }

    private void OnDestroy()
    {
        _clothPanelHandler.Disable();
        _startPointHandler.Disable();
        _endLevelHandler.Disable();
    }
}
