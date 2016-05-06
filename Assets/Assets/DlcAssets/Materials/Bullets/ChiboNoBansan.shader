Shader "Custom/ChiboNoBansan" {
  Properties {
  	  _scale ("scale", Float) = 0
  	  _rate  ("rate", Range (0, 1)) = 0
  	  _destroy("destroy", Float) = 1.01
   }
   SubShader {
      Tags {"Queue" = "Transparent"} 
 
      Pass {	
         Cull Off // first render the back faces

         ZWrite Off // don't write to depth buffer 
         
         AlphaTest Off
            // in order not to occlude other objects
         Blend SrcAlpha OneMinusSrcAlpha 
            // blend based on the fragment's alpha value
		 Lighting Off
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
		 uniform float _scale;
		 uniform float _rate;
		 uniform float _destroy;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 extra  : TEXCOORD1;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            float4 localpos = input.vertex;
			
            if (_rate > 1.0 - input.extra.x && input.extra.x < _destroy)
            {
            	localpos.xyz += input.normal * _rate * _scale * input.extra.x;
            }
            output.pos = mul(UNITY_MATRIX_MVP, localpos);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float4 c;
            c.rgba = 1.0f;
            return c;
         }
 
         ENDCG
      }
}
}
