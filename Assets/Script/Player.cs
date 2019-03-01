using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public BasicHero[] hero;
    public int heroNum;
    public bool inTurn;
    public Transform specifiedHero;
    bool specified = false;

    RaycastHit hit;
    RaycastHit tempHit;
    // Use this for initialization
    public void Start () {
    }
    public void InitPlayer() {
        heroNum = 2;
        hero = new BasicHero[heroNum];
        GameObject temp = (GameObject)Resources.Load("testHero");
        for (int i = 0; i < heroNum; i++) {
            hero[i] = Instantiate(temp, transform).GetComponent<BasicHero>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (inTurn)
            InTurnUpdate();
        else
            OthersTurnUpdate();
    }
    void InTurnUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (Physics.Raycast(ray, out tempHit, int.MaxValue) && Input.GetMouseButtonDown(0)) {
            hit = tempHit;
        }
        else if (Physics.Raycast(ray, out tempHit, int.MaxValue) && Input.GetMouseButtonUp(0) && tempHit.Equals(hit)) {
            if (hit.transform.tag == "Hero") {
                specifiedHero = hit.transform;
                specified = true;
                if (specified) specifiedHero.GetComponent<BasicHero>().SetOutline(true);
            }
            else if (hit.transform.tag == "MapUnit" && specified) {
                specifiedHero.GetComponent<BasicHero>().MoveTo(hit.transform.position);
                if (specified) specifiedHero.GetComponent<BasicHero>().SetOutline(false);
                specified = false;
                specifiedHero = null;
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(1)) {
            if(specified)specifiedHero.GetComponent<BasicHero>().SetOutline(false);
            specified = false;
            specifiedHero = null;
        }
    }
    void OthersTurnUpdate() {

    }
}
