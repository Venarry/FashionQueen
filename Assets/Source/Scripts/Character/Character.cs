using UnityEngine;

[RequireComponent(typeof(CharacterView))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(StringAnimator))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    [field: SerializeField] public CharacterView CharacterView { get; set; }
    [field: SerializeField] public CharacterMover CharacterMover { get; set; }
    [field: SerializeField] public StringAnimator Animator { get; set; }

    public void Init(Transform[] points, float speed)
    {
        CharacterMover.Init(points, speed);
    }
}
