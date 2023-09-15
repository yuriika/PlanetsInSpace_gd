using Godot;
using static Godot.GD;

namespace PlanetsInSpace.Map.Camera
{
    public partial class CameraController_Map : Node3D
    {
        [Export]
        public float PanSpeed { get; set; } = 2;

        [Export]
        public float ZoomedInAngle { get; set; } = 45;

        [Export]
        public float ZoomedOutAngle { get; set; } = 90;

        [Export]
        public float MinZoom { get; set; } = 20;

        [Export]
        public float MaxZoom { get; set; } = 200;

        [Export]
        public bool InvverseZoom { get; set; } = false;

        // Für die Ausrichtung des SelectionIcons
        public static Quaternion currentAngle;

        float zoomLevel = 0;
        Node3D panObject;
        Node3D rotationObject;
        Node3D zoomObject;

        // Used before _ready()
        public override void _Ready()
        {
            Print("CameraController_Map start..");
            Print(this.Name);

            rotationObject = this.GetChild<Node3D>(0);
            zoomObject = this.GetChild<Node3D>(0).GetChild<Node3D>(0);
            //var initiate = Instance;

            Print(rotationObject.Name);
            Print(zoomObject.Name);

            ResetCamera();
        }
        // _process is called once per frame
        //public override void _Process()
        //{
        //    ChangeZoom();
        //    ChangePosition();
        //}

        public void ResetCamera()
        {
            this.Position = Vector3.Zero;
            //this.Translate(Vector3.Zero);
            zoomLevel = 0;
            rotationObject.RotationDegrees = new Vector3(ZoomedInAngle, 0, 0);
            currentAngle = rotationObject.Quaternion;
            zoomObject.Position = new Vector3(0, 0, MinZoom);
            //zoomObject.Translate(new Vector3(0, 0, -MinZoom));


        }

        public override void _Process(double delta)
        {
            Print(this.Position.ToString());
            //this.Position = Vector3.Zero;
        }

        //void ChangePosition()
        //{
        //    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //    {
        //        float movementFactor = Mathf.Lerp(MinZoom, MaxZoom, zoomLevel);
        //        float distance = PanSpeed * Time.deltaTime;
        //        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        //        float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

        //        Transform.Translate(distance * dampingFactor * movementFactor * direction);

        //        ClampCameraPan();
        //    }
        //}

        //void ClampCameraPan()
        //{
        //    Vector3 position = this.Transform.position;

        //    if (Galaxy.Instance.GalaxyView == true)
        //    {
        //        position.x = Mathf.Clamp(Transform.position.x, -Galaxy.Instance.MaximumRadius, Galaxy.Instance.MaximumRadius);
        //        position.z = Mathf.Clamp(Transform.position.z, -Galaxy.Instance.MaximumRadius, Galaxy.Instance.MaximumRadius);

        //    }
        //    else
        //    {
        //        position.x = Mathf.Clamp(Transform.position.x, -50, 50);
        //        position.z = Mathf.Clamp(Transform.position.z, -50, 50);
        //    }

        //    this.Transform.position = position;
        //}

        //void ChangeZoom()
        //{
        //    if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //    {
        //        if (!InvverseZoom)
        //        {
        //            zoomLevel = Mathf.Clamp01(zoomLevel - Input.GetAxis("Mouse ScrollWheel"));
        //        }
        //        else
        //        {
        //            zoomLevel = Mathf.Clamp01(zoomLevel + Input.GetAxis("Mouse ScrollWheel"));
        //        }

        //        float zoom = Mathf.Lerp(-MinZoom, -MaxZoom, zoomLevel);
        //        zoomObject.Transform.localPosition = new Vector3(0, 0, zoom);

        //        // Wechselt smooth zwischen Draufsicht und angewinkelter Sicht
        //        float zoomAngle = Mathf.Lerp(ZoomedInAngle, ZoomedOutAngle, zoomLevel);
        //        rotationObject.Transform.localRotation = Quaternion.Euler(zoomAngle, 0, 0);
        //    }
        //}

        //public void MoveTo(Vector3 position)
        //{
        //    this.Transform.position = position;
        //}
    }
}
