// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32433,y:32711|diff-123-OUT,custl-2-OUT,olwid-162-OUT,olcol-168-RGB;n:type:ShaderForge.SFN_Multiply,id:2,x:33059,y:32839|A-3-OUT,B-11-OUT;n:type:ShaderForge.SFN_Round,id:3,x:33249,y:32777|IN-5-OUT;n:type:ShaderForge.SFN_Clamp01,id:5,x:33412,y:32724|IN-6-OUT;n:type:ShaderForge.SFN_Subtract,id:6,x:33600,y:32724|A-7-OUT,B-29-OUT;n:type:ShaderForge.SFN_Add,id:7,x:33772,y:32724|A-9-OUT,B-8-OUT;n:type:ShaderForge.SFN_Clamp01,id:8,x:33989,y:32845|IN-21-OUT;n:type:ShaderForge.SFN_Clamp01,id:9,x:33989,y:32724|IN-27-OUT;n:type:ShaderForge.SFN_Round,id:11,x:33227,y:32956|IN-13-OUT;n:type:ShaderForge.SFN_Clamp01,id:13,x:33394,y:33003|IN-15-OUT;n:type:ShaderForge.SFN_Subtract,id:15,x:33582,y:33003|A-17-OUT,B-29-OUT;n:type:ShaderForge.SFN_Add,id:17,x:33783,y:33003|A-18-OUT,B-23-OUT;n:type:ShaderForge.SFN_Clamp01,id:18,x:33964,y:33003|IN-19-OUT;n:type:ShaderForge.SFN_RemapRange,id:19,x:34181,y:33003,frmn:0,frmx:1,tomn:-1,tomx:1|IN-35-OUT;n:type:ShaderForge.SFN_RemapRange,id:21,x:34181,y:32845,frmn:1,frmx:0,tomn:-1,tomx:1|IN-47-OUT;n:type:ShaderForge.SFN_Clamp01,id:23,x:33952,y:33169|IN-25-OUT;n:type:ShaderForge.SFN_RemapRange,id:25,x:34169,y:33169,frmn:1,frmx:0,tomn:-1,tomx:1|IN-35-OUT;n:type:ShaderForge.SFN_RemapRange,id:27,x:34181,y:32685,frmn:0,frmx:1,tomn:-1,tomx:1|IN-47-OUT;n:type:ShaderForge.SFN_RemapRange,id:29,x:33941,y:33406,frmn:0,frmx:1,tomn:1,tomx:-1|IN-30-OUT;n:type:ShaderForge.SFN_Multiply,id:30,x:34124,y:33421|A-31-OUT,B-32-OUT;n:type:ShaderForge.SFN_Dot,id:31,x:34384,y:33463,dt:0|A-33-OUT,B-34-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:32,x:34384,y:33615;n:type:ShaderForge.SFN_LightVector,id:33,x:34600,y:33423;n:type:ShaderForge.SFN_NormalVector,id:34,x:34619,y:33580,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:35,x:34374,y:33107,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-36-OUT;n:type:ShaderForge.SFN_Frac,id:36,x:34554,y:33107|IN-37-UVOUT;n:type:ShaderForge.SFN_Rotator,id:37,x:34752,y:33124|UVIN-74-OUT,ANG-80-OUT;n:type:ShaderForge.SFN_ComponentMask,id:47,x:34346,y:32762,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-49-OUT;n:type:ShaderForge.SFN_Frac,id:49,x:34526,y:32762|IN-51-UVOUT;n:type:ShaderForge.SFN_Rotator,id:51,x:34724,y:32779|UVIN-74-OUT,ANG-78-OUT;n:type:ShaderForge.SFN_Multiply,id:74,x:34877,y:32966|A-75-OUT,B-76-OUT;n:type:ShaderForge.SFN_RemapRange,id:75,x:35138,y:32930,frmn:-1,frmx:1,tomn:0,tomx:1|IN-77-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:76,x:35113,y:33166,ptlb:Hatching Tilling,ptin:_HatchingTilling,glob:False,v1:120;n:type:ShaderForge.SFN_ScreenPos,id:77,x:35334,y:32950,sctp:0;n:type:ShaderForge.SFN_Multiply,id:78,x:35128,y:32762|A-81-OUT,B-79-OUT;n:type:ShaderForge.SFN_Divide,id:79,x:35553,y:32980|A-85-OUT,B-86-OUT;n:type:ShaderForge.SFN_Multiply,id:80,x:35084,y:33276|A-79-OUT,B-84-OUT;n:type:ShaderForge.SFN_Add,id:81,x:35530,y:32739|A-83-OUT,B-84-OUT;n:type:ShaderForge.SFN_Vector1,id:83,x:35790,y:32731,v1:90;n:type:ShaderForge.SFN_ValueProperty,id:84,x:35755,y:32877,ptlb:Hatching Angle,ptin:_HatchingAngle,glob:False,v1:45;n:type:ShaderForge.SFN_Tau,id:85,x:35771,y:32963;n:type:ShaderForge.SFN_Vector1,id:86,x:35680,y:33106,v1:360;n:type:ShaderForge.SFN_Tex2d,id:111,x:32870,y:32613,ptlb:Diffuse,ptin:_Diffuse,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Blend,id:123,x:32661,y:32698,blmd:1,clmp:True|SRC-217-OUT,DST-111-RGB;n:type:ShaderForge.SFN_ValueProperty,id:162,x:32728,y:32955,ptlb:Outline Width,ptin:_OutlineWidth,glob:False,v1:0.01;n:type:ShaderForge.SFN_Color,id:168,x:32671,y:33090,ptlb:Outline Color,ptin:_OutlineColor,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Blend,id:217,x:32893,y:32942,blmd:6,clmp:True|SRC-219-RGB,DST-2-OUT;n:type:ShaderForge.SFN_Color,id:219,x:33116,y:33108,ptlb:Hatching Color,ptin:_HatchingColor,glob:False,c1:0,c2:0,c3:0,c4:1;proporder:111-219-76-84-168-162;pass:END;sub:END;*/

