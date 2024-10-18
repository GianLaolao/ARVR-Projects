using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacer : MonoBehaviour
{
    ARAnchorManager anchorManager;
    [SerializeField] private float forwardOffset = 2f;
    private GameObject prefabToAnchor;
    
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject kanaPrefab;
    [SerializeField] public GameObject aiPrefab;
    [SerializeField] public GameObject rubyPrefab;
    [SerializeField] public Slider slider;    
    [SerializeField] public GameObject parent;

    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();

        Button[] buttons = canvas.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => { OnButtonClicked(0, kanaPrefab, buttons); });
        buttons[1].onClick.AddListener(() => { OnButtonClicked(1, aiPrefab, buttons); });
        buttons[2].onClick.AddListener(() => { OnButtonClicked(2, rubyPrefab, buttons); });
    }

    void Update()
    {   
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 spawnPos = Camera.main.ScreenPointToRay(Input.GetTouch(0).position)
                .GetPoint(forwardOffset);
            AnchorObject(spawnPos);
        }
    }

    void OnButtonClicked(int buttonIndex, GameObject prefab, Button[] buttons)
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        prefabToAnchor = prefab;

        buttons[buttonIndex].interactable = false;
    }

    public void AnchorObject(Vector3 worldPos)
    {
        Collider[] colliders = Physics.OverlapSphere(worldPos, 0.1f);
        if (colliders.Length > 0)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject == colliders[0].gameObject)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else
        {
            GameObject newAnchor = new GameObject("NewAnchor");
            newAnchor.transform.parent = null;
            newAnchor.transform.position = worldPos;
            newAnchor.AddComponent<ARAnchor>();

            GameObject obj = Instantiate(prefabToAnchor, newAnchor.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.parent = parent.transform;
            obj.transform.localScale = new Vector3(slider.value, slider.value, slider.value);
        }
    }

    public void DeleteObjects()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

  
}
