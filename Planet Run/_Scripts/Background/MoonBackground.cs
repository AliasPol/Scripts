using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBackground : MonoBehaviour {

    private MeshRenderer m_Renderer;
    public float changeScale = 0.5f;

    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        m_Renderer.material = RoundMenager.Instance.GetMoonMaterial();
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;
        scale += (Time.deltaTime * Vector3.one * changeScale);
        transform.localScale = scale;
    }
}
