using UnityEngine;

public class target_to_go : MonoBehaviour
{
    public Vector3 point;
    Ray ray;
    public LayerMask ground_mask;

    void Update()
    {
        ray = new Ray(transform.position, transform.up * -1.0f);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, ground_mask))
        {
            point = hit.point;
        }
    }
}
