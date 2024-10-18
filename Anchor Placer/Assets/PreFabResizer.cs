using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class PreFabResizer : MonoBehaviour
{

    [SerializeField] public Slider slider;
    [SerializeField] public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = slider.value.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString("F1");
    }  
}
