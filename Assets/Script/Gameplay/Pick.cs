using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pick
{
    private MouseWorldPosition _mouseWorldPosition;
    private CheckingUpperBlock _checkingUpperBlock;

    private float _zDistanceFromCamera;
    private Camera _mainCamera;

    public Pick(MouseWorldPosition mouseWorldPosition, CheckingUpperBlock checkingUpperBlock)
    {
        _mainCamera = Camera.main;
        _mouseWorldPosition = mouseWorldPosition;
        _checkingUpperBlock = checkingUpperBlock;
    }

    public void Select(ref Rigidbody selectedBlock, ref Vector3 offset, ref Plane movementPlane, ref Vector3 initialBlockPosition)
    {
        if (TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor))
        {
            if (CanBlockBeSelected(rb, monitor, hit))
            {
                ConfigureSelectedBlock(ref selectedBlock, rb, ref initialBlockPosition, ref movementPlane, ref offset, hit);
            }
        }
    }

    private bool CanBlockBeSelected(Rigidbody rb, ContactMonitor monitor, RaycastHit hit)
    {
        if (rb == null && monitor == null) return false;

        if (_checkingUpperBlock.IsBlockOnTop(hit.collider.gameObject))
        {
            Debug.Log("Этот блок является верхним и его нельзя выбрать!");
            return false; // Блок верхний — запретить выбор
        }

        Debug.Log("CanBlockBeSelected");
        return true;
    }

    private void ConfigureSelectedBlock(ref Rigidbody selectedBlock, Rigidbody rb, ref Vector3 initialBlockPosition, ref Plane movementPlane, ref Vector3 offset, RaycastHit hit)
    {
        selectedBlock = rb;
        initialBlockPosition = selectedBlock.position;
        _zDistanceFromCamera = Vector3.Distance(_mainCamera.transform.position, selectedBlock.position);
        movementPlane = new Plane(-_mainCamera.transform.forward, hit.point);
        offset = selectedBlock.position - _mouseWorldPosition.GetMouseWorldPosition(movementPlane);

        // Замораживаем вращение, чтобы блок не крутился
        selectedBlock.constraints = RigidbodyConstraints.FreezeRotation;

        selectedBlock.GetComponent<ContactMonitor>().ClearContacts();

        Debug.Log("ConfigureSelectedBlock");
    }

    private bool TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast попал в блок: " + hit.collider.gameObject.name);
            rb = hit.collider.GetComponent<Rigidbody>();
            monitor = hit.collider.GetComponent<ContactMonitor>();
            return true;
        }

        Debug.Log("Raycast не попал в блок");
        rb = null;
        monitor = null;
        return false;
    }
}
