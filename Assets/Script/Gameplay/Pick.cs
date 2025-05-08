using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pick
{
    private MouseWorldPosition _mouseWorldPosition;
    private CheckingUpperBlock _checkingUpperBlock;

    private float _zDistanceFromCamera;
    private Camera _mainCamera;
    public Vector3 InitialMouseWorldPosition { get; private set; }
    public Ray InitialMouseRay { get; private set; }
    public Vector3 _initialCameraRight { get; private set; }

    public Pick(MouseWorldPosition mouseWorldPosition, CheckingUpperBlock checkingUpperBlock)
    {
        _mainCamera = Camera.main;
        _mouseWorldPosition = mouseWorldPosition;
        _checkingUpperBlock = checkingUpperBlock;
    }

    public void Select(ref Rigidbody selectedBlock, ref Vector3 offset, ref Plane movementPlane, ref Vector3 initialBlockPosition)
    {
        Debug.Log("Select() called");

        if (TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor))
        {
            Debug.Log("TryGetRaycastHitBlock success");

            if (CanBlockBeSelected(rb, monitor, hit))
            {
                Debug.Log("CanBlockBeSelected success");

                ConfigureSelectedBlock(ref selectedBlock, rb, ref initialBlockPosition, ref movementPlane, ref offset, hit);

                Debug.Log("Selected block: " + selectedBlock.name);
            }
            else
            {
                Debug.Log("CanBlockBeSelected failed");
            }
        }
        else
        {
            Debug.Log("TryGetRaycastHitBlock failed");
        }
    }

    private bool CanBlockBeSelected(Rigidbody rb, ContactMonitor monitor, RaycastHit hit)
    {
        if (rb == null)
        {
            Debug.Log("Rigidbody is null");
            return false;
        }

        GameObject blockObject = rb.gameObject;

        // ќбновл€ем список верхних блоков
        _checkingUpperBlock.UpdateTopBlock();

        // ѕроверка: если блок верхний Ч запрещаем выбор
        if (_checkingUpperBlock.IsBlockOnTop(blockObject))
        {
            Debug.Log("Block is on top and cannot be selected.");
            return false;
        }

        Debug.Log("CanBlockBeSelected passed");
        return true;
    }

    private void ConfigureSelectedBlock(ref Rigidbody selectedBlock, Rigidbody rb, ref Vector3 initialBlockPosition, ref Plane movementPlane, ref Vector3 offset, RaycastHit hit)
    {
        selectedBlock = rb;
        initialBlockPosition = selectedBlock.position;
        _zDistanceFromCamera = Vector3.Distance(_mainCamera.transform.position, selectedBlock.position);
        movementPlane = new Plane(-_mainCamera.transform.forward, hit.point);
        offset = selectedBlock.position - _mouseWorldPosition.GetMouseWorldPosition(movementPlane);
        _initialCameraRight = _mainCamera.transform.right;


        selectedBlock.constraints = RigidbodyConstraints.FreezeRotation;
        selectedBlock.GetComponent<ContactMonitor>().ClearContacts();
    }

    private bool TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor)
    {
        // если стара€ камера ушла вместе со сценой Ц берЄм новую
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            rb = hit.collider.GetComponent<Rigidbody>();
            monitor = hit.collider.GetComponent<ContactMonitor>();
            Debug.Log("Raycast попал в блок: " + hit.collider.gameObject.name);
            return true;
        }

        rb = null;
        monitor = null;
        Debug.Log("Raycast не попал в блок");
        return false;
    }
}
