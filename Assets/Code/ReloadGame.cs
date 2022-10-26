using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    public bool destroyLevelController;
    public float waitTime = 5;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if(destroyLevelController)
            Destroy(FindObjectOfType<LevelController>().gameObject);

        yield return new WaitForSeconds(waitTime);
        if(!destroyLevelController)
            Destroy(FindObjectOfType<LevelController>().gameObject);

        SceneManager.LoadScene(0);
    }


   
}
