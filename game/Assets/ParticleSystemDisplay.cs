using UnityEngine;
using System.Collections;

public class ParticleSystemDisplay : MonoBehaviour {

    public ParticleSystem particleSystem;
    void Start()
    {
        particleSystem.renderer.sortingLayerName = "particles";
    }
}
