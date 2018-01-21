using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orenAnimMgr : MonoBehaviour {
    private Animator anim;
    private AudioSource audioSrc;
    public float spawnTime = 10f;
    public float timer = 0f;
    public bool play = false;
    public bool playOnce = false;
    private bool isDead = false;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer - Time.deltaTime >= spawnTime && !isDead)
        {            
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            anim.SetTrigger("triggerOren");

            timer = 0;
        }
        if (GameControl.instance.gameOver)
        {
            isDead = true;
        }
        

    }

  

    
}
