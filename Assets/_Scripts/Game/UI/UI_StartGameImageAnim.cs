using UnityEngine;

public class UI_StartGameImageAnim : MonoBehaviour
{
    private float _rotationSpeed = 0f;
    private float _timeAccumulator = 0f;

    [SerializeField] private float _maxRotationSpeed = 1000f;
    [SerializeField] private float _accelerationDuration = 2f; 
    [SerializeField] private AnimationCurve _accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Update()
    {        
        _timeAccumulator += Time.deltaTime;
               
        float normalizedTime = Mathf.Clamp01(_timeAccumulator / _accelerationDuration);
       
        float curveValue = _accelerationCurve.Evaluate(normalizedTime);
        
        _rotationSpeed = curveValue * _maxRotationSpeed;
      
        transform.Rotate(new Vector3(0, 0, -_rotationSpeed) * Time.deltaTime);
    }
}