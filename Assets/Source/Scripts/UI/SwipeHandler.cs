using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;
    private bool _isSwiping = false;
    private string _swipeAxis = "";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
            _isSwiping = true;
        }

        float magnitudeForSwipe = 25;
        
        if (Input.GetMouseButton(0) && _isSwiping)
        {
            _currentTouchPosition = Input.mousePosition;
            Vector2 delta = _currentTouchPosition - _startTouchPosition;

            if (_swipeAxis == "" && delta.magnitude >= magnitudeForSwipe)
            {
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    _swipeAxis = "x";
                else
                    _swipeAxis = "y";
            }

            if(_swipeAxis == "")
            {
                return;
            }

            if(_swipeAxis == "x")
            {
                _currentTouchPosition.y = _startTouchPosition.y;

                if(delta.x > 0)
                {
                    _cameraMovement.GoToPreviousSwipePosition();
                    DisableTouching();
                }
                else
                {
                    _cameraMovement.GoToNextSwipePosition();
                    DisableTouching();
                }
            }
            else
            {
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableTouching();
        }
    }

    private void DisableTouching()
    {
        _isSwiping = false;
        _swipeAxis = "";
    }
}