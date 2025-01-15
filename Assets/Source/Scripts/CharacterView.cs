using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _hair;
    [SerializeField] private SkinnedMeshRenderer _dress;
    [SerializeField] private SkinnedMeshRenderer _skirt;
    [SerializeField] private SkinnedMeshRenderer _shoes;

    public void SetHair(Material material, Mesh mesh)
    {
        _hair.sharedMaterial = material;
        _hair.sharedMesh = mesh;
    }

    public void SetDress(Material material, Mesh mesh)
    {
        _dress.sharedMaterial = material;
        _dress.sharedMesh = mesh;
    }
    public void SetSkirt(Material material, Mesh mesh)
    {
        _skirt.sharedMaterial = material;
        _skirt.sharedMesh = mesh;
    }

    public void SetShoes(Material material, Mesh mesh)
    {
        _shoes.sharedMaterial = material;
        _shoes.sharedMesh = mesh;
    }
}
