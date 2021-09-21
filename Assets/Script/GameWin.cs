using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameWin : MonoBehaviour
{
    public Image black;
    public Animator anim;
    
    void Start()
    {
        StartCoroutine(Fading());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Fading();
        }
       
    }
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
    }
  
}
