using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PlanetsInSpace.Map.Space;
public class Star
{
    public int StarID { get; }
    public string starName { get; protected set; }
    public int NumberOfPlanets { get; protected set; }

    public bool StarOwned = false;

    public List<Planet> PlanetList;

    public Vector3 StarPosition { get; set; }
    public Node3D StarNode { get; set; }

    public Star(int id, string name, int planets)
    {
        StarID = id;
        starName = name;
        NumberOfPlanets = planets;

        PlanetList = new List<Planet>();

        StarPosition = new Vector3();
    }

    public void OnInputEvent(Node camera, InputEvent inputEvent, Vector3 eventPosition, Vector3 normal, long shapeIdx, Action<Vector2, Node3D> moveSelectionIcon)
    {
        if (inputEvent is InputEventMouseButton && inputEvent.IsActionReleased("MouseLeftClick"))
        {
            Debug.WriteLine("Clicking on star " + starName + " with " + NumberOfPlanets + " planets; eventPosition: " + eventPosition.X + ", " + eventPosition.Y + ", " + eventPosition.Z);

            moveSelectionIcon(((InputEventMouseButton)inputEvent).Position, StarNode);
        }

    }
}
