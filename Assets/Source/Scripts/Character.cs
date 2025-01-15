using UnityEngine;

[RequireComponent(typeof(CharacterView))]
[RequireComponent(typeof(CharacterMover))]
public class Character : MonoBehaviour
{
    [field: SerializeField] public CharacterView CharacterView { get; set; }
    [field: SerializeField] public CharacterMover CharacterMover { get; set; }
}
