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
        public float ZoomFactor { get; set; } = 0.05f;

        [Export]
        public float ZoomDuration { get; set; } = 0.2f;

        public bool InvverseZoom { get; set; } = false;

        // Für die Ausrichtung des SelectionIcons
        public static Quaternion currentAngle;

        float zoomLevel = 0;
        Node3D panObject;
        Node3D rotationObject;
        Node3D zoomObject;
        //Tween tween;

        // Used before _ready()
        public override void _Ready()
        {
            Print("CameraController_Map start..");
            Print(this.Name);

            panObject = this;
            rotationObject = this.GetChild<Node3D>(0);
            zoomObject = this.GetChild<Node3D>(0).GetChild<Node3D>(0);
            //var initiate = Instance;


            Print("PanObject: " + panObject.Name);
            Print("RotationObject: " + rotationObject.Name);
            Print("ZoomObject: " + zoomObject.Name);

            ResetCamera();
        }

        public void ResetCamera()
        {
            this.Position = Vector3.Zero;
            zoomLevel = 0;
            rotationObject.RotationDegrees = new Vector3(ZoomedInAngle, 0, 0);
            currentAngle = rotationObject.Quaternion;
            zoomObject.Position = new Vector3(0, 0, -MinZoom);
            GD.Print(MinZoom + " " + -MinZoom);
        }

        // _process is called once per frame
        public override void _Process(double delta)
        {
            ChangeZoom();
            ChangePosition();
            //Print(this.Position.ToString());
            //this.Position = Vector3.Zero;
        }

        void ChangePosition()
        {

            float horizontalInput = 0;
            float verticalInput = 0;

            horizontalInput = Input.GetActionStrength("Left") - Input.GetActionStrength("Right");
            verticalInput = Input.GetActionStrength("Up") - Input.GetActionStrength("Down");

            //Print("horizontalInput: " + horizontalInput);
            //Print("verticalInput: " + verticalInput);

            float movementFactor = Mathf.Lerp(MinZoom, MaxZoom, zoomLevel);
            float distance = (float)(PanSpeed * GetProcessDeltaTime());

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).Normalized();

            float dampingFactor = Mathf.Max(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
            //Print("Direction: " + direction);
            panObject.Position += distance * dampingFactor * movementFactor * direction;

            //ClampCameraPan();
        }

        //void ClampCameraPan()
        //{
        //    Vector3 position = panObject.Position;

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

        void ChangeZoom()
        {

            if (Input.IsActionPressed("Left"))
            {
                Print("Left");
            }
            if (Input.IsActionJustReleased("ScrollUp"))
            {
                zoomLevel = Mathf.Clamp(zoomLevel - ZoomFactor, 0, 1);
                Print("Up");
            }
            if (Input.IsActionJustPressed("ScrollDown"))
            {
                zoomLevel = Mathf.Clamp(zoomLevel + ZoomFactor, 0, 1);
                Print("Down");
            }

            float zoom = Mathf.Lerp(-MinZoom, -MaxZoom, zoomLevel);

            //Print("ZoomLevel: " + zoomLevel);
            //Print("Zoom: " + zoom);

            var tween = zoomObject.CreateTween();
            if (tween.IsRunning())
            {
                tween.TweenProperty(zoomObject, "position", new Vector3(0, 0, zoom), ZoomDuration);
            }

            // Wechselt smooth zwischen Draufsicht und angewinkelter Sicht
            float zoomAngle = Mathf.Lerp(ZoomedInAngle, ZoomedOutAngle, zoomLevel);
            //Print("ZoomAngle: " + zoomAngle);
            rotationObject.RotationDegrees = new Vector3(zoomAngle, 0, 0);

        }

        //public void MoveTo(Vector3 position)
        //{
        //    this.Transform.position = position;
        //}
    }
}
