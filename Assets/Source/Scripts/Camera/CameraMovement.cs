using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private Transform[] _swipePositions;

    private int _swipeCounter = 0;
    private Vector3 _offset;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private int _moveCounter = 0;
    private bool _ended = false;
    private Coroutine _activeMoving;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _offset = transform.position;
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

    public void GoToNextSwipePosition()
    {
        if (_ended == true)
            return;

        _swipeCounter++;

        if(_swipeCounter >= _swipePositions.Length)
        {
            _swipeCounter = 0;
        }

        GoToSwipePosition();
    }

    public void GoToPreviousSwipePosition()
    {
        if (_ended == true)
            return;

        _swipeCounter--;

        if (_swipeCounter < 0)
        {
            _swipeCounter = _swipePositions.Length - 1;
        }

        GoToSwipePosition();
    }

    private void GoToSwipePosition()
    {
        Transform currentSwipePoint = _swipePositions[_swipeCounter];
        StopMoving();

        _offset = currentSwipePoint.position;
        _activeMoving = StartCoroutine(MovingTo(currentSwipePoint.position + GetCenter(), currentSwipePoint.rotation, 0.2f));
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
        _moveCounter = 0;

        transform.position = _startPosition;
        transform.rotation = _startRotation;

        _offset = transform.position;
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
