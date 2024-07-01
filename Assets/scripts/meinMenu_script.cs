using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meinMenu_script : MonoBehaviour
{
    [SerializeField] int siart_amount_of_floors = 3;
    [SerializeField] int siart_amount_spawned_plates = 22;
    [SerializeField] int siart_amound_dudes_to_spawn = 5;

    public void start_the_game()
    {
        PlayerPrefs.SetInt("amount_of_floors", siart_amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", siart_amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", siart_amound_dudes_to_spawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit_the_game()
    {
        Application.Quit();
    }
}
