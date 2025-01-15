using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferClothes : MonoBehaviour
{
    public SkinnedMeshRenderer sourceClothing; // Меш с одеждой
    public SkinnedMeshRenderer targetCharacter; // Целевая модель

    private void Awake()
    {
        AttachClothing();
    }

    public void AttachClothing()
    {
        if (sourceClothing == null || targetCharacter == null)
        {
            Debug.LogError("Не указаны источник или цель для одежды!");
            return;
        }

        // Создаём новый объект для одежды
        GameObject newClothing = new GameObject("Clothing");
        SkinnedMeshRenderer newRenderer = newClothing.AddComponent<SkinnedMeshRenderer>();

        // Переносим меш и материалы
        targetCharacter.sharedMesh = sourceClothing.sharedMesh;
        targetCharacter.sharedMaterials = sourceClothing.sharedMaterials;

        // Привязываем кости
        newRenderer.bones = targetCharacter.bones;
        newRenderer.rootBone = targetCharacter.rootBone;

        // Помещаем одежду как дочерний объект
        newClothing.transform.SetParent(targetCharacter.transform);
    }
}
