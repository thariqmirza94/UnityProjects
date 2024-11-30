using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform _chestLid; // The lid of chest to rotate(open)
    [SerializeField] private GameObject[] _itemToThrow; // Array for items to throw
    [SerializeField] private float _throwForce; // How much power for throwing

    private bool _isOpen = false; // Boolean for checking if chest is opened

    public void OpenChest()
    {
        if (_isOpen) return; // Prevent re-opening

        // Rotate the chest lid by 90 degrees on the z-axis
        _chestLid.localRotation = Quaternion.Euler(_chestLid.localRotation.x, _chestLid.localRotation.y, -90);
        _isOpen = true; // chest is opened

        // Start coroutine to delay the item instantiation
        StartCoroutine(ThrowItemAfterDelay());
    }

    private IEnumerator ThrowItemAfterDelay()
    {
        // 0.5 sec wait
        yield return new WaitForSeconds(0.5f);

        // Instantiate random item from array and throw it
        if (_itemToThrow != null)
        {
            GameObject thrownItem = Instantiate(_itemToThrow[Random.Range(0, _itemToThrow.Length)], _chestLid.position + new Vector3(0, 3, 0), Quaternion.identity);

            Rigidbody rb = thrownItem.GetComponent<Rigidbody>();

            //Throw item to right direction
            rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);
        }
    }
}
