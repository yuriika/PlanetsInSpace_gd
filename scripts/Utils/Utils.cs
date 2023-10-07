using Godot;

namespace PlanetsInSpace.Map;
public partial class Utils : Node
{
    public static uint Seed { get; set; } = 0;

    private static RandomNumberGenerator _random = null;
    public static RandomNumberGenerator Random
    {
        get
        {
            if (_random == null)
            {
                _random = new RandomNumberGenerator();
                if (Seed == 0)
                {
                    _random.Randomize();
                }
                else
                {
                    _random.Seed = Seed;
                }
                return _random;
            }
            else
            {
                return _random;
            }
        }
        set
        {
            if (_random == null)
                _random = value;
        }
    }

    private Utils() { }

    // This method creates a random polar coordinate then converts and returns it as a Cartesian coordinate
    public static Vector3 RandomPosition(float minRad, float maxRad)
    {
        float distance = Random.RandfRange(minRad, maxRad);
        float angle = RandomAngle();

        Vector3 cartPosition = PolarToCart(distance, angle);

        return cartPosition;
    }

    // This method returns a random angle between 0 and 2*PI
    public static float RandomAngle()
    {
        float angle = Random.RandfRange(0, 2 * Mathf.Pi);

        return angle;
    }

    // This method creates a positon for a planet based on its number in the planetList
    public static Vector3 PlanetPosition(int planetListNumber)
    {
        float distance = (planetListNumber + 1) * 5;
        float angle = RandomAngle();

        Vector3 cartPosition = PolarToCart(distance, angle);

        return cartPosition;
    }

    // This method converts a distance and angle (polar coordinates)
    public static Vector3 PolarToCart(float distance, float angle)
    {
        Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

        return cartPosition;
    }

    // This method creates a sphere object
    public static Node3D CreateSphereObject(string name, Vector3 position, PackedScene starScene, Node3D parent)
    {
        var sphere = starScene.Instantiate<Node3D>();
        sphere.Name = name;
        sphere.Position = position;

        parent?.AddChild(sphere);

        //TODO
        //CreateNamePlate(sphere);

        return sphere;
    }

    public static Node CreateOrbitPaths(PackedScene orbitSprite, string name, int orbitNumber, Node3D parent)
    {
        //Wenn nötig Singelton Instanz machen, damit Objekt vorhanden ist, dann kann auf GetTree zugegriffen werden
        //GetTree().ToString();
        var orbit = orbitSprite.Instantiate<Node3D>(); //Object.Instantiate(orbitSprite);
                                                       //orbit.Transform.ScaledLocal()
        orbit.Scale = orbit.Scale * orbitNumber;
        orbit.Name = name;
        //orbit.transform.localScale = orbit.transform.localScale * orbitNumber;

        parent?.AddChild(orbit);

        return orbit;
    }

    public static float SpiralAngle(float armAngle, float starAngle)
    {
        float angle = armAngle + starAngle;
        return angle;
    }

    //public static bool CheckCollisions(float minDistBetweenStars, Vector3 cartPosition)
    //{
    //    bool collision = false;
    //    Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

    //    if (positionCollider.Length > 0)
    //    {
    //        collision = true;
    //    }

    //    return collision;
    //}

    //public static void CreateNamePlate(Node3D go)
    //{
    //    TextMesh nameText = new GameObject(go.name + " Name Plate").AddComponent<TextMesh>();
    //    nameText.transform.SetParent(go.transform);
    //    nameText.text = go.name;
    //    nameText.transform.localPosition = new Vector3(0, -1.2f, 0);
    //    nameText.anchor = TextAnchor.MiddleCenter;
    //    nameText.alignment = TextAlignment.Center;
    //    nameText.color = Color.white;
    //    nameText.fontSize = 18;
    //    nameText.characterSize = 0.5f;

    //    UI.GUIManagementScript.GUIManagerInstance.namePlates.Add(nameText.gameObject);
    //}
}

