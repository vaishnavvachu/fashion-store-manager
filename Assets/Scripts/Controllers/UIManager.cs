using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject playerSpeechBubbleGO;
    [SerializeField] private GameObject customerSpeechBubbleGO;
    [SerializeField] private TextMeshProUGUI playerSpeechBubbleText;
    [SerializeField] private TextMeshProUGUI customerSpeechBubbleText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPlayerSpeechBubble(string text)
    {
        playerSpeechBubbleGO.SetActive(true);
        
        playerSpeechBubbleText.text = text;
        
        Invoke(nameof(HidePlayerSpeechBubble), 3f);
    }

    void HidePlayerSpeechBubble()
    {
        playerSpeechBubbleGO.SetActive(true);
    }
    
    public void ShowCustomerSpeechBubble(string text)
    {
        customerSpeechBubbleGO.SetActive(true);
        
        customerSpeechBubbleText.text = text;
        
        Invoke(nameof(HideCustomerSpeechBubble), 3f);
    }

    void HideCustomerSpeechBubble()
    {
        customerSpeechBubbleGO.SetActive(true);
    }

   public void ShowCashCollectedUIFx()
    {
        Debug.Log("Show Cash Collected");
    }
}