using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerController playerController;
    
    private PlayerState _playerState;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerState(PlayerState playerState)
    {
       playerController.ChangeState(playerState);
    }
    
}
