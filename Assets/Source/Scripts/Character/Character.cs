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
    [field: SerializeField] public CharacterRateShower RateShower { get; set; }

    public void Init(Transform[] points, Transform attackPoint, Transform roulettePoint, float speed, RateSmilesDataSource rateSmilesDataSource)
    {
        CharacterMover.Init(points, attackPoint, roulettePoint, speed);
        RateShower.Init(rateSmilesDataSource);
    }
}
