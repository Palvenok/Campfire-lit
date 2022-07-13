using System.Collections;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private Light fireLight;
    [SerializeField] private AnimationCurve intensityCurve;

    private float _cachedIntensity;
    private float _timer = 0;
    private Vector3 _lightPos;

    private void Awake()
    {
        if (fireLight == null) fireLight = GetComponentInChildren<Light>();
        _cachedIntensity = fireLight.intensity;
        _lightPos = fireLight.transform.localPosition;
    }

    private void Update()
    {
        if (_timer > 1) _timer = 0;
        _timer += Time.deltaTime;

        float curveValue = intensityCurve.Evaluate(_timer);

        fireLight.intensity = _cachedIntensity * curveValue;
        fireLight.transform.localPosition = new Vector3(_lightPos.x, _lightPos.y, _lightPos.z) * curveValue * .7f;
    }
}
