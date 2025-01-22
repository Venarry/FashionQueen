using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    public class SkinnedMeshData
    {
        public Mesh Mesh;
        public Material Material;
    }

    [SerializeField] private SkinnedMeshRenderer _hair;
    [SerializeField] private SkinnedMeshRenderer _dress;
    [SerializeField] private SkinnedMeshRenderer _skirt;
    [SerializeField] private SkinnedMeshRenderer _shoes;
    [SerializeField] private SkinnedMeshRenderer[] _meshes;

    private readonly Dictionary<int, int> _rate = new();
    private Mesh[] _startMeshes;
    private Material[] _startMaterials;

    public Dictionary<int, int> Rate => _rate.ToDictionary(c => c.Key, c => c.Value);

    private void Awake()
    {
        _startMeshes = new Mesh[_meshes.Length];
        _startMaterials = new Material[_meshes.Length];

        for (int i = 0; i < _meshes.Length; i++)
        {
            _startMeshes[i] = _meshes[i].sharedMesh;
            _startMaterials[i] = _meshes[i].sharedMaterial;
        }
    }

    public void Set(int index, Material material, Mesh mesh, int rate)
    {
        _meshes[index].sharedMaterial = material;
        _meshes[index].sharedMesh = mesh;
        _rate[index] = rate;
    }

    public void SetRates(Dictionary<int, int> rates)
    {
        foreach (KeyValuePair<int, int> rate in rates)
        {
            _rate[rate.Key] = rate.Value;
        }
    }

    public void SetStartCloth()
    {
        for (int i = 0; i < _meshes.Length; i++)
        {
            _meshes[i].sharedMesh = _startMeshes[i];
            _meshes[i].sharedMaterial = _startMaterials[i];
        }
    }

    /*public void SetHair(int index, Material material, Mesh mesh, int rate)
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
    }*/
}
