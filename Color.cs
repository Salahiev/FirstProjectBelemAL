using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    Animator myAnim;
    [SerializeField] bool Delete;
    [SerializeField] bool oneclick=true;
    [SerializeField] bool twolevel = false;
    [SerializeField] string playAnimOne;
    [SerializeField] string playAnimTwo;
    [SerializeField] KeyCode KeyAnimOne;
    [SerializeField] AudioClip soundAnimOne;
    [SerializeField] Vector3 posBlur;
    [SerializeField] GameObject blur;
    [SerializeField] GameObject bubbles;
    Vector2 pos = new Vector2(0, 0);

    void Start()
    { 
        myAnim = GetComponent<Animator>(); 
        Invoke("sound", 1f);
    }
    private void OnMouseOver()
    {   
            if (oneclick)
            {
                if (Input.GetKeyUp(KeyAnimOne))
                {
                    myAnim.Play(playAnimOne);
                    Instantiate(bubbles, pos, Quaternion.identity);
                    Instantiate(blur, posBlur, Quaternion.identity);
                    if (Delete)
                    {
                        Invoke("delete", 1f);
                    }
                    oneclick = false;
                    twolevel = true; 
                 }
            }
            else if (twolevel)
        {
            if (Input.GetKeyUp(KeyAnimOne))
            {
                oneclick = false;
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().PlayOneShot(soundAnimOne);
                        myAnim.Play(playAnimTwo);
                    }
                }
            }
        }
    }
    void delete()
    {
       var BlurDelete = GameObject.FindGameObjectsWithTag("Blur");
        for (int i = 0; i < BlurDelete.Length; i++)
        {
            Destroy(BlurDelete[i]);
        }    
    }
    void sound()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
           GetComponent<AudioSource>().PlayOneShot(soundAnimOne);
        }
    }


}
