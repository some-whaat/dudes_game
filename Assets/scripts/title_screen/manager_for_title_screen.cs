using UnityEngine;

public class manager_for_title_screen : MonoBehaviour
{
    [SerializeField] int amount_of_floors = 5;
    [SerializeField] int amount_spawned_plates = 44;
    [SerializeField] int amound_dudes_to_spawn = 33;
    [SerializeField] home_geniration home_geniration;


    private void Start()
    {
        PlayerPrefs.SetInt("amount_of_floors", amount_of_floors);
        PlayerPrefs.SetInt("amount_spawned_plates", amount_spawned_plates);
        PlayerPrefs.SetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);

        home_geniration.genirate_level();
    }
}
