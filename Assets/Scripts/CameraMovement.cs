using UnityEngine;
using System.Collections.Generic;
using TMPro;


[System.Serializable]
public class PlanetInfo
{
    public string Name;
    public float DistanceToSun;

    public PlanetInfo(string name, float distanceToSun)
    {
        Name = name;
        DistanceToSun = distanceToSun;
    }
}



public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    [SerializeField] private float minDistance = 1.5f; // Distance from spheres to restrict movement

    [SerializeField] private GameObject UIInfoPanel;
    private Dictionary<string, PlanetInfo> planetData;

    void Awake()
    {
        planetData = new Dictionary<string, PlanetInfo>()
        {
            { "Mercury", new PlanetInfo("Mercury", 57.9f) },
            { "Venus",   new PlanetInfo("Venus", 108.2f) },
            { "Earth",   new PlanetInfo("Earth", 149.6f) },
            { "Mars",    new PlanetInfo("Mars", 227.9f) },
            { "Jupiter", new PlanetInfo("Jupiter", 778.3f) },
            { "Saturn",  new PlanetInfo("Saturn", 1427.0f) },
            { "Uranus",  new PlanetInfo("Uranus", 2871.0f) },
            { "Neptune", new PlanetInfo("Neptune", 4497.1f) }
        };
    }
    void Update()
    {
        // Get input from WASD or Arrow Keys
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        // If there is movement input
        if (movementDirection != Vector3.zero)
        {
            // Check if movement is allowed (not blocked by a sphere)
            if (CanMove(movementDirection))
            {
                transform.position += movementDirection * moveSpeed * Time.deltaTime;
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        RaycastHit hit;
        float checkDistance = minDistance + 0.1f; 

        if (Physics.Raycast(transform.position, direction, out hit, checkDistance))
        {
            if (hit.collider.CompareTag("Sphere")) 
            {
                return false; 
            }
        }

        return true; 
    }

    void OnTriggerEnter(Collider other){
        print("Collided");
        EnableUI(other.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger: " + other.name);
        UIInfoPanel.SetActive(false);
    }


    void EnableUI(string name)
    {
        if(name == "Sun")
        {
            TextMeshProUGUI tmpText = UIInfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            tmpText.text = "You are near: " + name;
            TextMeshProUGUI tmpText_distance = UIInfoPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            tmpText_distance.gameObject.SetActive(false);
        }
        else
        {
            if (planetData.ContainsKey(name))
            {
                PlanetInfo planet = planetData[name];
            
                TextMeshProUGUI tmpText = UIInfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                tmpText.text = "You are near: " + planet.Name;
                TextMeshProUGUI tmpText_distance = UIInfoPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                tmpText_distance.gameObject.SetActive(true);
                tmpText_distance.text = "Distance to Sun: " + planet.DistanceToSun + " million kms";
            }
        }
        UIInfoPanel.SetActive(true);
    }
}
