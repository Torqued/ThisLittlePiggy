using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
	public class grayscaleNight : ImageEffectBase {
		public Texture  textureRamp;
		public float    rampOffset;
		public bool 	grayscaled=false;
		public DayNightCycle dayCycle;
		
		// Called by camera to apply image effect
		void OnRenderImage (RenderTexture source, RenderTexture destination) {
			if (grayscaled) {
				material.SetTexture ("_RampTex", textureRamp);
				material.SetFloat ("_RampOffset", rampOffset);
				Graphics.Blit (source, destination, material);
			}
		}
		
		void Update () {
			if (dayCycle.isNightTime () && grayscaled == false) {
				grayscaled = true;
			}else if (!dayCycle.isNightTime () && grayscaled == true) {
				grayscaled = false;
			}
		}
	}
}