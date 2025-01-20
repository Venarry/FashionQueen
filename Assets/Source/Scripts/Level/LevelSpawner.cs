using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private CharacterView _mainCharacter;

    private int _activeLevelIndex = 0;

    public void Spawn()
    {
        LevelData level = _levels[_activeLevelIndex];

        _clothPanelHandler.SetData(new ClothWithRateData[][] 
        { 
            level.Hair.ToArray(),
            level.Dress.ToArray(),
            level.Skirt.ToArray(),
            level.Shoes.ToArray(),
        });

        Dictionary<int, int> rates = new()
        {
            [ClothIndexDataSource.ClothIndexHair] = level.StartHairRate,
            [ClothIndexDataSource.ClothIndexDress] = level.StartDressRate,
            [ClothIndexDataSource.ClothIndexSkirt] = level.StartSkirtRate,
            [ClothIndexDataSource.ClothIndexShoes] = level.StartShoesRate,
        };

        _mainCharacter.SetRates(rates);
    }
}