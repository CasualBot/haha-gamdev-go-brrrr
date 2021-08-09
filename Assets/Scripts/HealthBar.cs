using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        healthCanvas = gameObject.GetComponent<Canvas>();
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if(mainCamera != null)
        {
            playerCamera = mainCamera.GetComponent<Camera>();
            healthCanvas.worldCamera = playerCamera;
            healthCanvas.renderMode = RenderMode.WorldSpace;
        }
    }
}
