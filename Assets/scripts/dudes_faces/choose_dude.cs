using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class choose_dude : MonoBehaviour
{
    manadger_script manadger_script;
    speeking_manager speeking_manager;
    catscene_manager catscene_manager;
    [SerializeField] timer_script timer_script;
    [SerializeField] dude_visualaiser dude_visualaiser;

    public HashSet<int[]> created_dudes;

    public int[] wanted_dude;

    [SerializeField] LayerMask mask;

    private int score = 0;

    bool isntcalled = true;

    void Start()
    {
        GameObject manadger = GameObject.FindWithTag("manager");
        manadger_script = manadger.GetComponent<manadger_script>();
        speeking_manager = manadger.GetComponent<speeking_manager>();
        catscene_manager = manadger.GetComponent<catscene_manager>();


        created_dudes = new HashSet<int[]>();
    }

    private void Update()
    {
        if (created_dudes.Count >= 2 && isntcalled)
        {
            Dude_to_Find();
            isntcalled = false;
        }
    }

    [ContextMenu("Dude_to_Find")]
    public void Dude_to_Find()
    {
        if (timer_script.enabled == false && !manadger_script.do_tutorial)
        {
            timer_script.enabled = true;
        }

        wanted_dude = created_dudes.ElementAt(Random.Range(0, created_dudes.Count));
        dude_visualaiser.SetDude(wanted_dude);
    }

    public void IsTheDude(int[] prop, animation_script animation_script)
    {
        if (manadger_script.do_tutorial)
        {
            if (prop == wanted_dude)
            {
                score += 1;

                animation_script.catch_animation(false);

                catscene_manager.had_finded_dude = true;
            }
            else
            {
                StartCoroutine(speeking_manager.nope());
            }
        }

        else
        {
            if (prop == wanted_dude)
            {
                score += 1;

                animation_script.catch_animation();
            }
            else
            {
                timer_script.timer_time -= 10;
            }
        }
    }
}
