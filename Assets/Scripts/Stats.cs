using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI currentHealthText;
    [SerializeField] TextMeshProUGUI totalHealthText;

    [SerializeField] float health = 100f;
    [SerializeField] Canvas spawnedUI;

    // Start is called before the first frame update
    void Start()
    {
        if(healthCanvas != null)
        {
            var healthPrefab = healthCanvas.transform.Find("Bar");

            healthBar = healthPrefab.GetComponent<Image>();
            if(healthPrefab)
            {
                // Need to get the TMPGUI value
                var textContainer = healthCanvas.transform.Find("Health Text");
                if (textContainer)
                {
                    var currentHealth = textContainer.transform.Find("Remaining Health Text");
                    var totalHealth = textContainer.transform.Find("Total Health");

                    if(currentHealth) 
                    {
                        currentHealthText = currentHealth.GetComponent<TextMeshProUGUI>();
                        currentHealthText.text = health.ToString();
                    }
                    if(totalHealth) 
                    {
                        totalHealthText = totalHealth.GetComponent<TextMeshProUGUI>();
                        totalHealthText.text = health.ToString();
                    }
                }
            }

            spawnedUI.GetComponent<RectTransform>().localScale = new Vector3(0.005f, 0.005f, 0.005f);
            spawnedUI.transform.position = gameObject.transform.Find("UI Anchor").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnedUI.transform.LookAt(GameObject.Find("Player").transform.position, Vector3.up);
        currentHealthText.text = health.ToString();
    }
}
