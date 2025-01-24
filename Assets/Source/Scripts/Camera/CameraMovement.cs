using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private Transform[] _swipePositions;
    [SerializeField] private Transform _startGamePoint;
    [SerializeField] private Transform _shopPoint;

    private int _swipeCounter = 0;
    private Vector3 _offset;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private int _moveCounter = 0;
    private bool _ended = false;
    private Coroutine _activeMoving;
    private bool _swipeLocked = true;
    private bool _inShop = false;
    private Vector3 _shopOffset = new Vector3(0, 0.4f, 0);

    private void Awake()
    {
        _startPosition = _swipePositions[0].position;
        _startRotation = _swipePositions[0].rotation;

        GoToStartPosition();
    }

    private void LateUpdate()
    {
        if (_ended == true)
        {
            return;
        }

        Vector3 targetLookPosition = GetCenter();
        transform.position = Vector3.Lerp(transform.position, targetLookPosition + _offset, _speed * Time.deltaTime);
    }

    public void GoToShop()
    {
        _swipeLocked = false;
        _inShop = true;
        _offset = _swipePositions[0].position * 0.5f + _shopOffset;
    }

    public void GoToStartPosition()
    {
        _swipeLocked = true;
        _inShop = false;

        _swipeCounter = 0;
        GoToSwipePosition(_startGamePoint);
        _offset = _startGamePoint.position;
    }

    public void StartMovement()
    {
        _swipeLocked = false;
        _offset = _startPosition;
    }

    public void GoToNextSwipePosition()
    {
        if (_swipeLocked == true)
            return;

        if (_ended == true)
            return;

        _swipeCounter++;

        if(_swipeCounter >= _swipePositions.Length)
        {
            _swipeCounter = 0;
        }

        Transform currentSwipePoint = _swipePositions[_swipeCounter];
        GoToSwipePosition(currentSwipePoint);
    }

    public void GoToPreviousSwipePosition()
    {
        if (_swipeLocked == true)
            return;

        if (_ended == true)
            return;

        _swipeCounter--;

        if (_swipeCounter < 0)
        {
            _swipeCounter = _swipePositions.Length - 1;
        }

        Transform currentSwipePoint = _swipePositions[_swipeCounter];
        GoToSwipePosition(currentSwipePoint);
    }

    private void GoToSwipePosition(Transform target)
    {
        StopMoving();

        float multiplier = 1f;
        Vector3 offset = Vector3.zero;

        if(_inShop == true)
        {
            multiplier = 0.5f;
            offset = _shopOffset;
        }

        _offset = target.position * multiplier + offset;
        _activeMoving = StartCoroutine(MovingTo((target.position + GetCenter()) * multiplier + offset, target.rotation, 0.2f));
    }

    public void Add(Transform target)
    {
        _targets.Add(target);
    }

    public void GoNext()
    {
        _ended = true;

        if (_moveCounter >= _movePoints.Length)
            return;

        Transform target = _movePoints[_moveCounter];
        _activeMoving = StartCoroutine(MovingTo(target.position, target.rotation, 2f));
        _moveCounter++;
    }

    public void OnResetLevel()
    {
        StopMoving();

        _ended = false;
        _swipeLocked = true;
        _moveCounter = 0;

        transform.position = _startGamePoint.position;
        transform.rotation = _startRotation;

        GoToStartPosition();
    }

    public void Remove(Transform target)
    {
        _targets.Remove(target);
    }

    private void StopMoving()
    {
        if (_activeMoving != null)
        {
            StopCoroutine(_activeMoving);
            _activeMoving = null;
        }
    }

    private IEnumerator MovingTo(Vector3 position, Quaternion rotation, float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, position, i / duration);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i / duration);

            yield return null;
        }

        _activeMoving = null;
    }

    private Vector3 GetCenter()
    {
        Vector3 targetLookPosition = Vector3.zero;

        foreach (Transform target in _targets)
        {
            if(target == null)
            {
                continue;
            }

            targetLookPosition += target.position;
        }

        targetLookPosition /= _targets.Count;

        return targetLookPosition;
    }
}
