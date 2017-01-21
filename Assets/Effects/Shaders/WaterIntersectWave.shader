Shader "Maikel/Effects/WaterIntersectWave"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1) 
	}
	SubShader
	{
		Blend One One
		Zwrite Off
		Cull Off
		Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
	
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 screenuv : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float depth : DEPTH;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _Color;
			sampler2D _CameraDepthNormalsTexture;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				o.screenuv = ((o.vertex.xy / o.vertex.w) +1)*0.5;
				o.screenuv.y = 1- o.screenuv.y;
				o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z * _ProjectionParams.w;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float screenDepth = DecodeFloatRG(tex2D(_CameraDepthNormalsTexture, i.screenuv).zw);
				float diff = screenDepth - i.depth;
				float intersect = 0;

				if( diff > 0)
				{
					intersect = 1 - smoothstep(0, _ProjectionParams.w*0.5, diff);
				}


				//fixed4 glowColor = fixed4(lerp(_Color.rgb, fixed3(1, 1, 1), pow(intersect, 4)), 1);


				//intersect *= sin(_Time.x);

				fixed4 col = _Color *_Color.a + intersect;

				return col;
			}
			ENDCG
		}
	}
}
