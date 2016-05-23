using UnityEngine;
using System.Collections;

public class GhostBehavior : MonoBehaviour {
    public float FadeInSpeed = 0.1f;
    private Color _emissionColor;

    void Start () {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.material;

        _emissionColor = material.GetColor("_EmissionColor");

        SetColliderState(false);
        StartCoroutine(Spawn());
	}
	
	void Update () {
	
	}

    private IEnumerator Spawn()
    {
        PlaySpookySound();
        yield return FadeIn();
        yield return new WaitForSeconds(10f);
        yield return FadeOut();

        yield return new WaitForSeconds(Random.Range(5f, 30f));
        StartCoroutine(Spawn());
    }

    private IEnumerator FadeIn()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.material;

        material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));

        for (float i = 0; i <= 1; i += FadeInSpeed)
        {
            Debug.Log("Set transparency to " + i);
            material.color = new Color(material.color.r, material.color.g, material.color.b, i);
            material.SetColor("_EmissionColor", Color.Lerp(new Color(0f, 0f, 0f), _emissionColor, i));
            yield return new WaitForSeconds(FadeInSpeed);
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
        material.SetColor("_EmissionColor", _emissionColor);

        SetColliderState(true);
    }

    private IEnumerator FadeOut()
    {
        SetColliderState(false);

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.material;

        for (float i = 1; i >= 0; i -= FadeInSpeed)
        {
            Debug.Log("Set transparency to " + i);
            material.color = new Color(material.color.r, material.color.g, material.color.b, i);

            var lerpValue = 1f - i;
            material.SetColor("_EmissionColor", Color.Lerp(_emissionColor, new Color(0f, 0f, 0f), lerpValue));
            yield return new WaitForSeconds(FadeInSpeed);
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, 0f);
        material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
    }

    private void PlaySpookySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void SetColliderState(bool enabled)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = enabled;
    }
}
