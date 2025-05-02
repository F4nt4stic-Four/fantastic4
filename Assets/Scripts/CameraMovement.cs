using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class PlanetInfo
{
    public string Name;
    public float DistanceToSun;
    public float GravityFactor;
    public float GravityMS2;            // Gravity in m/s²
    public float RelativeWeight;        // Weight on the planet if Earth weight = 100g
    public float TimesEarthGravity; 
    
    
    // Gravity ratio compared to Earth

    public PlanetInfo(string name, float distanceToSun, float gravityFactor,
                      float gravityMS2, float relativeWeight, float timesEarthGravity)
    {
        Name = name;
        DistanceToSun = distanceToSun;
        GravityFactor = gravityFactor;
        GravityMS2 = gravityMS2;
        RelativeWeight = relativeWeight;
        TimesEarthGravity = timesEarthGravity;
    }
}

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float originalSpeed;
    [SerializeField] private float reducedSpeed = 1f;
    [SerializeField] private float speedReductionRate = 2f;

    [SerializeField] private float minDistance = 1.5f;

    [SerializeField] private GameObject UIInfoPanel;
    [SerializeField] private Image flickerPanelImage; 
    private Dictionary<string, PlanetInfo> planetData;


    [SerializeField] private GameObject saturnUI;


    [SerializeField] private TextMeshProUGUI playerSpeed;
    [SerializeField] private TextMeshProUGUI gravitationalObject;

    private bool isInGravityField = false;
    private Vector3 lastPosition;

    void Awake()
    {
        originalSpeed = moveSpeed;

        planetData = new Dictionary<string, PlanetInfo>()
        {
            { "Mercury", new PlanetInfo("Mercury", 57.9f, 0.2f, 3.7f, 38f, 0.38f) },
            { "Venus",   new PlanetInfo("Venus", 108.2f, 0.3f, 8.87f, 90f, 0.90f) },
            { "Earth",   new PlanetInfo("Earth", 149.6f, 0.0f, 9.81f, 100f, 1.00f) },
            { "Moon",    new PlanetInfo("Moon", 0f, 0.1f, 1.62f, 17f, 0.17f) },
            { "Mars",    new PlanetInfo("Mars", 227.9f, 0.25f, 3.71f, 38f, 0.38f) },
            { "Jupiter", new PlanetInfo("Jupiter", 778.3f, 0.8f, 24.79f, 253f, 2.53f) },
            { "Saturn",  new PlanetInfo("Saturn", 1427.0f, 0.6f, 10.44f, 107f, 1.07f) },
            { "Uranus",  new PlanetInfo("Uranus", 2871.0f, 0.5f, 8.69f, 89f, 0.89f) },
            { "Neptune", new PlanetInfo("Neptune", 4497.1f, 0.65f, 11.15f, 114f, 1.14f) },
            { "Sun",     new PlanetInfo("Sun", 0f, 1.2f, 274.0f, 2795f, 27.95f) }
        };

        gravitationalObject.text = "";
        lastPosition = transform.position;
        playerSpeed.text = "";
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (movementDirection != Vector3.zero)
        {
            if (CanMove(movementDirection))
            {
                if (isInGravityField && moveSpeed > reducedSpeed)
                {
                    moveSpeed -= speedReductionRate * Time.deltaTime;
                    moveSpeed = Mathf.Max(moveSpeed, reducedSpeed);
                }
                
                float currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
                lastPosition = transform.position;
                playerSpeed.text = $"Current Speed: {currentSpeed:F2} units/s";
                transform.position += movementDirection * moveSpeed * Time.deltaTime;
            }
        }
    }



private bool CanMove(Vector3 direction)
{
    RaycastHit hit;
    float checkDistance = minDistance + 1f;

    if (Physics.Raycast(transform.position, direction, out hit, checkDistance) && hit.collider.CompareTag("Sphere"))
    {
        string planetName = hit.collider.name;
        float distance = hit.distance;

        float gravityFactor = 0.5f; // default fallback
        PlanetInfo planet = null;

        if (planetData.ContainsKey(planetName))
        {
            planet = planetData[planetName];
            gravityFactor = planet.GravityFactor;
        }
        else if (planetName == "Sun")
        {
            gravityFactor = 1.2f;
            planet = new PlanetInfo("Sun", 0f, 1.2f, 274.0f, 2795f, 27.95f); // create Sun info directly
        }

        if (distance <= minDistance * 3)
        {
            moveSpeed = Mathf.Max(originalSpeed * (1f - gravityFactor), reducedSpeed);
            isInGravityField = true;
        }
        else
        {
            isInGravityField = false;
        }

        if (planet != null)
        {
            gravitationalObject.text = $"Gravity: {planet.GravityMS2} m/s²\n" +
                                       $"Weight: {planet.RelativeWeight}g\n" +
                                       $"{planet.TimesEarthGravity}x Earth Gravity";
        }

        if (distance <= minDistance)
        {
            return false;
        }
    }
    else
    {
        if (!isInGravityField)
        {
            moveSpeed = originalSpeed;
        }

        gravitationalObject.text = "";
        isInGravityField = false;
    }

    return true;
}






    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.tag);
        EnableUI(other.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger: " + other.name);
        UIInfoPanel.SetActive(false);
    }

    void EnableUI(string name)
    {
        if (name == "Sun")
        {
            TextMeshProUGUI tmpText = UIInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tmpText.text = "You are near: " + name;
            TextMeshProUGUI tmpText_distance = UIInfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            tmpText_distance.gameObject.SetActive(false);
        }

        else
        {
            if (planetData.ContainsKey(name))
            {
                PlanetInfo planet = planetData[name];

                TextMeshProUGUI tmpText = UIInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tmpText.text = "You are near: " + planet.Name;
                TextMeshProUGUI tmpText_distance = UIInfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                tmpText_distance.gameObject.SetActive(true);
                tmpText_distance.text = "Distance to Sun: " + planet.DistanceToSun + " million kms";
            }
            if (name == "Saturn")
            {
                saturnUI.SetActive(true);
            }
        }
        UIInfoPanel.SetActive(true);
    }

    public void CloseSaturnPanel()
    {
        saturnUI.SetActive(false);
    }

}
