using UnityEngine;

public class ClothRack : MonoBehaviour, IInteractable
{
    
    public void OnInteracted()
    {
        Debug.Log("Cloth Rack Hit");
    }
}