Shader "Shader Forge/ComicShader" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "black" {}
        _HatchingColor ("Hatching Color", Color) = (0,0,0,1)
        _HatchingTilling ("Hatching Tilling", Float ) = 210
        _HatchingAngle ("Hatching Angle", Float ) = 45
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Float ) = 0.01
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_OutlineWidth,1));
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _HatchingTilling;
            uniform float _HatchingAngle;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _HatchingColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float node_79 = (6.28318530718/360.0);
                float node_51_ang = ((90.0+_HatchingAngle)*node_79);
                float node_51_spd = 1.0;
                float node_51_cos = cos(node_51_spd*node_51_ang);
                float node_51_sin = cos(node_51_spd*node_51_ang);
                float2 node_51_piv = float2(0.5,0.5);
                float2 node_74 = ((i.screenPos.rg*0.5+0.5)*_HatchingTilling);
                float2 node_51 = (mul(node_74-node_51_piv,float2x2( node_51_cos, -node_51_sin, node_51_sin, node_51_cos))+node_51_piv);
                float node_47 = frac(node_51).r;
                float node_29 = ((dot(lightDirection,i.normalDir)*attenuation)*-2.0+1.0);
                float node_37_ang = (node_79*_HatchingAngle);
                float node_37_spd = 0;
                float node_37_cos = cos(node_37_spd*node_37_ang);
                float node_37_sin = cos(node_37_spd*node_37_ang);
                float2 node_37_piv = float2(0.5,0.5);
                float2 node_37 = (mul(node_74-node_37_piv,float2x2( node_37_cos, -node_37_sin, node_37_sin, node_37_cos))+node_37_piv);
                float node_35 = frac(node_37).r;
                float node_2 = (round(saturate(((saturate((node_47*2.0+-1.0))+saturate((node_47*-2.0+1.0)))-node_29)))*round(saturate(((saturate((node_35*2.0+-1.0))+saturate((node_35*-2.0+1.0)))-node_29))));
                float2 node_249 = i.uv0;
                finalColor += diffuseLight * saturate((saturate((1.0-(1.0-_HatchingColor.rgb)*(1.0-node_2)))*tex2D(_Diffuse,TRANSFORM_TEX(node_249.rg, _Diffuse)).rgb));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _HatchingTilling;
            uniform float _HatchingAngle;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _HatchingColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float node_79 = (6.28318530718/360.0);
                float node_51_ang = ((90.0+_HatchingAngle)*node_79);
                float node_51_spd = 1.0;
                float node_51_cos = cos(node_51_spd*node_51_ang);
                float node_51_sin = sin(node_51_spd*node_51_ang);
                float2 node_51_piv = float2(0.5,0.5);
                float2 node_74 = ((i.screenPos.rg*0.5+0.5)*_HatchingTilling);
                float2 node_51 = (mul(node_74-node_51_piv,float2x2( node_51_cos, -node_51_sin, node_51_sin, node_51_cos))+node_51_piv);
                float node_47 = frac(node_51).r;
                float node_29 = ((dot(lightDirection,i.normalDir)*attenuation)*-2.0+1.0);
                float node_37_ang = (node_79*_HatchingAngle);
                float node_37_spd = 1.0;
                float node_37_cos = cos(node_37_spd*node_37_ang);
                float node_37_sin = sin(node_37_spd*node_37_ang);
                float2 node_37_piv = float2(0.5,0.5);
                float2 node_37 = (mul(node_74-node_37_piv,float2x2( node_37_cos, -node_37_sin, node_37_sin, node_37_cos))+node_37_piv);
                float node_35 = frac(node_37).r;
                float node_2 = (round(saturate(((saturate((node_47*2.0+-1.0))+saturate((node_47*-2.0+1.0)))-node_29)))*round(saturate(((saturate((node_35*2.0+-1.0))+saturate((node_35*-2.0+1.0)))-node_29))));
                float2 node_250 = i.uv0;
                finalColor += diffuseLight * saturate((saturate((1.0-(1.0-_HatchingColor.rgb)*(1.0-node_2)))*tex2D(_Diffuse,TRANSFORM_TEX(node_250.rg, _Diffuse)).rgb));
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
