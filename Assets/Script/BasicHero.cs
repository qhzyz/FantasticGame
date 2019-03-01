using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicHero : MonoBehaviour {
    public int health;
    public int proActPoint;
    public int posX, posY, pos;
    // Use this for initialization
    public Material outlineMat;//描边材质
    public Material defaultMat;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void MoveTo(Vector3 pos) {

    }
    public virtual void UseSkill() {

    }
    public void SetOutline(bool ifset) {
        if (ifset) {
            transform.Find("Cube").GetComponent<MeshRenderer>().material = outlineMat;
        }
        else {
            transform.Find("Cube").GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
}
