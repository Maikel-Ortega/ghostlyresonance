// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-232-OUT,alpha-2221-A;n:type:ShaderForge.SFN_Tex2d,id:2221,x:31558,y:32675,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:232,x:32428,y:32832,varname:node_232,prsc:2|A-4784-OUT,B-2883-OUT,T-827-OUT;n:type:ShaderForge.SFN_Blend,id:4784,x:32053,y:32774,varname:node_4784,prsc:2,blmd:10,clmp:True|SRC-2221-RGB,DST-8669-RGB;n:type:ShaderForge.SFN_LightVector,id:7721,x:31111,y:33137,varname:node_7721,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3343,x:30677,y:32772,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:7287,x:31392,y:33115,varname:node_7287,prsc:2,dt:0|A-3343-OUT,B-7721-OUT;n:type:ShaderForge.SFN_Multiply,id:7321,x:31632,y:33115,varname:node_7321,prsc:2|A-2333-OUT,B-7287-OUT;n:type:ShaderForge.SFN_Vector1,id:2333,x:31392,y:33047,varname:node_2333,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:169,x:31828,y:33042,varname:node_169,prsc:2|A-2333-OUT,B-7321-OUT;n:type:ShaderForge.SFN_Clamp01,id:827,x:32029,y:32986,varname:node_827,prsc:2|IN-169-OUT;n:type:ShaderForge.SFN_Color,id:8669,x:31558,y:32830,ptovrint:False,ptlb:shadowcolor,ptin:_shadowcolor,varname:node_5347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Transform,id:731,x:31083,y:32353,varname:node_731,prsc:2,tffrom:0,tfto:3|IN-3343-OUT;n:type:ShaderForge.SFN_RemapRange,id:8938,x:31490,y:32365,varname:node_8938,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-2930-OUT;n:type:ShaderForge.SFN_Tex2d,id:6652,x:31714,y:32365,ptovrint:False,ptlb:matcap,ptin:_matcap,varname:node_2267,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:12c32b42e89c3c549974638c83f0e419,ntxv:2,isnm:False|UVIN-8938-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2930,x:31291,y:32353,varname:node_2930,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-731-XYZ;n:type:ShaderForge.SFN_Blend,id:2883,x:32250,y:32545,varname:node_2883,prsc:2,blmd:6,clmp:True|SRC-6652-RGB,DST-2221-RGB;proporder:2221-8669-6652;pass:END;sub:END;*/

Shader "Shader Forge/shd_halflambertmatcapalpha" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _shadowcolor ("shadowcolor", Color) = (0.5,0.5,0.5,1)
        _matcap ("matcap", 2D) = "black" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _shadowcolor;
            uniform sampler2D _matcap; uniform float4 _matcap_ST;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_8938 = (mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb.rg*0.5+0.5);
                float4 _matcap_var = tex2D(_matcap,TRANSFORM_TEX(node_8938, _matcap));
                float node_2333 = 0.5;
                float3 emissive = lerp(saturate(( _shadowcolor.rgb > 0.5 ? (1.0-(1.0-2.0*(_shadowcolor.rgb-0.5))*(1.0-_MainTex_var.rgb)) : (2.0*_shadowcolor.rgb*_MainTex_var.rgb) )),saturate((1.0-(1.0-_matcap_var.rgb)*(1.0-_MainTex_var.rgb))),saturate((node_2333+(node_2333*dot(i.normalDir,lightDirection)))));
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
