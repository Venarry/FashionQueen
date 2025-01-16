using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public List<ClothData> Hair;
    public List<ClothData> Dress;
    public List<ClothData> Skirt;
    public List<ClothData> Shoes;
}
