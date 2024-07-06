using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class animation_script : MonoBehaviour
{
    manadger_script manadger_script;
    [SerializeField] wolking_manadger wolking_manadger;
    [SerializeField] movement_boids_script movement_boids_script;
    [SerializeField] Collider coll;
    Rigidbody rb;

    [SerializeField] float catch_animation_len = 100f;
    [SerializeField] float catch_animation_speed = 0.01f;
    [SerializeField] float catch_animation_radius = 0.001f;
    [SerializeField] float catch_animation_one_move_time = 0.01f;
    [SerializeField] float catch_animation_cube_side = 0.5f;

    /*
    public void start_catch_animation()
    {
        wolking_manadger.enabled = false;
        movement_boids_script.enabled = false;
    }
    */

    public IEnumerator catch_animation()
    {
        rb = GetComponent<Rigidbody>();

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        wolking_manadger.enabled = false;
        movement_boids_script.enabled = false;
        coll.enabled = false;

        manadger_script = GameObject.FindWithTag("manager").GetComponent< manadger_script>();


        for (float i = 0; i < catch_animation_len; i += Time.deltaTime)
        {
            float angle = i * catch_animation_speed;
            Vector3 sferical_pos = Vector3.zero;
            sferical_pos += new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * catch_animation_radius + transform.position;
            sferical_pos.y -= 0.3f;

            Vector3 random_pos = new Vector3(Random.Range(transform.position.x - catch_animation_cube_side, transform.position.x + catch_animation_cube_side), Random.Range(transform.position.y - catch_animation_cube_side, transform.position.y + catch_animation_cube_side), Random.Range(transform.position.z - catch_animation_cube_side, transform.position.z + catch_animation_cube_side));

            Vector3 start_pos = transform.position;
            //Vector3 end_pos = (sferical_pos + random_pos) / 2;
            Vector3 end_pos = random_pos;

            float current_movement_time = 0;
            while (Vector3.Distance(transform.position, end_pos) > 0)
            {
                current_movement_time += Time.deltaTime;
                transform.position = Vector3.Lerp(start_pos, end_pos, Mathf.Pow(current_movement_time / catch_animation_one_move_time, 2));
                yield return null;
            }

            
            yield return null;
        }

        manadger_script.new_level();
    }
}
