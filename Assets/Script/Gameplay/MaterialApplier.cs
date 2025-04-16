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
        Debug.Log("�������� �� GetDataEvent");
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ApplyMaterial;
    }

    private void Start()
    {
        // ���� SDK ��� �������� � ����� ��������� ��������
        if (YandexGame.SDKEnabled)
        {
            Debug.Log("SDK ��� ��������, �������� ApplyMaterial �������");
            ApplyMaterial();
        }
        else
        {
            Debug.Log("SDK ��� �� ��������, ��� GetDataEvent");
        }
    }

    private void ApplyMaterial()
    {
        int selectedIndex = YandexGame.savesData.selectedMaterialIndex;

        Debug.Log("��������� �������� �� �������: " + selectedIndex);

        if (selectedIndex >= 0 && selectedIndex < _materials.Length)
        {
            _renderer.material = _materials[selectedIndex];
            Debug.Log("�������� ������� �������: " + selectedIndex);
        }
        else
        {
            Debug.LogWarning("������ ��������� ��� ���������!");
        }
    }
}
