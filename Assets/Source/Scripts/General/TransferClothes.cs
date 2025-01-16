using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferClothes : MonoBehaviour
{
    public SkinnedMeshRenderer sourceClothing; // ��� � �������
    public SkinnedMeshRenderer targetCharacter; // ������� ������

    private void Awake()
    {
        AttachClothing();
    }

    public void AttachClothing()
    {
        if (sourceClothing == null || targetCharacter == null)
        {
            Debug.LogError("�� ������� �������� ��� ���� ��� ������!");
            return;
        }

        // ������ ����� ������ ��� ������
        GameObject newClothing = new GameObject("Clothing");
        SkinnedMeshRenderer newRenderer = newClothing.AddComponent<SkinnedMeshRenderer>();

        // ��������� ��� � ���������
        targetCharacter.sharedMesh = sourceClothing.sharedMesh;
        targetCharacter.sharedMaterials = sourceClothing.sharedMaterials;

        // ����������� �����
        newRenderer.bones = targetCharacter.bones;
        newRenderer.rootBone = targetCharacter.rootBone;

        // �������� ������ ��� �������� ������
        newClothing.transform.SetParent(targetCharacter.transform);
    }
}
