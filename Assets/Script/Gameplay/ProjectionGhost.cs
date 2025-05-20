using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionGhost : MonoBehaviour
{
    private Transform ghostInstance;
    private Rigidbody targetBlock;
    private Material ghostMaterial;

    public void Initialize(Rigidbody target, Material material)
    {
        targetBlock = target;
        ghostMaterial = material;

        // ������ ����� �����
        ghostInstance = Instantiate(target.gameObject).transform;
        ghostInstance.name = "GhostProjection";

        // ������� ������ � ��������������
        foreach (var rb in ghostInstance.GetComponentsInChildren<Rigidbody>())
            Destroy(rb);

        foreach (var collider in ghostInstance.GetComponentsInChildren<Collider>())
            Destroy(collider);

        // ��������� ���������� ��������
        foreach (var renderer in ghostInstance.GetComponentsInChildren<Renderer>())
        {
            renderer.material = ghostMaterial;
        }

        // ��������� ���� Ignore Raycast
        ghostInstance.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        MaterialApplier applier = ghostInstance.GetComponent<MaterialApplier>();
        if (applier != null)
        {
            Destroy(applier);
        }
    }

    private void Update()
    {
        if (targetBlock == null || ghostInstance == null) return;

        Vector3 origin = targetBlock.position;
        Vector3 direction = Vector3.down;

        // ���������� ���� ��������
        int mask = ~LayerMask.GetMask("Ignore Raycast");

        if (Physics.Raycast(origin, direction, out RaycastHit hit, 100f, mask))
        {
            Vector3 newPos = hit.point;
            ghostInstance.position = new Vector3(origin.x, newPos.y, origin.z);
            ghostInstance.rotation = targetBlock.rotation;
        }
    }

    public void DestroyGhost()
    {
        if (ghostInstance != null)
        {
            Destroy(ghostInstance.gameObject);
        }
    }
}
