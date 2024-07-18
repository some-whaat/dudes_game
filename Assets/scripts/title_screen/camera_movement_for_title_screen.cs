using UnityEngine;

public class camera_movement_for_title_screen : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
