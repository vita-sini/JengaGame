using UnityEngine;

public class Movement
{
    private float _maxMoveSpeed = 17f;
    private float _verticalMoveSpeed = 15f;

    private MouseWorldPosition _mouseWorldPosition;
    private Manipulation _manipulation;

    public Movement(MouseWorldPosition mouseWorldPosition, Manipulation manipulation)
    {
        _mouseWorldPosition = mouseWorldPosition;
        _manipulation = manipulation;
    }

    public void MoveMouse(Rigidbody selectedBlock, Vector3 offset)
    {
        //�������������� ����������� �� X � Z
        Plane movementPlane = new Plane(Vector3.up, selectedBlock.position);
        Vector3 mouseTargetPosition = _mouseWorldPosition.GetMouseWorldPosition(movementPlane) + offset;

        // ��������� Y �� ������� ������� (����� �� ������)
        Vector3 horizontalTarget = new Vector3(mouseTargetPosition.x, selectedBlock.position.y, mouseTargetPosition.z);
        Vector3 horizontalDirection = horizontalTarget - selectedBlock.position;
        Vector3 horizontalVelocity = horizontalDirection / Time.fixedDeltaTime;

        if (horizontalVelocity.magnitude > _maxMoveSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * _maxMoveSpeed;
        }

        //������������ ����������� �� ��� Y 
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        else if (Input.GetKey(KeyCode.S)) verticalInput = -1f;

        float verticalVelocity = verticalInput * _verticalMoveSpeed;

        //����������� �������������� � ������������ ��������
        Vector3 finalVelocity = new Vector3(
            horizontalVelocity.x,
            verticalVelocity,
            horizontalVelocity.z
        );

        selectedBlock.velocity = finalVelocity;
    }
}
