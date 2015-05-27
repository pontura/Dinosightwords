using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    [SerializeField]
    Character characterHero;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos =  transform.localPosition;
        pos.x = characterHero.transform.localPosition.x;
        transform.localPosition = pos;
	}
}
