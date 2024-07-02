using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class choose_dude : MonoBehaviour
{
    [SerializeField] manadger_script manadger_script;
    [SerializeField] timer_script timer_script;
    [SerializeField] dude_visualaiser dude_visualaiser;
    [SerializeField] camera_whatch camera_whatch;
    private Camera cam;

    public HashSet<int[]> created_dudes;

    public int[] wanted_dude;

    [SerializeField] LayerMask mask;

    private int score = 0;
    
    private bool isntcalled = true;


    void Start()
    {
        created_dudes = new HashSet<int[]>();

        cam = Camera.main;
    }

    private void Update()
    {
       if (created_dudes.Count >= 2 && isntcalled) 
       {
            Dude_to_Find();
            isntcalled = false;
       }


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 10f;
            mouse_pos = cam.ScreenToWorldPoint(mouse_pos);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 999999, mask))
            {
                if (hit.collider.gameObject.tag == "dude")
                {
                    IsTheDude(hit.collider.GetComponent<head_changer>().prop);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 10f;
            mouse_pos = cam.ScreenToWorldPoint(mouse_pos);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 999999, mask))
            {
                if (hit.collider.gameObject.tag != "dude")
                {
                    camera_whatch.target = hit.collider.gameObject.transform;
                    camera_whatch.change_pos();
                }
            }
        }
    }

    [ContextMenu("Dude_to_Find")]
    public void Dude_to_Find()
    {
        wanted_dude = created_dudes.ElementAt(Random.Range(0, created_dudes.Count));
        dude_visualaiser.SetDude(wanted_dude);
    }

    public void IsTheDude(int[] prop)
    {
        if (prop == wanted_dude)
        {
            score += 1;

            manadger_script.new_level();

            //Dude_to_Find();
        }
        else
        {
            timer_script.timer_time -= 10;
        }
    }
}
