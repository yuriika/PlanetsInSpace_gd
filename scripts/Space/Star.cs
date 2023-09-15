using Godot;
using System.Collections.Generic;

namespace PlanetsInSpace.Map.Space
{
    public class Star
    {
        public int StarID { get; }
        public string starName { get; protected set; }
        public int NumberOfPlanets { get; protected set; }

        public bool StarOwned = false;

        public List<Planet> PlanetList;

        public Vector3 StarPosition { get; set; }

        public Star(int id, string name, int planets)
        {
            StarID = id;
            starName = name;
            NumberOfPlanets = planets;

            PlanetList = new List<Planet>();

            StarPosition = new Vector3();
        }
    }
}
