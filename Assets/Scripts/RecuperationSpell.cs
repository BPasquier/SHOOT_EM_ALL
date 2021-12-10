using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecuperationSpell : MonoBehaviour
{
    // Start is called before the first frame update
    public Image animateImageObj;
    [SerializeField] private Player Joueur;
    private float tmp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp = Joueur.timing;
        if (tmp>1)
            tmp = 1;
        animateImageObj.color = new Color(tmp, tmp, tmp, 1f);
    }
}
