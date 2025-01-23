using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private StringAnimator _characterAnimator;
    private Transform[] _movePoints;
    private Transform _attackPoint;
    private Transform _roulettePoint;
    private float _moveSpeed = 3f;

    public Vector3 Position => transform.position;
    public float MoveSpeed => _moveSpeed;
    public bool IsStarted { get; private set; } = false;

    public event Action ReachedNewStage;
    public event Action ReachedStartPoint;
    public event Action ReachedFinish;

    public void Init(Transform[] points, Transform attackPoint, Transform roulettePoint, float speed)
    {
        _movePoints = points;
        _attackPoint = attackPoint;
        _roulettePoint = roulettePoint;
        _moveSpeed = speed;
    }

    public IEnumerator GoToStartPoint()
    {
        IsStarted = true;
        _characterAnimator.ChangeAnimation(AnimationsName.GirlWalk);
        yield return StartCoroutine(GoToPoint(_movePoints[0]));

        _characterAnimator.ChangeAnimation(AnimationsName.GirlIdle);
        ReachedStartPoint?.Invoke();
    }

    public IEnumerator Go()
    {
        _characterAnimator.ChangeAnimation(AnimationsName.GirlWalk);

        for (int i = 1; i < _movePoints.Length; i++)
        {
            yield return StartCoroutine(GoToPoint(_movePoints[i]));

            if(i < _movePoints.Length - 1)
            {
                ReachedNewStage?.Invoke();
                continue;
            }

            _characterAnimator.ChangeAnimation(AnimationsName.GirlIdle);

            ReachedFinish?.Invoke();
        }
    }

    public IEnumerator GoToAttackPoint()
    {
        yield return StartCoroutine(GoToPoint(_attackPoint, isLookAtPoint: true));
    }

    public IEnumerator GoToRoulettePoint()
    {
        yield return StartCoroutine(GoToPoint(_roulettePoint, isLookAtPoint: true));
    }

    public void OnLevelReset()
    {
        IsStarted = false;
    }

    private IEnumerator GoToPoint(Transform point, bool isLookAtPoint = false)
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

            yield return null;
        }
    }
}