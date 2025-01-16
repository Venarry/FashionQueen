using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _hair;
    [SerializeField] private SkinnedMeshRenderer _dress;
    [SerializeField] private SkinnedMeshRenderer _skirt;
    [SerializeField] private SkinnedMeshRenderer _shoes;

    private Dictionary<int, Action<int, Material, Mesh, int>> _setActions;
    private Dictionary<int, int> _rate = new();

    private void Awake()
    {
        _setActions = new()
        {
            [ClothIndexDataSource.ClothIndexHair] = SetHair,
            [ClothIndexDataSource.ClothIndexDress] = SetDress,
            [ClothIndexDataSource.ClothIndexSkirt] = SetSkirt,
            [ClothIndexDataSource.ClothIndexShoes] = SetShoes,
        };
    }

    public void Set(int index, Material material, Mesh mesh, int rate)
    {
        _setActions[index](index, material, mesh, rate);
    }

    public void SetHair(int index, Material material, Mesh mesh, int rate)
    {
        _hair.sharedMaterial = material;
        _hair.sharedMesh = mesh;
        _rate[index] = rate;
    }

    public void SetDress(int index, Material material, Mesh mesh, int rate)
    {
        _dress.sharedMaterial = material;
        _dress.sharedMesh = mesh;
        _rate[index] = rate;
    }
    public void SetSkirt(int index, Material material, Mesh mesh, int rate)
    {
        _skirt.sharedMaterial = material;
        _skirt.sharedMesh = mesh;
        _rate[index] = rate;
    }

    public void SetShoes(int index, Material material, Mesh mesh, int rate)
    {
        _shoes.sharedMaterial = material;
        _shoes.sharedMesh = mesh;
        _rate[index] = rate;
    }
}
