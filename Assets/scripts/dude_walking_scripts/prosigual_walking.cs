using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class prosigual_walking : MonoBehaviour
{
    public Vector3 carent_position;
    public bool is_steping = false;

    [SerializeField] target_to_go target_to_go;
    [SerializeField] LayerMask ground_mask;

    Ray ray;
    

    void Start()
    {
        carent_position = transform.position;
    }

    void Update()
    {
        /*
        Vector3 lerp = Vector3.Lerp(transform.position, carent_position, step_velosyty * Time.deltaTime);
        transform.position = lerp;
        */
        if (!is_steping)
        {
            transform.position = carent_position;
        }
    }

    public void MakeStep(Vector3 new_pos)
    {
        carent_position = ProjetOnPlain((new_pos + target_to_go.point) / 2);
    }
    /*
    //public IEnumerator MakeStep(Vector3 new_pos)
    public void MakeStep(Vector3 new_pos)
    {
        //carent_position = new_pos;
        //carent_position = new Vector3[2] { new_pos, target_to_go.point }[Random.Range(0,2)];

        carent_position = ProjetOnPlain((new_pos + target_to_go.point) / 2);
        
        Vector3 ex_pos = transform.position;
        Vector3 need_pos = ProjetOnPlain((new_pos + target_to_go.point) / 2);
        float dist = Vector3.Distance(ex_pos, need_pos);
        //float t = 0.5f;
        while (carent_position != need_pos)
        {
            float t = Vector3.Distance(transform.position, ex_pos) / dist + step_velosyty;
            //t += step_velosyty;
            carent_position = Vector3.Lerp(ex_pos, need_pos, t);
            transform.position = carent_position;
            yield return null;
        }
        


    }
*/
    public Vector3 ProjetOnPlain(Vector3 pos)
    {
        ray = new Ray(pos, transform.up);
        if (Physics.Raycast(ray, out RaycastHit hitup, 1000, ground_mask))
        {
            return hitup.point;
        }

        ray = new Ray(pos, transform.up);
        if (Physics.Raycast(ray, out RaycastHit hitdown, 1000, ground_mask))
        {
            return hitdown.point;
        }
        return pos;
    }

    /*
    public IEnumerator MakeStep(Vector3 new_pos)
    {
        //carent_position = new_pos;
        //carent_position = new Vector3[2] { new_pos, target_to_go.point }[Random.Range(0,2)];
        Vector3 need_pos = (new_pos + target_to_go.point) / 2;
        Vector3 mid_pos = new Vector3((need_pos.x + carent_position.x) / 2, (target_manager.transform.position.y + carent_position.y) / 2 , need_pos.y);
        Vector3 ex_pos = carent_position;

        transform.position = mid_pos;
        carent_position = mid_pos;

        yield return new WaitForSeconds(step_duration / (need_pos - ex_pos).magnitude);

        carent_position = need_pos;
    }
    */
}