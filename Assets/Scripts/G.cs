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
        StateManager(State.InMenu);
        // currentGameState = State.InMenu;
        // SceneManager.LoadScene("Game");
        // SceneManager.sceneLoaded += GameSceneLoaderHandler;
    }

    void GameSceneLoaderHandler(Scene scene, LoadSceneMode mode)
    {
        InitGame();
        Debug.Log("Handle player inst");
        player = Instantiate(player_prefab, Vector3.one, Quaternion.identity);
        player.GetComponent<PlayerControlller>().Death += PlayerDeathHandler;
    }

    void Update()
    {

    }

    public void CutsceneIsOver()
    {
        Debug.Log("EndCutscene");
        if(currentGameState < State.Ending)
            StateManager(currentGameState + 1);
        else
            StateManager(State.InMenu);
    }

    void StateManager(State st)
    {
        switch (st)
        {
            case State.InMenu:
                currentGameState = State.InMenu;
                SceneManager.LoadScene("Menu");
                Debug.Log("Menu");
                StartCoroutine(ProcessMenu());
            break;
            case State.Intro:
                currentGameState = State.Intro;
                SceneManager.LoadScene("Intro");
            break;
            case State.InGame:
                currentGameState = State.InGame;
                SceneManager.LoadScene("Game");
                SceneManager.sceneLoaded += GameSceneLoaderHandler;
            break;
            case State.Ending:
                currentGameState = State.Ending;
                var scn = SceneManager.GetActiveScene();
                SceneManager.sceneLoaded -= GameSceneLoaderHandler;
                SceneManager.LoadSceneAsync("Outro");
            break;
            default:
                throw new System.Exception("Unhandled state " + st);
        }
    }

    void InitGame()
    {
        Debug.Log("Init game");
        dm = Instantiate(dm_prefab, Vector3.zero, Quaternion.identity);
        Debug.Log(dm.transform.position);
        dm.GetComponent<DreamMan>().NewZone += ZoneTranslation;
        fadeFx = Instantiate(fadefx_prefab);
    }

    IEnumerator ProcessMenu()
    {
        while(!Input.GetButtonDown("Fire1"))
        {
            yield return null;  
        }
        var start_game = Input.GetButtonDown("Fire1");
        Debug.Log("Start? - " + start_game);
        if(start_game)
            StateManager(State.Intro);
    }

    void ZoneTranslation(Transform pos)
    {
        Debug.Log("Trigger zone at - " + pos.position);
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
        currentGameState = State.Ending;
        StateManager(State.Ending);
    }
}
