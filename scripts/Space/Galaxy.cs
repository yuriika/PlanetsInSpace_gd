using Godot;
using System.Collections.Generic;

namespace PlanetsInSpace.Map.Space
{
    public partial class Galaxy : Node
    {
        [Export] public PackedScene PlanetScene;
        [Export] public int NumberOfStars { get; set; } = 299;
        [Export] public int MaximumRadius { get; set; } = 100;
        [Export] public int MinimumRadius { get; set; } = 0;
        [Export] public float MinDistBetweenStars { get; set; } = 1f;

        [Export] public int SeedNumber = 100;
        [Export(PropertyHint.Range, "0,100")] public int NumberOfArms = 2;

        [Export] public int PercentageStarsCentre = 25;

        [Export] public string[] AvailablePlanetTypes = { "Barren", "Terran", "Gas Giant" };

        public Dictionary<Star, Node> StarToObjectMap { get; protected set; }

        [Export] public bool GalaxyView { get; set; }

        [Export] public Node SelectionIcon;

        [Export] public Label StarNames;

        List<string> _availableStarNames;

        public Material StarOwnedMaterial;

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
            GD.Print("Galaxy start..");
            var planet = PlanetScene.Instantiate();
            AddChild(planet);

        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
