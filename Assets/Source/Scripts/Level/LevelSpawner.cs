using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private ClothPanelHandler _clothPanelHandler;
    [SerializeField] private CharacterView _mainCharacter;
    [SerializeField] private TextPanel _levelNameTextPanel;

    public int ActiveLevelIndex { get; private set; } = 0;

    public void SpawnNext()
    {
        if (ActiveLevelIndex >= _levels.Length)
        {
            ActiveLevelIndex = 0;
        }

        LevelData level = _levels[ActiveLevelIndex];

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

        _levelNameTextPanel.Set(level.LevelName);
        _levelNameTextPanel.Show();

        ActiveLevelIndex++;
    }

    public void LoadData(int levelIndex)
    {
        ActiveLevelIndex = levelIndex;
    }

    public void ResetLevels()
    {
        ActiveLevelIndex = 0;
    }
}