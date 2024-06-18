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

    [SerializeField] float spawn_enterval;

    [SerializeField] GameObject DUDE_prefab;

    public Transform[] destList;

    public HashSet<int[]> all_dudes;

    private float timer;

    void Start()
    {
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

        Instantiate(DUDE_prefab, transform);
    }

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

    private void spawn_dude()
    {
        int[] prop = all_dudes.ElementAt(Random.Range(0, all_dudes.Count));
        GameObject dude = Instantiate(DUDE_prefab, transform);
        dude.GetComponent<head_changer>().prop = prop;
        all_dudes.Remove(prop);
    }
}
