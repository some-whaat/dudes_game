using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class choose_dude : MonoBehaviour
{
    public HashSet<int[]> created_dudes;

    public int[] wanted_dude;

    [SerializeField] dude_visualaiser dude_visualaiser;

    private int score = 0;
    [SerializeField] Text scoreText;

    private bool isntcalled = true;

    void Start()
    {
        created_dudes = new HashSet<int[]>();
    }

    private void Update()
    {
       if (created_dudes.Count >= 3 && isntcalled) 
       {
            Dude_to_Find();
            isntcalled = false;
       }
    }

    [ContextMenu("Dude_to_Find")]
    public void Dude_to_Find()
    {
        wanted_dude = created_dudes.ElementAt(Random.Range(0, created_dudes.Count));
        dude_visualaiser.SetDude(wanted_dude);
    }

    public void IsTheDude(int[] prop)
    {
        if (prop == wanted_dude)
        {
            Debug.Log("thedude");

            score += 1;
            scoreText.text = score.ToString();

            Dude_to_Find();
        }
        else
        {
            Debug.Log("nope");
        }
    }
}
