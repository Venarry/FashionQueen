using System.Collections;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;

    private int _activeLevelIndex = 0;

    public void Spawn()
    {
        LevelData level = _levels[_activeLevelIndex];

        _clothPanelHandler.SetData(new ClothData[][] 
        { 
            level.Hair.ToArray(),
            level.Dress.ToArray(),
            level.Skirt.ToArray(),
            level.Shoes.ToArray(),
        });
    }
}