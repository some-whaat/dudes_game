using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class speeking_manager : MonoBehaviour
{
    [SerializeField] Text speech_text;

    [SerializeField] float typing_speed;

    private Queue<string> speach_queue;
    private string curr_sent;
    
    bool is_speaking = false;
    bool is_typing = false;

    private void Update()
    {
        if (is_speaking)
        {
            if (Input.GetButtonDown("space"))
            {
                if (is_typing)
                {
                    StopCoroutine("display_next_sent");
                    speech_text.text = curr_sent;
                }
                else
                {
                    StartCoroutine("display_next_sent");
                }
            }
        }
    }

    public void start_speaking(string[] in_sentences)
    {
        //вщбавить бабл
        is_speaking = true;
        speach_queue = new Queue<string>();

        foreach (string sentence in in_sentences)
        {
            speach_queue.Enqueue(sentence);
        }

        display_next_sent();
    }

    void display_next_sent()
    {
        if (speach_queue.Count == 0)
        {
            end_speach();
        }
        else
        {
            curr_sent = speach_queue.Dequeue();
            StartCoroutine("typing_animation");
        }
        
    }


    IEnumerator typing_animation()
    {
        //добавить анимацию кура
        is_typing = true;

        foreach (char letter in curr_sent)
        {
            speech_text.text += letter;
            yield return new WaitForSeconds(typing_speed);
        }
        is_typing = false;
    }

    void end_speach()
    {
        curr_sent = "";
        speech_text.text = "";
        // убрать бабл
    }

}
