using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manadger_script : MonoBehaviour
{
    //[SerializeField] spawner_script spawner_script;
    [SerializeField] home_geniration home_geniration;

    public int amount_of_floors = 5;
    public int amount_spawned_plates = 44;
    public int amound_dudes_to_spawn = 33;

    private int nomber_of_iteration;

    //

    [SerializeField] speeking_manager speeking_manager;

    [TextArea(3, 16)]
    public string[] sentences;

    private void Start()
    {
        amount_of_floors = PlayerPrefs.GetInt("amount_of_floors");
        amount_spawned_plates = PlayerPrefs.GetInt("amount_spawned_plates", amount_spawned_plates);
        amound_dudes_to_spawn = PlayerPrefs.GetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);
        nomber_of_iteration = PlayerPrefs.GetInt("nomber_of_iteration", nomber_of_iteration);
        nomber_of_iteration ++;

        home_geniration.genirate_level();
        if (PlayerPrefs.GetInt("was_tutorial") != 1)
        {
            speeking_manager.start_speaking(sentences);
        }
    }

    [ContextMenu("create a level!")]
    public void new_level()
    {
        if (nomber_of_iteration % 2 == 0)
        {
            PlayerPrefs.SetInt("amount_of_floors", amount_of_floors + 1);
        }

        //amount_of_floors = (int)Mathf.Round(Mathf.Lerp(1f, 8f, (float)Mathf.Sqrt(nomber_of_iteration / 15f)));
        //PlayerPrefs.SetInt("amount_of_floors", amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", amount_spawned_plates + 5);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", amound_dudes_to_spawn + 3);
        PlayerPrefs.SetInt("nomber_of_iteration", nomber_of_iteration);

        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
