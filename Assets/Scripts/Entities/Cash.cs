using UnityEngine;

public class Cash : MonoBehaviour, IInteractable
{
    public void OnInteracted()
    {
        GameManager.Instance.UpdatePlayerState(PlayerState.CashCollected);
    }
}
