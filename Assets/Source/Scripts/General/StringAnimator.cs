using UnityEngine;

public class StringAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private string _currentAnimation;

    public void ChangeAnimation(string name, float transitionDuration = 0.1f, bool canRepeat = false, float animatorSpeed = 1f)
    {
        _animator.speed = animatorSpeed;

        if (_currentAnimation == name && canRepeat == false)
        {
            return;
        }

        _animator.CrossFadeInFixedTime(name, transitionDuration);
        _currentAnimation = name;
    }
}
