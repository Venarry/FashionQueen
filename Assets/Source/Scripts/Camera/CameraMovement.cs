using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform _endLevelPoint;
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _rotationSpeed = 1f;

    private Vector3 _offset;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private bool _ended = false;

    private void Awake()
    {
        _offset = transform.position - GetCenter();

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if(_ended == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endLevelPoint.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _endLevelPoint.rotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 targetLookPosition = GetCenter();

            transform.position = Vector3.Lerp(transform.position, targetLookPosition + _offset, _speed * Time.deltaTime);
        }
    }

    public void Add(Transform target)
    {
        _targets.Add(target);
    }

    public void GoToEndPoint()
    {
        _ended = true;
    }

    public void OnResetLevel()
    {

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
