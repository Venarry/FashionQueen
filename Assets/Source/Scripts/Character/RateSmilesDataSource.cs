using System.Collections.Generic;
using UnityEngine;

public class RateSmilesDataSource : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;

    public Sprite[] Sprites => _sprites.ToArray();
}
