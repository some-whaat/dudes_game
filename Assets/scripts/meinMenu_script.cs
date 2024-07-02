using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meinMenu_script : MonoBehaviour
{
    [SerializeField] int siart_amount_of_floors = 1;
    [SerializeField] int siart_amount_spawned_plates = 11;
    [SerializeField] int siart_amound_dudes_to_spawn = 3;

    public void start_the_game()
    {
        PlayerPrefs.SetInt("amount_of_floors", siart_amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", siart_amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", siart_amound_dudes_to_spawn);
        PlayerPrefs.SetInt("nomber_of_iteration", 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit_the_game()
    {
        Application.Quit();
    }
}
