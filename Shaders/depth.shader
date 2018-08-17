Shader "Custom/Depth Fade" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_ShoreColor("Shore Color", Color) = (1,1,1,1)
		_Depth("Depth Fade", Float) = 1.0
		_Fix("Depth Distance", Float) = -0.09
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200

		ZWrite Off

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		sampler2D _CameraDepthTexture;

	half _Depth;
	half _Fix;

	fixed4 _Color, _ShoreColor;

	struct v2f
	{
		float4 pos : SV_POSITION;
		float4 uv : TEXCOORD0;
		float3 worldNorm : TEXCOORD1;
		float3 viewDir : TEXCOORD2;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = ComputeScreenPos(o.pos);
		o.worldNorm = UnityObjectToWorldNormal (v.normal);
		o.viewDir = WorldSpaceViewDir (v.vertex);
		return o;
	}

	half4 frag(v2f i) : SV_Target
	{
		float2 uv = i.uv.xy / i.uv.w;

		float rim = saturate (dot (normalize (i.viewDir), i.worldNorm));

		half lin = LinearEyeDepth(tex2D(_CameraDepthTexture, uv).r);
		half dist = i.uv.w - _Fix;
		half depth = lin - dist;

		return lerp (half4 (_ShoreColor.rgb,0), _Color, saturate (depth * _Depth * rim));
		//return lerp(half4 (1,1,1,0), _Color, saturate(depth * _Depth));
	}
		ENDCG
	}
	}
		FallBack "Diffuse"
}
