Shader "Custom/Clipping Mask"
{
   Properties
   {
      _MainTex ("Texture", 2D) = "white" {}
      _Mask ("Mask", 2D) = "white" {}
      _Cutoff ("Alpha Cutoff", Range (0,1)) = 0.1
   }
   SubShader
   {
      Tags {"Queue"="Transparent"}
      Lighting Off
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      AlphaTest GEqual [_Cutoff]
      Pass
      {
         SetTexture [_Mask] {combine texture}
         SetTexture [_MainTex] {combine texture, previous}
      }
   }
}