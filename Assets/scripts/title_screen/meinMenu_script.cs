using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meinMenu_script : MonoBehaviour
{
    [SerializeField] int siart_amount_of_floors = 1;
    [SerializeField] int siart_amount_spawned_plates = 11;
    [SerializeField] int siart_amound_dudes_to_spawn = 3;

    [SerializeField] home_geniration home_geniration;

    public void start_tutorial()
    {
        PlayerPrefs.SetInt("amount_of_floors", siart_amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", siart_amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", siart_amound_dudes_to_spawn);
        PlayerPrefs.SetInt("nomber_of_iteration", 0);

        DOTween.KillAll();
        SceneManager.LoadScene("tutorial_scene");
    }

    public void start_the_game()
    {
        PlayerPrefs.SetInt("amount_of_floors", siart_amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", siart_amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", siart_amound_dudes_to_spawn);
        PlayerPrefs.SetInt("nomber_of_iteration", 1);

        DOTween.KillAll();
        SceneManager.LoadScene("test_scene");
    }

    public void quit_the_game()
    {
        Application.Quit();
    }
}
