using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

public class speeking_manager : MonoBehaviour
{
    [SerializeField] TMP_Text speech_text;
    [SerializeField] GameObject bubble;
    //[SerializeField] GameObject chickentecktive;
    [SerializeField] chickentecktive_animation_script ch_anim_script;

    [SerializeField] float typing_speed;
    [SerializeField] float bubble_scale = 120f;
    [SerializeField] float bubble_emerging_dur = 1f;

    private Queue<string> speach_queue;
    private string curr_sent;
    
    //public bool tutorial = false;
    public bool is_speaking = false;
    public bool hide_bubble = true;
    public bool is_unskip = false;
    bool is_typing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swich_skip_sent_imput();
        }
    }

    public void start_speaking(string[] in_sentences)
    {
        curr_sent = "";
        speech_text.text = "";

        bubble.transform.DOScale(bubble_scale, bubble_emerging_dur).SetEase(Ease.OutBounce);

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
            speech_text.text = "";
            curr_sent = speach_queue.Dequeue();
            StartCoroutine("typing_animation");
        }
        
    }


    IEnumerator typing_animation()
    {
        is_typing = true;

        foreach (char letter in curr_sent)
        {
            if (!ch_anim_script.is_speaking_anim)
            {
                ch_anim_script.speaking_animation();
            }

            speech_text.text += letter;
            //ch_anim_timer += typing_speed;
            yield return new WaitForSeconds(typing_speed);

        }
        is_typing = false;
    }


    public void skip_sent_imput()
    {
        if (is_speaking)
        {
            if (is_typing)
            {
                StopCoroutine("typing_animation");
                speech_text.text = curr_sent;
                is_typing = false;
            }
            else
            {
                display_next_sent();
            }
        }
    }

    public void swich_skip_sent_imput()
    {
        if (!is_unskip)
        {
            skip_sent_imput();
        }
        else if (is_typing)
        {
            StopCoroutine("typing_animation");
            speech_text.text = curr_sent;
            is_typing = false;
        }
    }


    void end_speach()
    {
        is_speaking = false;
        curr_sent = "";
        speech_text.text = "";

        
        if (hide_bubble)
        {
            bubble.transform.DOScale(0, bubble_emerging_dur).SetEase(Ease.InCubic);
        }
    }
}
