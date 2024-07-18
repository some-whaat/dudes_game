using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manadger_script : MonoBehaviour
{
    public home_geniration home_geniration;

    public int amount_of_floors = 5;
    public int amount_spawned_plates = 44;
    public int amound_dudes_to_spawn = 33;

    private int nomber_of_iteration;

    catscene_manager catscene_manager;

    public string[] sentences;

    public bool do_tutorial = false;

    private void Start()
    {
        catscene_manager = GetComponent<catscene_manager>();

        amount_of_floors = PlayerPrefs.GetInt("amount_of_floors");
        amount_spawned_plates = PlayerPrefs.GetInt("amount_spawned_plates", amount_spawned_plates);
        amound_dudes_to_spawn = PlayerPrefs.GetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);
        nomber_of_iteration = PlayerPrefs.GetInt("nomber_of_iteration", nomber_of_iteration);
        nomber_of_iteration ++;
        PlayerPrefs.SetInt("was_tutorial", 0);

        if (do_tutorial)
        {
            catscene_manager.tutorial();
        }
        else
        {
            home_geniration.genirate_level();
        }
    }

    [ContextMenu("create a level!")]
    public void new_level()
    {
        if (nomber_of_iteration % 2 == 0)
        {
            PlayerPrefs.SetInt("amount_of_floors", amount_of_floors + 1);
        }

        PlayerPrefs.SetInt("amount_spawned_plates", amount_spawned_plates + 5);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", amound_dudes_to_spawn + 3);

        PlayerPrefs.SetInt("nomber_of_iteration", nomber_of_iteration);

        DOTween.KillAll();
        SceneManager.LoadScene("test_scene");
    }
}
