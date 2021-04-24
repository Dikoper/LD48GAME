using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G : MonoBehaviour
{
    [SerializeField] GameObject player_prefab;


    GameObject player;

    enum State
    {
        InMenu = 0,
        Intro = 1,
        InGame = 2,
        Ending = 3
    }

    State currentGameState {set; get;}

    void Awake() 
    {
       DontDestroyOnLoad(this.gameObject); 
    }
    void Start()
    {
        currentGameState = State.InMenu;
        SceneManager.LoadScene("test1");
        
        SceneManager.sceneLoaded += SceneLoaderHandler;
    }

    void SceneLoaderHandler(Scene scene, LoadSceneMode mode)
    {
       player = Instantiate(player_prefab, Vector3.one, Quaternion.identity);
    }

    void Update()
    {
        
    }

    void StateManager(State st)
    {
        switch (st)
        {
            case State.InMenu:
                SceneManager.LoadScene("Menu");
            break;
            case State.Intro:
                SceneManager.LoadScene("Intro");
            break;
            case State.InGame:
                SceneManager.LoadScene("Game");
            break;
            case State.Ending:
                SceneManager.LoadScene("Ending");
            break;
            default:
                throw new System.Exception("Unhandled state " + st);
        }

    }
}
