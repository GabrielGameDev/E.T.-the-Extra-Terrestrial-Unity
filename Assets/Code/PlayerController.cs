using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float levitateForce = 20;
    public bool canLevitate;
    public bool falling;
    bool jumping;

    Rigidbody rb;
    Animator animator;

    public UnityEvent OnWalk;
    public UnityEvent OnLevitate, OnFalling;

    public AudioClip levitateSound, fallSound;
    public AudioClip[] stepSounds;
 
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        animator.SetBool("jumping", falling);
        if (falling && !jumping)
        {
            OnFalling.Invoke();
            audioSource.clip = fallSound;
            if (!audioSource.isPlaying)
                audioSource.Play();

            return;
        }


        if (Input.GetButton("Jump") && canLevitate)
        {
            falling = true;
            jumping = true;
            OnLevitate.Invoke();
            rb.AddForce(Vector3.up * levitateForce);

            audioSource.clip = levitateSound;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
		else
		{
            jumping = false;
		}


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0, v);
        direction.Normalize();

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        if(direction != Vector3.zero)
            rb.MoveRotation(Quaternion.LookRotation(direction));

        bool walking = direction != Vector3.zero ? true : false;
        animator.SetBool("walk", walking);
		if (walking)
		{
            OnWalk.Invoke();
		}

		
    }

	private void OnCollisionEnter(Collision collision)
	{
        falling = false;
	}

    public void PlayFootStep()
	{
        audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)], 0.5f);
	}
}
