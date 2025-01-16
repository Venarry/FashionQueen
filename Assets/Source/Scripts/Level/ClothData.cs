using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewClothData", menuName = "ScriptableObjects/ClothData", order = 2)]
public class ClothData : ScriptableObject
{
    [Range(1, 5)] public int Rate;
    public Material Material;
    public Mesh Mesh;
    public Sprite Icon;
}
