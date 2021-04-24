using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G : MonoBehaviour
{
    [SerializeField] GameObject player_prefab;
    [SerializeField] GameObject fadefx_prefab;
    [SerializeField] GameObject dm_prefab;


    GameObject player;
    GameObject fadeFx;
    GameObject dm;

    public enum State
    {
        InMenu = 0,
        Intro = 1,
        InGame = 2,
        Ending = 3
    }

    public State currentGameState {set; get;}
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
        StateManager(State.InGame);
        player = Instantiate(player_prefab, Vector3.one, Quaternion.identity);
        player.GetComponent<PlayerControlller>().Death += PlayerDeathHandler;
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
                //SceneManager.LoadScene("Game");
                InitGame();
            break;
            case State.Ending:
                SceneManager.LoadScene("Ending");
            break;
            default:
                throw new System.Exception("Unhandled state " + st);
        }
    }

    void InitGame()
    {
        
        dm = Instantiate(dm_prefab, Vector3.zero, Quaternion.identity);
        Debug.Log(dm.transform.position);
        dm.GetComponent<DreamMan>().NewZone += ZoneTranslation;
        fadeFx = Instantiate(fadefx_prefab);
    }

    void ZoneTranslation(Vector3 pos)
    {
        Debug.Log("Trigger zone at - " + pos);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        fadeFx.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(1);
        fadeFx.GetComponent<Animator>().SetTrigger("Out");
    }

    void PlayerDeathHandler()
    {
        Debug.Log("Wake up, you are OBOSRALSYA");
    }
}
