using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private StringAnimator _characterAnimator;
    private Transform[] _movePoints;
    private Transform _attackPoint;
    private float _moveSpeed = 3f;

    public Vector3 Position => transform.position;
    public float MoveSpeed => _moveSpeed;
    public bool IsStarted { get; private set; } = false;

    public event Action ReachedNewStage;
    public event Action ReachedStartPoint;
    public event Action ReachedFinish;

    public void Init(Transform[] points, Transform attackPoint, float speed)
    {
        _movePoints = points;
        _attackPoint = attackPoint;
        _moveSpeed = speed;
    }

    public async void GoToStartPoint()
    {
        try
        {
            IsStarted = true;
            _characterAnimator.ChangeAnimation(AnimationsName.GirlWalk);
            await GoToPoint(_movePoints[0]);

            _characterAnimator.ChangeAnimation(AnimationsName.GirlIdle);
            ReachedStartPoint?.Invoke();
        }
        catch { }
    }

    public async void Go()
    {
        _characterAnimator.ChangeAnimation(AnimationsName.GirlWalk);

        for (int i = 1; i < _movePoints.Length; i++)
        {
            try
            {
                await GoToPoint(_movePoints[i]);

                if(i < _movePoints.Length - 1)
                {
                    ReachedNewStage?.Invoke();
                    continue;
                }

                _characterAnimator.ChangeAnimation(AnimationsName.GirlIdle);

                ReachedFinish?.Invoke();
            }
            catch { }
        }
    }

    public async Task GoToAttackPoint()
    {
        await GoToPoint(_attackPoint, isLookAtPoint: true);
    }

    public void OnLevelReset()
    {
        IsStarted = false;
    }

    private async Task GoToPoint(Transform point, bool isLookAtPoint = false)
    {
        float minMagnitude = 0.05f;
        float rotationSpeed = 300f;

        while (Vector3.Distance(transform.position, point.position) > minMagnitude)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _moveSpeed * Time.deltaTime);

            if(isLookAtPoint == true)
            {
                Quaternion lookAtPoint = Quaternion.LookRotation(point.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtPoint, rotationSpeed * Time.deltaTime);
            }

            await Task.Yield();
        }
    }
}