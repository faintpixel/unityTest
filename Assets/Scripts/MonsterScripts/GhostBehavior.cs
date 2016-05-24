using UnityEngine;
using System.Collections;

public class GhostBehavior : MonoBehaviour
{
    public float FadeInSpeed = 0.1f;

    private Color _emissionColor;
    private Material _material;


    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        _material = renderer.material;
        _emissionColor = _material.GetColor("_EmissionColor");

        SetColliderState(false);
        StartCoroutine(Spawn());
    }

    void Update()
    {
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
        _material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));

        for (float i = 0; i <= 1; i += FadeInSpeed)
        {
            Debug.Log("Set transparency to " + i);
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, i);
            _material.SetColor("_EmissionColor", Color.Lerp(new Color(0f, 0f, 0f), _emissionColor, i));
            yield return new WaitForSeconds(FadeInSpeed);
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1f);
        _material.SetColor("_EmissionColor", _emissionColor);

        SetColliderState(true);
    }

    private IEnumerator FadeOut()
    {
        SetColliderState(false);

        for (float i = 1; i >= 0; i -= FadeInSpeed)
        {
            Debug.Log("Set transparency to " + i);
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, i);

            var lerpValue = 1f - i;
            _material.SetColor("_EmissionColor", Color.Lerp(_emissionColor, new Color(0f, 0f, 0f), lerpValue));
            yield return new WaitForSeconds(FadeInSpeed);
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 0f);
        _material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
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
