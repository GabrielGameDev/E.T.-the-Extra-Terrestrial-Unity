using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FBIAgent : MonoBehaviour
{

    public GameObject myCam;

    Transform player;
    NavMeshAgent agent;
    bool chasing;

    Vector3 startPoint;

	AudioSource audioSource;
    void OnEnable()
    {
        PlayerInteractions.OnChangeScreen += SetState;
    }


    void OnDisable()
    {
        PlayerInteractions.OnChangeScreen -= SetState;
    }

    // Start is called before the first frame update
    void Start()
	{
		audioSource = GetComponent<AudioSource>();
		startPoint = transform.position;
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		CheckChase();
	}

	private void CheckChase()
	{
		for (int i = 0; i < 3; i++)
		{
			if (LevelController.instance.GetTelephoneIndex(i) == i)
			{
				if (myCam.activeInHierarchy)
					chasing = true;
			}

		}
	}

	// Update is called once per frame
	void Update()
    {
		if (chasing)
		{
            agent.SetDestination(player.position);
		}
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			audioSource.Play();
            chasing = false;
		}

        LevelController.instance.DisableTelephone();
	}

    void SetState()
	{
        if (chasing)
		{
            chasing = false;
            agent.destination = startPoint;
        }
		else
		{
			CheckChase();
		}
            
	}
}
