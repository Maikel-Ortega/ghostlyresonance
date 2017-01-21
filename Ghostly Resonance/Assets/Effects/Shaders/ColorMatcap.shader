Shader "Maikel/MatCap/ColorMatcap"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1) 
		_MatCap ("MatCap (RGB)", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4  pos : SV_POSITION;
				half3	TtoV0 : TEXCOORD1;
				half3	TtoV1 : TEXCOORD2;
				half3 	n : TEXCOORD3;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			uniform sampler2D _MatCap;
			
			v2f vert (appdata_tan v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				o.n = mul( normalize(v.normal), UNITY_MATRIX_IT_MV);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 matcapLookup  = tex2D(_MatCap, i.n * 0.5 + 0.5);
				return _Color + matcapLookup;
			}

			ENDCG
		}
	}
}
