using TMPro;
using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter instance;

    private TMP_Text _counterText;
    private int _currentlyCollected = 0;
    private int _maxCollectible;

    private void Awake()
    {
        _counterText = GetComponent<TMP_Text>();
        instance = this;
    }

    void Start()
    {
        _maxCollectible = GameObject.FindGameObjectsWithTag("Collectible").Length;
        UpdateCounterDisplay();
    }

    [ContextMenu("Increment Collected")]
    public void IncrementCollected()
    {
        _currentlyCollected++;
        UpdateCounterDisplay();
    }

    private void UpdateCounterDisplay()
    {
        _counterText.text = _currentlyCollected.ToString() + "/" + _maxCollectible;
    }
}
