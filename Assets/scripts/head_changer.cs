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
        choose_dude = spawner.GetComponent<choose_dude>();

        parts = new GameObject[4][] { spawner_script.head, spawner_script.eyes, spawner_script.nose, spawner_script.mouth };
        generateFace();
    }

    [ContextMenu("generateFace")]
    private void generateFace()
    {
        //Debug.Log(spawner_script.all_dudes.ElementAt(0));
        prop = spawner_script.all_dudes.ElementAt(Random.Range(0, spawner_script.all_dudes.Count));
        //prop = new int[4] { Random.Range(0, head.Length), Random.Range(0, eyes.Length), Random.Range(0, nose.Length), Random.Range(0, mouth.Length) };

        
        for (int i = 0; i < prop.Length; i++)
        {
            Instantiate(parts[i][prop[i]], transform);
        }

        choose_dude.created_dudes.Add(prop);
        spawner_script.all_dudes.Remove(prop);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            choose_dude.IsTheDude(prop);
        }
         
        //Debug.Log('k');
    }
}
