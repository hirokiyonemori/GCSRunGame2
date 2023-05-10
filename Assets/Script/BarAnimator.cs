using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAnimator : MonoBehaviour
{
	public float dampening = 5f;
	public float changeSpeed = 1.5f;

	float timeout = 0f;

	Material mat;
	float fillTarget = 1f;
	float delta = 0f;
	public float value = 0;
	private float beforValue = 0;

	void Awake()
	{

		Renderer rend = GetComponent<Renderer>();
		Image img = GetComponent<Image>();
		if (rend != null)
		{
			mat = new Material(rend.material);
			rend.material = mat;
		}
		else if (img != null)
		{
			mat = new Material(img.material);
			img.material = mat;
		}
		else
		{
			Debug.LogWarning("No Renderer or Image attached to " + name);
		}


	}

	private async UniTaskVoid Update()
	{
		if(value != beforValue)
		{
			beforValue = value;
			float newFill = value;
			//LogSystem.Log(" newFill " + newFill);
			// Modify delta by how much fillTarget will change
			delta -= fillTarget - newFill;
			fillTarget = newFill;
		}
		timeout += Time.deltaTime * changeSpeed;
		//LogSystem.Log(" timeout " + timeout);
		if (timeout > 1.0f)
		{
			timeout = 0f;

			// Choose new fill value 
			

		}

		// The main idea of animating the bar this way is 
		// 1. Set "_Fill" to whatever value the bar actually has [0, 1]
		// 2. Gradually bring "_Delta" to zero

		// For a slightly different effect, 
		// 1. Keep "_Delta" at zero 
		// 2. Lerp "_Fill" to the target value [0, 1]

		// Also: See the included shader for more information about other properties.

		delta = Mathf.Lerp(delta, 0, Time.deltaTime * dampening);

		mat.SetFloat("_Delta", delta);
		mat.SetFloat("_Fill", fillTarget);

		await UniTask.Yield();
	}
}
