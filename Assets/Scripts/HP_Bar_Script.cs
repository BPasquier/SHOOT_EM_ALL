using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar_Script : MonoBehaviour
{
    [SerializeField] private Player joueur;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = joueur.HP_Max;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = joueur.HP;
    }
}
