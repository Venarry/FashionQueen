using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _rotationSpeed = 1f;

    private Vector3 _offset;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private int _moveCounter = 0;
    private bool _ended = false;

    private void Awake()
    {
        _offset = transform.position - GetCenter();

        _startPosition = transform.position;
        _startRotation = transform.rotation;
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
        StartCoroutine(MovingToEndPoint(target));
        _moveCounter++;
    }

    public void OnResetLevel()
    {

    }

    private IEnumerator MovingToEndPoint(Transform target)
    {
        float duration = 2f;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, i / duration);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, i / duration);

            yield return null;
        }
    }

    private Vector3 GetCenter()
    {
        Vector3 targetLookPosition = Vector3.zero;

        foreach (Transform target in _targets)
        {
            targetLookPosition += target.position;
        }

        targetLookPosition /= _targets.Count;

        return targetLookPosition;
    }
}