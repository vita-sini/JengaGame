using UnityEngine;
using YG;

public class MaterialApplier : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Renderer _renderer;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += ApplyMaterial;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ApplyMaterial;
    }

    private void Start()
    {
        // Если SDK уже загружен — сразу применяем материал
        if (YandexGame.SDKEnabled)
            ApplyMaterial();
    }

    private void ApplyMaterial()
    {
        int selectedIndex = YandexGame.savesData.selectedMaterialIndex;

        if (selectedIndex >= 0 && selectedIndex < _materials.Length)
            _renderer.material = _materials[selectedIndex];
    }
}
