using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class head_changer : MonoBehaviour
{
    [SerializeField] GameObject[][] parts;

    public int[] prop;

    private GameObject spawner;
    private spawner_script spawner_script;
    private choose_dude choose_dude;

    void Start()
    {
        
        spawner = GameObject.FindGameObjectWithTag("spawner");
        spawner_script = spawner.GetComponent<spawner_script>();
        try { choose_dude = spawner.GetComponent<choose_dude>(); }
        catch { Debug.Log("no choosedude"); }

        parts = new GameObject[4][] { spawner_script.head, spawner_script.eyes, spawner_script.nose, spawner_script.mouth };
        generateFace();
    }

    [ContextMenu("generateFace")]
    private void generateFace()
    {
        prop = spawner_script.all_dudes.ElementAt(Random.Range(0, spawner_script.all_dudes.Count));


        for (int i = 0; i < prop.Length; i++)
        {
            Instantiate(parts[i][prop[i]], transform);
        }

        if (choose_dude)
        {
            choose_dude.created_dudes.Add(prop);
        }
        
        spawner_script.all_dudes.Remove(prop);
    }
}
