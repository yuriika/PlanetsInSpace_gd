using System.ComponentModel.Design;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Godot.GD;

using diag = System.Diagnostics;

namespace PlanetsInSpace.Map.Space;
public partial class Galaxy : Node3D
{
    // Die SceneNamen strings sind die exportierten var's in SceneManager.gd, 
    // dort sind die Scenen dazu hinterlegt
    [Export] public string starSceneName = "star_scene";
    [Export] public string selectedStarIconSceneName = "selectedStar_scene";
    [Export] public int NumberOfStars { get; set; } = 299;
    [Export] public int MaximumRadius { get; set; } = 100;
    [Export] public int MinimumRadius { get; set; } = 0;
    [Export] public float MinDistBetweenStars { get; set; } = 1f;
    [Export] public uint SeedNumber = 100;
    [Export(PropertyHint.Range, "0,100")] public int NumberOfArms = 2;
    [Export] public int PercentageStarsCentre = 25;
    [Export] public string[] AvailablePlanetTypes = { "Barren", "Terran", "Gas Giant" };
    public Dictionary<Star, Node3D> StarToObjectMap { get; protected set; }
    [Export] public bool GalaxyView { get; set; }
    [Export] public Sprite3D LastSelectedSelectionIcon = null;
    [Export] public Label StarNames;
    List<string> _availableStarNames;
    [Export] public Material StarOwnedMaterial;
    RandomNumberGenerator Random;
    PackedScene planetScene;
    PackedScene selectedStarNodeScene;

    float _percent;
    float _starsInCentre;
    int _starsInCentreRounded;

    float _starsPerArm;
    int _starsPerArmRounded;
    int _difference;
    int _starCount = 0;
    int _starOwned;


    //PackedScene planet_scene = (PackedScene)GD.Load("res://assets/Star.tscn");


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Debug.WriteLine("Test");
        Debug.Print("TestPrint");
        Debug.Write("Test...");
        Debug.Flush();
        GD.Print("Galaxy start..");

        planetScene = GDUtils.GetScene(starSceneName);
        selectedStarNodeScene = GDUtils.GetScene(selectedStarIconSceneName);

        //var planet = PlanetScene.Instantiate();
        //AddChild(planet);

        diag.Stopwatch watch = new diag.Stopwatch();
        watch.Start();
        SanityChecks();
        watch.Stop();
        Print("Time spent in SanityChecks(): " + watch.Elapsed);

        watch.Start();
        //CreateSelectionIcon();
        watch.Stop();
        Print("Time spent in CreateSelectionIcon(): " + watch.Elapsed);

        watch.Start();
        CreateGalaxy();
        //CreateSpiralGalaxy();
        watch.Stop();
        Print("Time spent in CreateGalaxy(): " + watch.Elapsed);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    // Checks if the values are ok before create the universe
    private void SanityChecks()
    {
        if (MinimumRadius > MaximumRadius)
        {
            int tempValue = MaximumRadius;
            MaximumRadius = MinimumRadius;
            MinimumRadius = tempValue;
        }
    }

    public void CreateGalaxy()
    {
        Print("Entering CreateGalaxy()...");
        InitializeGalaxy();

        int failCount = 0;

        for (int i = 0; i < NumberOfStars; i++)
        {
            //Print("Entering CreateGalaxy() for (" + i + ")...");
            Star starData = CreateStarData(_starCount);
            //Print("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");

            Vector3 cartPosition = Utils.RandomPosition(MinimumRadius, MaximumRadius);
            starData.StarPosition = cartPosition;

            //Collider[] positionCollider = Physics.OverlapSphere(cartPosition, MinDistBetweenStars);

            PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
            var queryResult = spaceState.IntersectShape(new PhysicsShapeQueryParameters3D()
            {
                Transform = new Transform3D(Basis.Identity, cartPosition),
                Shape = new SphereShape3D() { Radius = MinDistBetweenStars }
            });

            if (queryResult.Count == 0)
            {
                //GD.Print("No Interesect");
                CreateStarObject(starData, cartPosition);
                failCount = 0;
            }
            else
            {
                GD.Print("Interesect Fail");
                i--;
                failCount++;
            }
            if (failCount > NumberOfStars)
            {
                PrintErr("failCount in Schleife für Sternen Generierung überschritten!");
                break;
            }
        }
    }

