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

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    [ContextMenu("create a level!")]
    void RegenirateALevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
