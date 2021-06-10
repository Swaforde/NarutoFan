using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public Sprite reticleHit;
    public Sprite defaultReticle;
    public GameObject hitPoint;
    public Image reticle;
    public float range;

    public bool hitGround;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.tag == "ground" && hit.distance <= range){
                reticle.sprite = reticleHit;
                hitGround = true;
            }
            else
            {
                reticle.sprite = defaultReticle;
                hitGround = false;
            }
        }
        hitPoint.transform.position = (hit.point);


    }
}
