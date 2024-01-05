using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
public class PlayerController_Horizontal : PlayerController
{
    public GameObject corridors;
    private int _currentCorridor = 2;
    private int _lastCorridor = -1;

    // private float _targetPosition = 0f;
    private float _detltaTime = 1f;
    [SerializeField] private float _seuilTime = 0.5f;

	private float? _lastAxisValue;
    // Move
	// private bool _moving;
	// private bool _movingRight;
	// private bool _movingLeft;

    public override void HandleDirection()
    {
        // Debug.Log("Traget: "+ _targetPosition.ToString());
        _frameVelocity.x = 0; // Pour etre sûr
        if (_detltaTime >= _seuilTime){
            // Debug.Log("Key: "+ FrameInput.x.ToString());
            Move(FrameInput.x);
            _detltaTime = 0;
        }      
        _detltaTime += Time.deltaTime;
    }

    private void Move(float axis){
        // _moving = false;
		// _movingLeft = false;
		// _movingRight = false;
		if (axis == 0f) _lastAxisValue = null; // On ne bouge pas
        else if (axis != _lastAxisValue)
		{
            // if (axis < 0f) _movingLeft = true;
            // if (axis > 0f) _movingRight = true;

            _currentCorridor += (int)FrameInput.x;
            _currentCorridor = ValidCorridor(_currentCorridor); 
            if (_lastCorridor != _currentCorridor) {} // Animation

            _lastCorridor = _currentCorridor;
            
            // TP to axe
            Vector3 position = base.transform.position;
            position[0] = corridors.transform.GetChild(_currentCorridor).position.x;
            base.transform.position = position;
        }
    }

    private int ValidCorridor(int corridor){
        if (corridor > 4) corridor = 4;
        if (corridor < 0) corridor = 0;
        return corridor;
    }
}
}
