using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelHandler : MonoBehaviour
{
    [SerializeField] private CharacterMover _playerMover;
    [SerializeField] private CameraMovement _camera;

    public void Enable()
    {
        _playerMover.ReachedFinish += OnFinishReach;
    }

    private void OnFinishReach()
    {

    }
}
