using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manadger_script : MonoBehaviour
{
    //[SerializeField] spawner_script spawner_script;
    //[SerializeField] home_geniration home_geniration;

    public int amount_of_floors = 5;
    public int amount_spawned_plates = 44;
    public int amound_dudes_to_spawn = 33;

    private void Start()
    {
        amount_of_floors = PlayerPrefs.GetInt("amount_of_floors");
        amount_spawned_plates = PlayerPrefs.GetInt("amount_spawned_plates", amount_spawned_plates);
        amound_dudes_to_spawn = PlayerPrefs.GetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);
    }

    [ContextMenu("create a level!")]
    void RegenirateALevel()
    {
        PlayerPrefs.SetInt("amount_of_floors", amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
