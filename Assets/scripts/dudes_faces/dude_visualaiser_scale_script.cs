using UnityEngine;

public class dude_visualaiser_scale_script : MonoBehaviour
{
    public void change_scale(float zoom)
    {
        transform.localScale = Vector3.one * zoom;
    }
}
