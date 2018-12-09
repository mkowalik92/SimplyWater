// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/top_water"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}
		_MainColor ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Cube ("Reflection Map", Cube) = "" {}
		_ReflectionAmount ("Reflection Amount", Range(0.0, 1.0)) = 1.0
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 normalDir : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
			};

			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			fixed4 _MainColor;
			uniform samplerCUBE _Cube;
			float _ReflectionAmount;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;
				o.viewDir = mul(modelMatrix, v.vertex).xyz
					- _WorldSpaceCameraPos;
				o.normalDir = normalize(
					mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
				o.vertex = UnityObjectToClipPos(v.vertex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				////fixed4 col = tex2D(_MainTex, i.uv);
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return _MainColor;
				float3 reflectedDir = reflect(i.viewDir, normalize(i.normalDir));
				return lerp(_MainColor, texCUBE(_Cube, reflectedDir), _ReflectionAmount);
			}
			ENDCG
		}
	}
}
