namespace PlanetsInSpace.Map.Space
{
    public class Planet
    {
        public string PlanetName { get; protected set; }
        public string PlanetType { get; protected set; }

        public bool PlanetColonised = false;

        public Planet(string name, string type)
        {
            PlanetName = name;
            PlanetType = type;
        }
    }
}
