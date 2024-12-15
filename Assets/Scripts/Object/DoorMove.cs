using UnityEngine;
using System.Collections;

public class DoorMove : MonoBehaviour
{

    [SerializeField] private float _moveUpDistance; // Distance to move up
    [SerializeField] private float _moveSpeed;      // Speed of movement
    private Vector3 _originalPosition; // Original position 
    private bool _isMoving = false;    // Check if the wall is moving

    private void Start()
    {
        // Save the original position of the object
        _originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger has the "Player" tag and stone wall is not moving
        if (other.CompareTag("Player") && !_isMoving)
        {
            StartCoroutine(MoveUp());
        }
    }
    private IEnumerator MoveUp()
    {
        _isMoving = true;
        Vector3 targetPosition = _originalPosition + Vector3.up * _moveUpDistance;
        float distanceCovered = 0f;

        // This loop continues as long as distanceCovered is less than 1f, meaning the object has not yet reached the target position.
        // Using 1f as the stopping condition makes this loop act like a percentage
        while (distanceCovered < 1f) // Ensures the movement runs until the end
        {
            distanceCovered += _moveSpeed * Time.deltaTime; // Distance = Speed * Time  
            transform.position = Vector3.Lerp(_originalPosition, targetPosition, distanceCovered); // Move stone door
            yield return null; // Pauses the coroutine until the next frame
        }

        // Ensure the position is set to the exact target position at the end
        transform.position = targetPosition;
        _isMoving = false;
    }
}