using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objectInspector : MonoBehaviour
{
	// the prefab (3d model) to be passed  	
	public GameObject obToUse;
	//second cam
	public Camera obCamera;
	public Camera mainCamera;
	public GameObject rootPrefabDelete;

	//3D MODEL VIEWER ROT & SCALE
	public float rotSpeed = 20;
	Vector3 minScale = new Vector3(100f, 100f, 100f);
	Vector3 maxScale = new Vector3(150, 150, 150);
	

	//ob is for making an instance of the object 
	private GameObject ob;

	//3D PANNING
	public float panSpeed = 15f;
	public bool limitPan = true;
	public float yMax = 2f;
	public float yMin = -2f;
	public float xMax = 2.7f;
	public float xMin = -3.0f;

	//Rendertex passed in from Assets, to be used to project the cam view over the UI
	public RenderTexture rt;

	/*
		Bounds getRenderBounds(GameObject objeto)
		{
			Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
			Renderer render = objeto.GetComponent<Renderer>();
			if (render != null)
			{
				return render.bounds;
			}
			return bounds;
		}
	*/
	public void SetComponentEnabled(Component component, bool value)
	{
		if (component == null) return;
		if (component is Renderer)
		{
			(component as Renderer).enabled = value;
		}
		else if (component is Collider)
		{
			(component as Collider).enabled = value;
		}
		else if (component is Animation)
		{
			(component as Animation).enabled = value;
		}
		else if (component is Animator)
		{
			(component as Animator).enabled = value;
		}
		else if (component is AudioSource)
		{
			(component as AudioSource).enabled = value;
		}
		else if (component is MonoBehaviour)
		{
			(component as MonoBehaviour).enabled = value;
		}
		else
		{
			Debug.Log("Don't know how to enable " + component.GetType().Name);
		}
	}

	private void Start()
    {
		//obCamera = gameObject.AddComponent<Camera>();
		

		//check for null and dusplicate the ob passed in first
		if (obToUse == null)
		{
			print("NO object selected in " + this.gameObject.GetComponent<MonoBehaviour>());
			//Destroy(GetComponentInParent<objectInspector>().gameObject);
			Destroy(GetComponent<objectInspector>().gameObject);
			return;
		}
		//else
		//	Debug.Log("Object Loaded: "+obToUse.name);
		

		ob = Instantiate(obToUse, obCamera.transform.forward * 5 + obCamera.transform.position, Quaternion.identity) as GameObject;
			
		
		foreach (var component in ob.GetComponents<Component>())
		{
			if (component is MonoBehaviour)
				SetComponentEnabled(component, false);
		}

	


		float height = obCamera.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		float size = ob.transform.localScale.x;
		ob.transform.localScale = (Vector3.one * size) * height / 10f;
		ob.transform.localScale = (Vector3.one * size) * width / 10f;

		ob.transform.parent = gameObject.transform;

		//disable main game cam (not necessary?) 
		mainCamera=Camera.main;
		mainCamera.enabled = false;
		
		//enable second cam for Object Examination Menu
	    obCamera.enabled = true;
	 			
		//center object
		ob.transform.position = obCamera.transform.position + obCamera.transform.forward;


		//set all objects to ItemExamine layer so cam only renders the main ob not the background
		foreach (Transform child in ob.transform)
		{
			child.gameObject.layer = 9;
		}
		ob.layer = 9;

		//obCamera.orthographicSize = 25;
		//obCamera.cullingMask= 1 << 9;
		//obCamera.nearClipPlane = -15f;
		
		
		//	rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
		//	rt.Create();
		//	obCamera.targetTexture = rt;


		//ob.AddComponent<Pan>();

		//targetBounds = getRenderBounds(ob);

	}





	/*
	void OnGUI()
	{
		if (guiEnabled)
		{
			GUI.skin = skinny;

			GUI.Label(new Rect(screenwidth - 175, screenhieght - 50, 200, 30), "ITEM FOUND");
		//	GUI.Label(new Rect(screenwidth - 130, screenhieght - 30, 200, 30), "by Fusobotic");

		//	GUI.Label(new Rect(15, screenhieght - 60, 500, 30), "Hit M to mute audio, H to hide user interface");
			GUI.Label(new Rect(20, screenhieght - 40, 500, 30), "Hit R to reset view and Escape to quit");

			GUI.Label(new Rect(screenwidth - 310, 30, 500, 30), "Left click to rotate, middle click to zoom");
			GUI.Label(new Rect(screenwidth - 250, 60, 500, 30), "Alt-Left click to adjust lights");
			GUI.Label(new Rect(screenwidth - 175, 90, 500, 30), "WASD to pan");
/*
			//add additional buttons/scenes here, remember to go 40 units down, and one up on the LoadLevel and "model#"
			if (GUI.Button(new Rect(20, 20, buttonWidth, 25), model1))
			{
				SceneManager.LoadScene(0);
			}
			if (GUI.Button(new Rect(20, 60, buttonWidth, 25), model2))
			{
				SceneManager.LoadScene(1);
			}
			if (GUI.Button(new Rect(20, 100, buttonWidth, 25), model3))
			{
				SceneManager.LoadScene(2);
			}
			if (GUI.Button(new Rect(20, 140, buttonWidth, 25), model4))
			{
				SceneManager.LoadScene(3);
			}
		}
	}*/


	private void Update()
    {
		// Allow panning using the ASWD keys- restricted by xMin, xMax - Horizontal, yMin, Ymax- Vertical
		
		Vector3 inputVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f) * panSpeed;				
		ob.transform.position += inputVelocity * Time.deltaTime;
		ob.transform.Rotate(Vector3.up * 15 * Time.deltaTime);

		if (limitPan)
		{
			var y = Mathf.Clamp(ob.transform.position.y, yMin, yMax);
			var x = Mathf.Clamp(ob.transform.position.x, xMin, xMax);
			ob.transform.position = new Vector3(x, y, 0f);
		}

		
	}


    private void LateUpdate() // Look rotation (UP down is Camera) (Left right is Transform rotation)
	{
        // Allow scaling in and out of Object restricted by the minScale and maxScale values
        float zoomValue = Input.GetAxis("Mouse ScrollWheel");

		if (Input.GetMouseButton(0))
		{
			ob.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotSpeed);
		}

		if (zoomValue != 0)
		{
			ob.transform.localScale += Vector3.one *zoomValue*20;
			//ob.transform.localScale = Vector3.Max(ob.transform.localScale, minScale);
		//	ob.transform.localScale = Vector3.Min(ob.transform.localScale, maxScale);
		}/*
		else
        {
			ob.transform.localScale += Vector3.one * 100;
			ob.transform.localScale = Vector3.Max(ob.transform.localScale, minScale);
			ob.transform.localScale = Vector3.Min(ob.transform.localScale, maxScale);
		}*/
	}

}