    void CreateStarObject(Star starData, Vector3 cartPosition)
    {
        //Print("Entering CreateStarObject()...");
        Node3D starGO = Utils.CreateSphereObject(starData.StarID + starData.starName, cartPosition, planetScene, this);
        starData.StarNode = starGO;
        //CreatePlanetData(starData);

        // InputEvent wird festgelegt auf starData.OnInputEvent, 
        // und zu den normalen Prametern wird zusätzlich die Methode MoveSelectionIcon mit übergeben, 
        //die wiederum aus dem Event heraus aufgerufen werden kann, um das SelectionIcon zu bewegen
        starGO.GetNode<StaticBody3D>("MeshInstance3D/StaticBody3D").InputEvent +=
            (camera, inputEvent, eventPosition, normal, shapeIdx) =>
                starData.OnInputEvent(camera, inputEvent, eventPosition, normal, shapeIdx,
                                        (Vector2 vector, Node3D node) => MoveSelectionIcon(vector, node));

        SetStarMaterial(starGO, starData);
        StarToObjectMap.Add(starData, starGO);
    }

    void SetStarMaterial(Node3D starGO, Star starData)
    {
        //Print("Entering SetStarMaterial()...");
        if (starData.StarOwned)
        {
            foreach (Node child in starGO.GetChildren())
            {
                if (child is MeshInstance3D)
                {
                    ((MeshInstance3D)child).MaterialOverride = StarOwnedMaterial;
                    break;
                }
            }
            //starGO.GetComponent<MeshRenderer>().material = StarOwnedMaterial;
        }
    }

    void InitializeGalaxy()
    {
        Print("Entering InitializeGalaxy()...");
        StarToObjectMap = new Dictionary<Star, Node3D>();

        Random = new RandomNumberGenerator();
        Random.Seed = SeedNumber;
        Utils.Seed = SeedNumber;
        Utils.Random = Random;

        GalaxyView = true;
        _starCount = 0;
        //_availableStarNames = TextAssetManager.TextToList(StarNames);
    }


    Star CreateStarData(int starCount)
    {
        // TODO
        //Print("Entering CreateStarData()...");
        string name;
        int randomIndex;
        //if (_availableStarNames != null)
        //{
        //if (_availableStarNames.Count > 0)
        //{
        //randomIndex = Random.RandiRange(0, _availableStarNames.Count - 1);
        //name = _availableStarNames[randomIndex];
        //_availableStarNames.RemoveAt(randomIndex);
        //}
        //else
        //{
        //name = "Star " + starCount;
        //}
        //}

        Star starData = new Star(starCount, Name, Random.RandiRange(1, 10));
        CreatePlanetData(starData);

        return starData;
    }

    void CreatePlanetData(Star star)
    {
        //Print("Entering CreatePlanetData()...");
        for (int i = 0; i < star.NumberOfPlanets; i++)
        {
            string name = star.starName + (star.PlanetList.Count + 1).ToString();

            int random = Random.RandiRange(1, 100);
            string type = "";

            if (random < 40)
                type = AvailablePlanetTypes[0];
            else if (40 <= random && random < 50)
                type = AvailablePlanetTypes[1];
            else
                type = AvailablePlanetTypes[2];

            Planet planetData = new Planet(name, type);
            //Print(planetData.PlanetName + " " + planetData.PlanetType);

            star.PlanetList.Add(planetData);
        }
    }

    // void CreateSelectionIcon()
    // {
    //     SelectionIcon = selectedStarNodeScene.Instantiate<Control>();
    //     SelectionIcon.Scale = SelectionIcon.Scale * 2.5f;
    //     SelectionIcon.Visible = false;
    //     this.AddChild(SelectionIcon);
    // }


    public void MoveSelectionIcon(Vector2 targetPosition, Node3D node)
    {
        //Debug.WriteLine("Setze Icon on " + targetPosition);
        if (LastSelectedSelectionIcon != null)
            LastSelectedSelectionIcon.Visible = false;
        LastSelectedSelectionIcon = ((Sprite3D)node.GetNode("Selectionbox"));
        LastSelectedSelectionIcon.Visible = true;
    }
}

