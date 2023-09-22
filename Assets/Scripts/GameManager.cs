using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomerState customerState;
    public PlayerState playerState;
    public PlayerController playerController;
    
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerState(PlayerState playerState)
    {
       playerController.ChangeState(this.playerState);
    }
    
}
