using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner_script : MonoBehaviour
{
    public GameObject[] head;
    public GameObject[] eyes;
    public GameObject[] nose;
    public GameObject[] mouth;

    [SerializeField] GameObject DUDE_prefab;

    public HashSet<int[]> all_dudes;

    int amound_dudes_to_spawn;

    choose_dude choose_dude;

    public void spawn_dudes(HashSet<Vector3> poses)
    {
        try { choose_dude = transform.gameObject.GetComponent<choose_dude>(); }
        catch { choose_dude = null;}
        
        //создаю сет со всеми возможными чувачками
        amound_dudes_to_spawn = PlayerPrefs.GetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);

        all_dudes = new HashSet<int[]>();

        for (int heads = 0; heads < head.Length; heads++)
        {
            for (int eyess = 0; eyess < eyes.Length; eyess++)
            {
                for (int noses = 0; noses < nose.Length; noses++)
                {
                    for (int mouths = 0; mouths < mouth.Length; mouths++)
                    {
                        int[] prop = new int[4] { heads, eyess, noses, mouths };
                        all_dudes.Add(prop);
                    }
                }
            }
        }

        if (choose_dude != null)
        {
            choose_dude.enabled = true;
        }

        //спавню чувачков
        for (int i = 0; i < amound_dudes_to_spawn; i++)
        {
            Vector3 pos = poses.ElementAt(Random.Range(0, poses.Count));
            GameObject dude = Instantiate(DUDE_prefab, transform);
            dude.transform.position = pos;
            poses.Remove(pos);
        }
    }
}
