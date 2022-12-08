using UnityEngine;

// this class has something to do with the Oculus Interaction plugin, so we use its namespace here
namespace Oculus.Interaction
{
    // this class can read the hand input from Oculus Interaction plugin, and use it to manipulate the active building, regardless of its rendering mode
    public class BuildingHandInteraction : MonoBehaviour
    {
        // the following two interactors are directly driven by Oculus Interaction, and can be found under OVRCameraRig
        public RayInteractor leftRayInteractor;
        public RayInteractor rightRayInteractor;
        // the following two cursors are used to retrive the hand position
        public Transform leftCursor;
        public Transform rightCursor;
        // this is the resistance for spinning when hand interaction is over
        public float resistance = 0.03f;
        // the building to manipulate
        public Transform building;
        // these bools are used as flags to know the hand state of the last frame
        private bool bothSelected = false;
        private bool nothingSelected = true;
        // two distance between two hands
        private float distance;

        private Vector3 buildingScaleBeforeSelection;
        private Vector3 buildingRotBeforeSelection;

        private float distanceBeforeSelection;
        private Vector3 selectorPosBeforeSelection;
        private Vector3 pointingVecBeforeSelection;

        private float lastYRot;
        private float rotationSpeed = 0;

        private void Update()
        {
            // when no building is in active, ignore any of the hand input and return
            if (building == null) return;

            // Nothing selected, if the building is spinning last frame, keep spinning it and decrease its spinning speed, until it meets threhold and keep still
            if (leftRayInteractor.State != InteractorState.Select && rightRayInteractor.State != InteractorState.Select)
            {
                // Keep rotating
                if (Mathf.Abs(rotationSpeed) > 2 * resistance)
                {
                    // Some models has initial rotation, so use world space vector here instead
                    building.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
                    rotationSpeed = (rotationSpeed > 0) ? rotationSpeed - resistance : rotationSpeed + resistance;
                }

                nothingSelected = true;
                bothSelected = false;
                return;
            }

            // Right selector enabled: rotation control, get the spinning angle from mathematical functions
            if (leftRayInteractor.State != InteractorState.Select && rightRayInteractor.State == InteractorState.Select)
            {
                Transform currentSelector = rightCursor;

                // init
                if (nothingSelected)
                {
                    buildingRotBeforeSelection = building.localEulerAngles;
                    lastYRot = buildingRotBeforeSelection.y;
                    selectorPosBeforeSelection = currentSelector.position;
                    pointingVecBeforeSelection = Vector3.Cross(Vector3.up, new Vector3(building.position.x, currentSelector.position.y, building.position.z));
                }

                // change rotation of the building
                else
                {
                    float rotMultiplier = 30.0f;
                    float changeFac = Vector3.Dot(pointingVecBeforeSelection, currentSelector.position - selectorPosBeforeSelection);
                    Vector3 rotToApply = new Vector3(buildingRotBeforeSelection.x, buildingRotBeforeSelection.y - changeFac * rotMultiplier, buildingRotBeforeSelection.z);
                    rotationSpeed = -changeFac * rotMultiplier;
                    building.localEulerAngles = rotToApply;

                    rotationSpeed = building.localEulerAngles.y - lastYRot;
                    lastYRot = building.localEulerAngles.y;
                }

                nothingSelected = false;
                bothSelected = false;
                return;
            }

            // TODO: Left selector enabled: info card interaction (not implemented yet)
            if (leftRayInteractor.State == InteractorState.Select && rightRayInteractor.State != InteractorState.Select)
            {
                rotationSpeed = 0;
                nothingSelected = false;
                bothSelected = false;
                return;
            }


            // both selected, scale the building from the distance of two hands
            distance = Vector3.Distance(leftCursor.position, rightCursor.position);

            // init
            if (bothSelected == false)
            {
                buildingScaleBeforeSelection = building.localScale;
                distanceBeforeSelection = distance;
            }

            // change scale of the building
            else
            {
                building.localScale = (distance / distanceBeforeSelection) * buildingScaleBeforeSelection;
            }

            bothSelected = true;
            return;
        }

    }

}
