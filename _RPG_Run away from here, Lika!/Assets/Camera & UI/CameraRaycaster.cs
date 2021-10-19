using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using RPG.Character;

namespace RPG.CameraUI
{
	public class CameraRaycaster : MonoBehaviour
	{
		// INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
		//[SerializeField] int[] layerPriorities=null;
		[SerializeField] Texture2D walkCursor = null;
		//[SerializeField] Texture2D unknownCursor = null;
		[SerializeField] Texture2D targetCursor = null;
		[SerializeField] Texture2D buttonCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);
		[SerializeField] const int walkableLayerNumber = 6;
		[SerializeField] const int enemyLayerNumber = 7;

		float maxRaycastDepth = 100f; // Hard coded value		

		
		public delegate void OnMouseoverTerrain(Vector3 destination); 
		public event OnMouseoverTerrain notifyOnMouseoverTerrainObservers; 

		public delegate void OnMouseoverEnemy(Enemy enemy); 
		public event OnMouseoverEnemy notifyOnMouseoverEnemyObservers; 

		void Update()
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
				Cursor.SetCursor(buttonCursor, cursorHotspot, CursorMode.Auto);
				//  NotifyObserersIfLayerChanged(5);
				//  return; // Stop looking for other objects
			}
            else
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (RaycastForEnemy(ray)) {return;}
                if (RaycastForWalkable(ray)) {return;}
				
            }

            bool RaycastForWalkable(Ray ray)
			{
				RaycastHit raycastHit;
				LayerMask layerMask = 1 << walkableLayerNumber;
				bool walkableHit = Physics.Raycast(ray, out raycastHit, maxRaycastDepth, layerMask);
				if (walkableHit)
				{
					Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
					notifyOnMouseoverTerrainObservers(raycastHit.point);					
					return true;
				}
				return false;
			}

			bool RaycastForEnemy(Ray ray)
			{
				RaycastHit raycastHit;
				Physics.Raycast(ray, out raycastHit, maxRaycastDepth);
				var enemy = raycastHit.collider.gameObject;
				Enemy enemyHit = enemy.GetComponent<Enemy>();				
				if (enemyHit)
                {				
					Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
					notifyOnMouseoverEnemyObservers(enemyHit);
					return true;
                }
				return false;
			}
            //void RaycastingLayers()
            //{
            //    // Raycast to max depth, every frame as things can move under mouse
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

            //    RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
            //    if (!priorityHit.HasValue) // if hit no priority object
            //    {
            //        NotifyObserersIfLayerChanged(0); // broadcast default layer
            //        return;
            //    }

            //    // Notify delegates of layer change
            //    var layerHit = priorityHit.Value.collider.gameObject.layer;
            //    NotifyObserersIfLayerChanged(layerHit);

            //    // Notify delegates of highest priority game object under mouse when clicked
            //    if (Input.GetMouseButton(0))
            //    {
            //        notifyMouseClickObservers(priorityHit.Value, layerHit);
            //    }
            //    if (Input.GetMouseButtonDown(1))
            //    {
            //        notifyMouseRightClickObservers(priorityHit.Value, layerHit);
            //    }
            //}
        }


 //       void NotifyObserersIfLayerChanged(int newLayer)
	//	{
	//		if (newLayer != topPriorityLayerLastFrame)
	//		{
	//			topPriorityLayerLastFrame = newLayer;
	//			notifyLayerChangeObservers(newLayer);
	//		}
	//	}

	//	RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits)
	//	{
	//		// Form list of layer numbers hit
	//		List<int> layersOfHitColliders = new List<int>();
	//		foreach (RaycastHit hit in raycastHits)
	//		{
	//			layersOfHitColliders.Add(hit.collider.gameObject.layer);
	//		}

	//		// Step through layers in order of priority looking for a gameobject with that layer
	//		foreach (int layer in layerPriorities)
	//		{
	//			foreach (RaycastHit hit in raycastHits)
	//			{
	//				if (hit.collider.gameObject.layer == layer)
	//				{
	//					return hit; // stop looking
	//				}
	//			}
	//		}
	//		return null; // because cannot use GameObject? nullable
	//	}
	}
}