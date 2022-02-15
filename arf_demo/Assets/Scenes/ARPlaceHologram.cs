using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace com.xrvar.demo
{
    public class ARPlaceHologram : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabToPlace;
        public Text LogText;

        private ARRaycastManager _raycastManager;
        private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();

        void Awake()
        {
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            // Support single touch only
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
                return;

            if (_raycastManager.Raycast(touch.position, Hits, TrackableType.AllTypes))
            {
                var hitPose = Hits[0].pose;

                Instantiate(_prefabToPlace, hitPose.position, hitPose.rotation);

                string logMessage = $"Instantiated on : {Hits[0].hitType} at {Hits[0].pose.position}";
                if (LogText != null)
                    LogText.text = logMessage;
                else
                    Debug.Log(logMessage);
            }
        }
    }
}