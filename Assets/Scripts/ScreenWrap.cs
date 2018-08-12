using UnityEngine;
using System.Collections;

public class ScreenWrap : MonoBehaviour {
	
	public Renderer[] renderers;
	private bool flipped = false;

	private bool isVisible() 
	{
		bool visible = true;
		foreach (Renderer renderer in renderers) 
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			if (!GeometryUtility.TestPlanesAABB(planes, renderer.bounds)) {
				visible = false;
				break;
			}
		}
		return visible;
	}

	void Update() 
	{
		bool visible = isVisible();
		if (visible) {
			flipped = false;
		}

		var previousPosition = transform.position;
		if (!visible && flipped == false) 
		{
			Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
			Vector3 currentPosition = transform.position;

			if (viewportPosition.x < 0 || viewportPosition.x > 1) 
			{
				Vector3 screenEdge;
				if (viewportPosition.x > 1) {
					screenEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
				} else {
					screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 1));
				}
				currentPosition.x = screenEdge.x;
			}
			if (viewportPosition.y < 0 || viewportPosition.y > 1) 
			{
				Vector3 screenEdge;
				if (viewportPosition.y > 1) {
					screenEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
				} else {
					screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 1));
				}
				flipped = true;
				currentPosition.y = screenEdge.y;
			}
			transform.position = currentPosition;
			transform.position = new Vector3(transform.position.x, transform.position.y, previousPosition.z);
		}
	}
}
