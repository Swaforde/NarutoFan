using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public Sprite reticleHit;
    public Sprite defaultReticle;
    public Image reticle;
    public float range;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.tag == "ground"){
                reticle.sprite = reticleHit;
            }
        }
        else {
             reticle.sprite = defaultReticle;
        }


    }
}
