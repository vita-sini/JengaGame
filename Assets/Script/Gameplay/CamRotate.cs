using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _rotationSpeed = 50f;
    private float _scrollSpeed = 10f; // —корость перемещени€ камеры по оси Y (вверх/вниз при прокрутке колесика)

    // ќграничение по минимальной и максимальной высоте (по оси Y)
    private float _minHeight = 4f;
    private float _maxHeight = 60f;

    private Manipulation _manipulation;

    private void Start()
    {
        _manipulation = FindObjectOfType<Manipulation>();
    }

    private void Update()
    {
        if (_manipulation != null && _manipulation.IsBlockHeld)
            return;

        if (Input.GetKey(KeyCode.Q))
            RotateAroundTarget(-1);

        if (Input.GetKey(KeyCode.E))
            RotateAroundTarget(1);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
            MoveCameraVertically(scroll);
    }

    private void RotateAroundTarget(float direction)
    {
        // ¬ращаем вокруг оси Y
        transform.RotateAround(_target.position, Vector3.up, direction * _rotationSpeed * Time.deltaTime);
    }

    private void MoveCameraVertically(float scrollAmount)
    {
        // ѕолучаем текущее положение камеры
        Vector3 position = transform.position;

        // »змен€ем высоту камеры в зависимости от прокрутки колесика
        float newY = position.y + scrollAmount * _scrollSpeed;

        // ќграничиваем высоту камеры в пределах minHeight и maxHeight
        newY = Mathf.Clamp(newY, _minHeight, _maxHeight);

        // ”станавливаем новое положение камеры
        position.y = newY;
        transform.position = position;
    }
}
