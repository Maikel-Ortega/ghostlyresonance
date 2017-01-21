Shader "Maikel/MatCap/UnlitMatcapStencil"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1) 
		_OutlineColor ("OutlineColor", Color) = (1,1,1,1) 		
		_MatCap ("MatCap (RGB)", 2D) = "black" {}
		_OutlineWidth("Width", Range(0,1)) = 0
		_MinWidth("MinWidth", float) = 1

	}
	SubShader
	{
		LOD 100
		Pass
		{
			Tags { "RenderType"="Transparent" "Queue"="Geometry"}
			Zwrite Off
			Cull Front
			ColorMask 0
			Stencil
			{
				Ref 1
				Comp always
				Pass replace
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 pos : TEXCOORD0;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4  pos : SV_POSITION;
			};

			float _OutlineWidth;
			float _MinWidth;

			v2f vert (appdata v)
			{
				v2f o;
				float routline =_MinWidth + _OutlineWidth * (1+_SinTime.w);
				v.vertex.xyz += ((normalize(v.normal)) * routline);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return (0,0,0,0);
			}

			ENDCG
		}

		Pass
		{
			Tags { "RenderType"="Opaque" "Queue"="Geometry"}
			Zwrite Off
			Cull Back
			Stencil
			{
				Ref 1
				Comp NotEqual
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 pos : TEXCOORD0;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4  pos : SV_POSITION;
			};

			float4 _OutlineColor;
			float _OutlineWidth;
			float _MinWidth;

			v2f vert (appdata v)
			{
				v2f o;
				float routline =_MinWidth + _OutlineWidth * (1+_SinTime.w);
				v.vertex.xyz += ((normalize(v.normal)) * routline );
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return _OutlineColor;
			}

			ENDCG
		}

		Pass
		{
			Tags { "RenderType"="Opaque" "Queue"="Geometry"}
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 pos : TEXCOORD0;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4  pos : SV_POSITION;
				half3 	n : TEXCOORD1;
			};

			float4 _Color;
			uniform sampler2D _MatCap;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.n = mul( UNITY_MATRIX_IT_MV,  v.normal);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 matcapLookup  = tex2D(_MatCap, normalize(i.n) * 0.5 + 0.5);
				return _Color + matcapLookup;
			}

			ENDCG
		}
	}
}
