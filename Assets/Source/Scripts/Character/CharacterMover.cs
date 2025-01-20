using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private StringAnimator _characterAnimator;
    private Transform[] _movePoints;
    private float _moveSpeed = 3f;

    public Vector3 Position => transform.position;
    public float MoveSpeed => _moveSpeed;

    public event Action ReachedNewStage;
    public event Action ReachedStartPoint;
    public event Action ReachedFinish;

    public void Init(Transform[] points, float speed)
    {
        _movePoints = points;
        _moveSpeed = speed;
    }

    public async void GoToStartPoint()
    {
        try
        {
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

    private async Task GoToPoint(Transform point)
    {
        float minMagnitude = 0.05f;

        while (Vector3.Distance(transform.position, point.position) > minMagnitude)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _moveSpeed * Time.deltaTime);

            await Task.Yield();
        }
    }
}