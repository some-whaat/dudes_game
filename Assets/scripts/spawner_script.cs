using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner_script : MonoBehaviour
{
    //[SerializeField] manadger_script manadger_script;

    public GameObject[] head;
    public GameObject[] eyes;
    public GameObject[] nose;
    public GameObject[] mouth;

    //[SerializeField] float spawn_enterval;

    [SerializeField] GameObject DUDE_prefab;

    //public Transform[] destList;

    public HashSet<int[]> all_dudes;

    //private float timer;

    int amound_dudes_to_spawn;

    void Start()
    {
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
                        int[] narr = new int[4] { heads, eyess, noses, mouths };
                        all_dudes.Add(narr);
                    }
                }
            }
        }
    }

    /*
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawn_enterval)
        {
            timer += Time.deltaTime;
        }
        else if (all_dudes.Count != 0)
        {
            timer = 0;
            Instantiate(DUDE_prefab, transform);
            spawn_enterval *= 1.5f;
        }
        //Debug.Log(all_dudes.Count);
    }
    */

    public void spawn_dudes(HashSet<Vector3> poses)
    {
        amound_dudes_to_spawn = PlayerPrefs.GetInt("amound_dudes_to_spawn", amound_dudes_to_spawn);

        for (int i = 0; i < amound_dudes_to_spawn; i++)
        {
            Vector3 pos = poses.ElementAt(Random.Range(0, poses.Count));
            Debug.Log(pos);
            GameObject dude = Instantiate(DUDE_prefab, transform);
            pos.y += 1;
            dude.transform.position = pos;
            poses.Remove(pos);
        }
        Debug.Log("spawned");
    }
}
