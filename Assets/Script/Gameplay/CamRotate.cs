using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] private Transform _target; // Сюда можно вставить объект, вокруг которого вращается камера
    // Скорость вращения камеры
    private float _rotationSpeed = 50f;
    // Скорость перемещения камеры по оси Y (вверх/вниз при прокрутке колесика)
    private float _scrollSpeed = 10f;

    // Ограничение по минимальной и максимальной высоте (по оси Y)
    private float _minHeight = 4f;
    private float _maxHeight = 60f;
    // Объект, вокруг которого будет вращаться камера

    private Manipulation _manipulation;

    private void Start()
    {
        _manipulation = FindObjectOfType<Manipulation>();
    }

    private void Update()
    {
        if (_manipulation != null && _manipulation.IsBlockHeld)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            RotateAroundTarget(-1);
        }

        if (Input.GetKey(KeyCode.E))
        {
            RotateAroundTarget(1);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            MoveCameraVertically(scroll);
        }
    }

    // Метод для вращения камеры вокруг цели
    private void RotateAroundTarget(float direction)
    {
        // Вращаем вокруг оси Y
        transform.RotateAround(_target.position, Vector3.up, direction * _rotationSpeed * Time.deltaTime);
    }

    // Метод для перемещения камеры по оси Y (вверх/вниз)
    private void MoveCameraVertically(float scrollAmount)
    {
        // Получаем текущее положение камеры
        Vector3 position = transform.position;

        // Изменяем высоту камеры в зависимости от прокрутки колесика
        float newY = position.y + scrollAmount * _scrollSpeed;

        // Ограничиваем высоту камеры в пределах minHeight и maxHeight
        newY = Mathf.Clamp(newY, _minHeight, _maxHeight);

        // Устанавливаем новое положение камеры
        position.y = newY;
        transform.position = position;
    }
}
