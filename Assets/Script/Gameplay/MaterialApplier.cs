using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MaterialApplier : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Renderer _renderer;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += ApplyMaterial;
        Debug.Log("Подписка на GetDataEvent");
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ApplyMaterial;
    }

    private void Start()
    {
        // Если SDK уже загружен — сразу применяем материал
        if (YandexGame.SDKEnabled)
        {
            Debug.Log("SDK уже загружен, вызываем ApplyMaterial вручную");
            ApplyMaterial();
        }
        else
        {
            Debug.Log("SDK ещё не загружен, ждём GetDataEvent");
        }
    }

    private void ApplyMaterial()
    {
        int selectedIndex = YandexGame.savesData.selectedMaterialIndex;

        Debug.Log("Применяем материал по индексу: " + selectedIndex);

        if (selectedIndex >= 0 && selectedIndex < _materials.Length)
        {
            _renderer.material = _materials[selectedIndex];
            Debug.Log("Материал успешно применён: " + selectedIndex);
        }
        else
        {
            Debug.LogWarning("Индекс материала вне диапазона!");
        }
    }
}
