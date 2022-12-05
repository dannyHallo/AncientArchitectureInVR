using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class BuildingHandInteraction : MonoBehaviour
    {
        public RayInteractor leftRayInteractor;
        public RayInteractor rightRayInteractor;
        public Transform leftCursor;
        public Transform rightCursor;
        public float draft = 0.03f;

        public Transform building;

        private bool bothSelected = false;
        private bool nothingSelected = true;

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
            if (building == null) return;

            // Nothing selected
            if (leftRayInteractor.State != InteractorState.Select && rightRayInteractor.State != InteractorState.Select)
            {
                // Keep rotating
                if (Mathf.Abs(rotationSpeed) > 2 * draft)
                {
                    // Some models has initial rotation, so use world space vector here instead
                    building.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
                    rotationSpeed = (rotationSpeed > 0) ? rotationSpeed - draft : rotationSpeed + draft;
                }

                nothingSelected = true;
                bothSelected = false;
                return;
            }

            // Right selector enabled: rotation control
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

            // TODO: Left selector enabled: info
            if (leftRayInteractor.State == InteractorState.Select && rightRayInteractor.State != InteractorState.Select)
            {
                rotationSpeed = 0;
                nothingSelected = false;
                bothSelected = false;
                return;
            }


            // both selected
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
