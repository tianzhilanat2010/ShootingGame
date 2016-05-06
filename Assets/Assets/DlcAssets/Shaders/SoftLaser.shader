Shader "Custom/SoftLaser" {
  Properties {
      _MainTex ("RGBA Texture Image", 2D) = "white" {} 
	  _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
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
 
         uniform sampler2D _MainTex;    
         uniform float _Cutoff;
		 uniform float4 _Color;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.tex = input.texcoord;
			output.tex.y = 0.875f + output.tex.y * 0.125f;
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float4 c = tex2D(_MainTex, input.tex.xy); 
			c.r += _Color.r - _Color.a; 
			c.g += _Color.g - _Color.a; 
			c.b += _Color.b - _Color.a; 
			c.rgb *= 2.0f;
			//c.r = 1.0f;
			//c.gb -= (1.0f - c.a);
            return c;
         }
 
         ENDCG
      }
}
}