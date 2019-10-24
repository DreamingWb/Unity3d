using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public PropFactory patrol_factory;
    public ScoreRecorder recorder;
    public PatrolActionManager action_manager;
    public int wall_sign = 4;
    public GameObject player;
    public float player_speed = 5; 
    private List<GameObject> patrols;
    private bool game_over = false;
	public int GetScore(){return recorder.GetScore();}
	public bool GetGameover(){return game_over;}
	public void Restart(){SceneManager.LoadScene(0);}
	void OnEnable(){GameEventManager.ScoreChange += AddScore;GameEventManager.GameoverChange += Gameover;}
	void OnDisable(){GameEventManager.ScoreChange -= AddScore;GameEventManager.GameoverChange -= Gameover;}
	void AddScore(){recorder.AddScore();}
	void Gameover(){game_over = true;action_manager.DestroyAllAction();}
    void Update()
    {
        for (int i = 0; i < patrols.Count; i++)patrols[i].gameObject.GetComponent<PatrolData>().wall_sign = wall_sign;
		for (int i = 0; i < patrols.Count; i++)
			if (patrols [i].gameObject.GetComponent<PatrolData> ().sign == patrols [i].gameObject.GetComponent<PatrolData> ().wall_sign) {
				patrols [i].gameObject.GetComponent<PatrolData> ().follow_player = true;
				patrols [i].gameObject.GetComponent<PatrolData> ().player = player;
			} else {
				patrols [i].gameObject.GetComponent<PatrolData>().follow_player = false;
				patrols [i].gameObject.GetComponent<PatrolData>().player = null;
			}
		
    }
    void Start()
    {
		wall_sign = 4;
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        patrol_factory = Singleton<PropFactory>.Instance;
        action_manager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;
        LoadResources();
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    public void LoadResources()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        patrols = patrol_factory.GetPatrols();
        for (int i = 0; i < patrols.Count; i++) action_manager.GoPatrol(patrols[i]);
    }
    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
			player.transform.Translate(translationX * player_speed * Time.deltaTime, 0, translationZ * player_speed * Time.deltaTime);
			//if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0) player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
			//if (player.transform.position.y != 0) player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        }
    }
}
