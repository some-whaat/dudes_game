using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_holder_script : MonoBehaviour
{

    [SerializeField] speeking_manager speeking_manager;

    private void OnMouseDown()
    {
        speeking_manager.skip_sent_imput();
    }
}
