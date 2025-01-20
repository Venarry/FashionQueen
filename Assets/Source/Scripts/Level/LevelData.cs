using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string LevelName;
    [Range(0, 5)] public int StartHairRate;
    [Range(0, 5)] public int StartDressRate;
    [Range(0, 5)] public int StartSkirtRate;
    [Range(0, 5)] public int StartShoesRate;
    public List<ClothWithRateData> Hair;
    public List<ClothWithRateData> Dress;
    public List<ClothWithRateData> Skirt;
    public List<ClothWithRateData> Shoes;
}

[Serializable]
public class ClothWithRateData
{
    [Range(1, 5)] public int Rate;
    public ClothData Data;
}
