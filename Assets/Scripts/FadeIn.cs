using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
    public float Speed;

	// Use this for initialization
	void Start () {
        StartCoroutine(PerformFade());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator PerformFade()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.material;// GetComponent<Material>();

        for(float i = 0; i <= 1; i += Speed)
        {
            Debug.Log("Set transparency to " + i);
            material.color = new Color(material.color.r, material.color.g, material.color.b, i);
            yield return new WaitForSeconds(Speed);
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);

        Destroy(this);
    }
}
