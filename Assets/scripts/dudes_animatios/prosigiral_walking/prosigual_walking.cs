using UnityEngine;

public class prosigual_walking : MonoBehaviour
{
    public Vector3 carent_position;
    public bool is_steping = false;

    [SerializeField] target_to_go target_to_go;

    void Start()
    {
        carent_position = transform.position;
    }

    void Update()
    {
        if (!is_steping)
        {
            transform.position = carent_position;
        }
    }
}