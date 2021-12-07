using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public short HP;
    [SerializeField] private short HP_Max;

    void Start()
    {
        HP = HP_Max;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
