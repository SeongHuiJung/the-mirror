using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SineNoiseGenerator : MonoBehaviour
{
    [SerializeField]
    float amplitude;
    [SerializeField]
    float noiseAmplitude;
    [SerializeField]
    float time;
    [SerializeField]
    float offset;
    Light2D light;
    float output;
    float t = 0;//���ݱ��� ���� �ð�
    float noise = 0; //����
    float lastUpdatedTime = 0; //�ֱ� noise �� ���� �ð�
    bool lightOn = false; //������ ����/����
    bool toggleable = true; //�ߺ� �Է� ����
    private void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleLight();
        }
        if (t / time - lastUpdatedTime >= Mathf.PI * 2)
        {
            noise = Random.Range(-noiseAmplitude, noiseAmplitude);
            lastUpdatedTime = t / time;
        }
        output = Mathf.Sin(t / time) * noise + offset;
        light.intensity = output * System.Convert.ToInt16(lightOn);
        t += 1;
    }
    void toggleLight()
    {
        if (toggleable)
        {
            lightOn = !lightOn;
            StartCoroutine(preventDoubleInputCoroutine());
        }

    }
    IEnumerator preventDoubleInputCoroutine()
    {
        toggleable = false;
        yield return new WaitForSeconds(0.1f);
        toggleable = true;

    }
}
