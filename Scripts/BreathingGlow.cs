using UnityEngine;

public class BreathingGlow : MonoBehaviour
{
    public float breathingSpeed = 1.0f;

    public float breathingPhase = 0.0f;
    private Material materialInstance;

    void Start()
    {
        materialInstance = GetComponent<MeshRenderer>().material;
        Debug.Log(materialInstance);
    }

    void Update()
    {
        breathingPhase += Time.deltaTime * breathingSpeed;
        Color emissionColor = materialInstance.GetColor("_EmissionColor");
        //Debug.Log(emissionColor);
        emissionColor.a = Mathf.Sin(breathingPhase) * 0.5f + 0.5f;
        materialInstance.SetColor("_EmissionColor", emissionColor);
    }
}