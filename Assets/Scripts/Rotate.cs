using UnityEngine;

public class Rotate : MonoBehaviour
{    
    [SerializeField] private Transform target;
    [SerializeField] private int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate Around
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);

    }
}
