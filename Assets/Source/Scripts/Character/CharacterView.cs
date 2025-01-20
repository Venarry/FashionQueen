using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _hair;
    [SerializeField] private SkinnedMeshRenderer _dress;
    [SerializeField] private SkinnedMeshRenderer _skirt;
    [SerializeField] private SkinnedMeshRenderer _shoes;

    private readonly Dictionary<int, int> _rate = new();
    private Dictionary<int, Action<int, Material, Mesh, int>> _setActions;

    public Dictionary<int, int> Rate => _rate.ToDictionary(c => c.Key, c => c.Value);

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

    public void SetRates(Dictionary<int, int> rates)
    {
        foreach (KeyValuePair<int, int> rate in rates)
        {
            _rate[rate.Key] = rate.Value;
        }
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
