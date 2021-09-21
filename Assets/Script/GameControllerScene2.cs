using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControllerScene2 : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public TrailController TrailController;
    
    //Yellow Bird
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
   
    private bool _isGameEnded = false;
    [Header("Game Over")]
   // public Text gameWinText;
    public GameObject GameOver;
    // public Button reloadButton;
    // public Sprite spriteImage;
   
    void Start()
    {
       
       
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

   
    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        if (Birds.Count == 0 && Enemies.Count>0)
        {
            _isGameEnded = true;
            GameOver.SetActive(true);
          

           
            
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
       
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }
       
        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
           
           GameOver.SetActive(true);    
           
        }
       
        
    }
    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}