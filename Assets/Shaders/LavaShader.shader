Shader "Custom/LavaShader"
{
	Properties
	{
		/*Defines the intensity cutoff for how much influence the balls have before filling the area with color.*/
		_Threshold("Threshold", Range(0,1)) = 0.3 
		_Threshold2("Second Threshold", Range(0,1)) = 0.5
		_Threshold3("Third Threshold", Range(0,1)) = 0.7

		/*Defines the width of the outline around the influenced areas, controlling the sharpness of the outline.*/
		_OutlineTolerance("OutlineTolerance", Range(0,0.1)) = 0.01 

		_OutlineColor("OutlineColor", Color) = (1,1,1,1) /*Control the color of the outline and the main fill color of the lava effect.*/

		/*Control the color of the main fill color of the lava effect.*/
		_Color("Color", Color) = (1,1,1,1) 
		_Color2("Second Color", Color) = (1, 0, 0, 1)   // Red
		_Color3("Third Color", Color) = (0, 0, 1, 1)    // Blue


		// Balls - (x,y,intensity,unused)
		_Ball0("Ball0", Vector) = (0,0,0,0)
		_Ball1("Ball1", Vector) = (0,0,0,0)
		_Ball2("Ball2", Vector) = (0,0,0,0)
		_Ball3("Ball3", Vector) = (0,0,0,0)
		_Ball4("Ball4", Vector) = (0,0,0,0)
		_Ball5("Ball5", Vector) = (0,0,0,0)
		_Ball6("Ball6", Vector) = (0,0,0,0)
		_Ball7("Ball7", Vector) = (0,0,0,0)
		_Ball8("Ball8", Vector) = (0,0,0,0)
		_Ball9("Ball9", Vector) = (0,0,0,0)
		/*These are vectors (x, y, intensity) representing the positions and intensities of the “balls” that influence the visual effect.
			- Each "ball" is a point of influence that affects the pixels' intensity based on the distance from the ball’s center.*/
	}
		SubShader //rendering settings
		{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 100

			ZWrite Off //Disables writing to the depth buffer (useful for transparent objects).
			Blend SrcAlpha OneMinusSrcAlpha //Specifies how the blending of the shader works, using alpha blending for transparency.

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
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};


				float _Threshold;
				float _Threshold2;
				float _Threshold3;
				float _OutlineTolerance;
				fixed4 _Color;
				fixed4 _Color2;
				fixed4 _Color3;
				fixed4 _OutlineColor;

				float4 _Ball0;
				float4 _Ball1;
				float4 _Ball2;
				float4 _Ball3;
				float4 _Ball4;
				float4 _Ball5;
				float4 _Ball6;
				float4 _Ball7;
				float4 _Ball8;
				float4 _Ball9;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float calcBall(float4 ball, float2 uv) {
					float xDiff = uv.x - ball.x;
					float yDiff = uv.y - ball.y;
					return ball.z * 1.0/sqrt(xDiff*xDiff + yDiff*yDiff);
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float influence = calcBall(_Ball0, i.uv) + calcBall(_Ball1, i.uv) + calcBall(_Ball2, i.uv) + calcBall(_Ball3, i.uv) + calcBall(_Ball4, i.uv) 
									+ calcBall(_Ball5, i.uv) + calcBall(_Ball6, i.uv) + calcBall(_Ball7, i.uv) + calcBall(_Ball8, i.uv) + calcBall(_Ball9, i.uv);

					float fillIntensity = min(10000*max(0, influence - _Threshold), 1);

					float thresDiff = abs(influence - _Threshold);
					float outlineIntensity = max(0, (_OutlineTolerance - thresDiff) / _OutlineTolerance);

				// Interpolate between colors based on the influence value
				fixed4 finalColor;
    
				if (influence < _Threshold2)
				{
					// Between Color1 and Color2
					finalColor = lerp(_Color, _Color2, smoothstep(_Threshold, _Threshold2, influence));
				}
				else if (influence < _Threshold3)
				{
					// Between Color2 and Color3
					finalColor = lerp(_Color2, _Color3, smoothstep(_Threshold2, _Threshold3, influence));
				}
				else
				{
					// Above Threshold3, use Color3
					finalColor = _Color3;
				}

				return fixed4(finalColor.rgb, min(1.1, fillIntensity + outlineIntensity));


					/*fixed4 col = fixed4(_Color.x, _Color.y, _Color.z, min(1.1, fillIntensity + outlineIntensity));
					
					return col;*/
				}

			ENDCG
		}
		}
}
