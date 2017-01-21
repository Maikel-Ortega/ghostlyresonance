// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Bravo/MatCap/MatCap Diffuse Mask Transparency Cutout 2 sided FIXED Flag" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_Mask    ("Mask (Alpha)", 2D) = "black" {}	
		_MatCap ("MatCap (RGB)", 2D) = "black" {}
		_RimLightIntensity("Rimlight Intensity", FLOAT) = 2.0
		_Cutoff ("Alpha cutoff", FLOAT) = 0.5
		_Color ("Color", Color) = (1,1,1,1)
		_Amplitude ("Amplitude", FLOAT) = 5
		_Frequency ("Frequency", FLOAT) = 0.2
		_Speed("Speed", FLOAT) = 10
	}
	
	Subshader {
		Tags { "Queue"="Transparent-1"}
		Fog { Color [_AddFog] }
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater 0
		Cull Off
		
		Pass {
			//Name "BASE"
			//Tags { "LightMode" = "ForwardBase" }
			
			CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_fog_exp2
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f { 
					half4 	pos : SV_POSITION;
					half2	uv : TEXCOORD0;
					half3	TtoV0 : TEXCOORD1;
					half3	TtoV1 : TEXCOORD2;
					half3 	n : TEXCOORD3;
				};
				
				uniform float _Amplitude;
				uniform float _Frequency;
				uniform float _Speed;
				
				v2f vert (appdata_full v)
				{
					float4 worldPos = mul (unity_ObjectToWorld, v.vertex);
					worldPos.y += sin(worldPos.x * _Frequency + _Time.y * _Speed) * _Amplitude * (v.color.r);
					worldPos.z += sin(worldPos.x * _Frequency + _Time.y * _Speed) * _Amplitude * (v.color.r);
				
					v2f o;
					o.pos = mul (UNITY_MATRIX_VP, worldPos);
					o.uv = v.texcoord;
					
					TANGENT_SPACE_ROTATION;
					o.n = mul(rotation, v.normal);
					o.TtoV0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
					o.TtoV1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform sampler2D _Mask;
				uniform sampler2D _MatCap;
				uniform float _Cutoff;
				fixed _RimLightIntensity;
				float4 _Color;
				
				float4 frag (v2f i) : COLOR
				{
					half2 vn;
					vn.x = dot(i.TtoV0, i.n);
					vn.y = dot(i.TtoV1, i.n);

					fixed4 matcapLookup  = tex2D(_MatCap, vn*0.5 + 0.5);
					fixed4 mainTextColor = tex2D(_MainTex,i.uv);
					fixed4 maskTextColor = tex2D(_Mask,i.uv);
					
					fixed4 ret = (mainTextColor + (matcapLookup * _RimLightIntensity) * mainTextColor.a) * _Color;
					ret.a = maskTextColor.a;
					if (ret.a < _Cutoff)
					  discard;
					return ret;
				}
			ENDCG
		}
	}
}