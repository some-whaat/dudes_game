using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class dude_visualaiser : MonoBehaviour
{
    public GameObject[] parts;

    private GameObject[] old_parts;

    private spawner_script spawner_script;



    void Start()
    {
        old_parts = new GameObject[4];
        spawner_script = GameObject.FindGameObjectWithTag("spawner").GetComponent<spawner_script>();
    }

    [ContextMenu("generateFace")]
    public void SetDude(int[] prop)
    {
        parts = new GameObject[4] { spawner_script.head[prop[0]], spawner_script.eyes[prop[1]], spawner_script.nose[prop[2]], spawner_script.mouth[prop[3]] };

        if (old_parts.Length != 0)
        {
            for (int i = 0; i < old_parts.Length; i++)
            {
                GameObject delobj = old_parts[i] as GameObject;
                Destroy(delobj);
            }
        }

        for (int i = 0; i < parts.Length; i++)
        {
            GameObject obj = Instantiate(parts[i], transform);
            MeshRenderer obj_rend = obj.GetComponent<MeshRenderer>();
            if (obj_rend != null)
            {
                obj_rend.shadowCastingMode = ShadowCastingMode.Off;
            }
            else
            {
                obj_rend = obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

                if (obj_rend != null)
                {
                    obj_rend.shadowCastingMode = ShadowCastingMode.Off;
                }

            }
            old_parts[i] = obj;
        }

    }
}
