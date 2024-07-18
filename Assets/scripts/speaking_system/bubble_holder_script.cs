using UnityEngine;

public class bubble_holder_script : MonoBehaviour
{
    [SerializeField] speeking_manager speeking_manager;

    private GameObject bubble_canvas;
    private GameObject conteiner;

    private void Start()
    {
        bubble_canvas = transform.GetChild(0).gameObject;
        conteiner = transform.parent.gameObject;
    }

    private void OnMouseDown()
    {
        speeking_manager.swich_skip_sent_imput();
    }

    public void turn()
    {
        conteiner.transform.Rotate(Vector3.up, Mathf.PI);
        bubble_canvas.transform.Rotate(Vector3.up, Mathf.PI);
    }
}
