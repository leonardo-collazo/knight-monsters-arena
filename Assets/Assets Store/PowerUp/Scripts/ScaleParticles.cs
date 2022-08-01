using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {
    [System.Obsolete]
    void Update()
    {
        GetComponent<ParticleSystem>().startSize = transform.lossyScale.magnitude;
	}
}