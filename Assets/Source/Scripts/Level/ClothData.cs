using System;
using UnityEngine;

[Serializable]
public class ClothData
{
    [Range(1, 5)] public int Rate;
    public Material Material;
    public Mesh Mesh;
    public Sprite Icon;
}
