using System.Text;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace com.xrvar.demo
{
    public class PointCloudInfo : MonoBehaviour
    {
        private ARPointCloud _pointCloud;

        public UnityEngine.UI.Text LogText;

        void OnEnable()
        {
            _pointCloud = GetComponent<ARPointCloud>();
            _pointCloud.updated += OnPointCloudChanged;
        }

        void OnDisable()
        {
            _pointCloud.updated -= OnPointCloudChanged;
        }
        
        private void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
        {
            if (!_pointCloud.positions.HasValue || !_pointCloud.identifiers.HasValue ||
                !_pointCloud.confidenceValues.HasValue) return;

            var positions = _pointCloud.positions.Value;
            var identifiers = _pointCloud.identifiers.Value;
            var confidence = _pointCloud.confidenceValues.Value;

            StringBuilder messageBuilder = new StringBuilder();
            // string logMessage = "";
            if (positions.Length == 0)
                messageBuilder.AppendLine("No point");
            else
            {
                messageBuilder.AppendLine($"Number of points : {positions.Length}");
                messageBuilder.AppendLine($"Point info : ");
                messageBuilder.AppendLine($"x0 = {positions[0].x}, y0 = {positions[0].y}, z0 = {positions[0].z}");
                messageBuilder.AppendLine($"Identifier = {identifiers[0]}");
                messageBuilder.AppendLine($"Confidence = {confidence[0]}"); 
            }

            if (LogText == null)
                Debug.Log(messageBuilder.ToString());
            else
                LogText.text = messageBuilder.ToString();
        }
    }
}