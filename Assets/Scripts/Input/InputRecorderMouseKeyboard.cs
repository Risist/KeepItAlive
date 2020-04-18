using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorderMouseKeyboard : InputRecorder
{
    public InputBinding binding;

    Camera cam;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        cam = Camera.main;
    }

    private void Update()
    {
        Debug.Assert(binding);
        //Debug.Assert(binding.keyAxisCode.Length >= inputHolder.keys.Length);

        


        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputHolder.rotationInput = inputHolder.directionInput = mousePosition - (Vector2)transform.position;

        /// position
        /*inputHolder.positionInput = GetStrifeInput(
            inputHolder.rotationInput.normalized, 
            new Vector2(
                Input.GetAxis(binding.positionAxisCodeX), 
                Input.GetAxis(binding.positionAxisCodeY)).normalized)
            ;*/
        inputHolder.positionInput.x = Input.GetAxis(binding.positionAxisCodeX);
        inputHolder.positionInput.y = Input.GetAxis(binding.positionAxisCodeY);

        /// keys
        int n = Mathf.Min(inputHolder.keys.Length, binding.keyAxisCode.Length);
        for (int i = 0; i < n; ++i)
        {
            inputHolder.keys[i] = Input.GetButton(binding.keyAxisCode[i]);
        }
    }

    Vector2 GetStrifeInput(Vector2 directionInput, Vector2 positionInput)
    {
        return directionInput * positionInput.y +
               new Vector2(directionInput.y, -directionInput.x) * positionInput.x;
    }
    Vector2 GetAllignedDirection(Vector2 directionInput, Vector2 positionInput)
    {
        Vector2[] directions = {
            directionInput,
            -directionInput,
            new Vector2(directionInput.y, -directionInput.x),
            new Vector2(-directionInput.y, directionInput.x)
        };


        Vector2 inputDirection = transform.up;

        float maxDot = float.NegativeInfinity;
        inputDirection = Vector2.zero;
        int currentDirectionId = 0;
        if (positionInput.sqrMagnitude < inputHolder.inputThreshold * inputHolder.inputThreshold)
            return inputDirection;

        for (int i = 0; i < directions.Length; ++i)
        {
            var newDot = Vector2.Dot(positionInput, directions[i]);
            Debug.Log(newDot);
            if (newDot > maxDot)
            {
                maxDot = newDot;
                inputDirection = directions[i];
                currentDirectionId = i;
            }
        }

        return inputDirection;
    }
}
