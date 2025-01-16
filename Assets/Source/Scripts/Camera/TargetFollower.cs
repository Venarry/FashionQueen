using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private float _speed = 8;

    public Vector3 _offset;

    private void Awake()
    {
        //_offset = transform.position - _target.position;
        _offset = transform.position - GetCenter();
    }

    private void LateUpdate()
    {
        Vector3 targetLookPosition = GetCenter();

        transform.position = Vector3.Lerp(transform.position, targetLookPosition + _offset, _speed * Time.deltaTime);
    }

    public void Add(Transform target)
    {
        _targets.Add(target);
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
