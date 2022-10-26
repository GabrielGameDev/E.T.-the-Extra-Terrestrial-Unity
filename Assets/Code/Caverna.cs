using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caverna : MonoBehaviour
{

    public GameObject[] phoneParts;

	public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
		LevelController lc = LevelController.instance;
		if (lc.chosenHoles.Contains(lc.currentHole))
		{
			for (int i = 0; i < 3; i++)
			{
				if(lc.currentHole == lc.chosenHoles[i])
				{
					if(lc.GetTelephoneIndex(i) != i)
					{
						phoneParts[i].SetActive(true);
						currentIndex = i;
					}
						
				}
			}
		}
    }

   
}
