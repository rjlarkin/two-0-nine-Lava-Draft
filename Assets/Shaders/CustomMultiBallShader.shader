Shader "Custom/MultiBallShader"
{
    Properties
    {
        /*Group 1 Properties*/
        _Threshold1("Group1 Threshold", Range(0,1)) = 0.3
        _OutlineTolerance1("Group1 OutlineTolerance", Range(0,0.1)) = 0.01
        _Color1("Group1 Color", Color) = (1, 1, 1, 1)
        _Ball0_Group1("Ball0 Group1", Vector) = (0,0,0,0)
        _Ball1_Group1("Ball1 Group1", Vector) = (0,0,0,0)
        // (Define more balls for Group1 here)

        /*Group 2 Properties*/
        _Threshold2("Group2 Threshold", Range(0,1)) = 0.5
        _OutlineTolerance2("Group2 OutlineTolerance", Range(0,0.1)) = 0.01
        _Color2("Group2 Color", Color) = (1, 0, 0, 1) // Red
        _Ball0_Group2("Ball0 Group2", Vector) = (0,0,0,0)
        _Ball1_Group2("Ball1 Group2", Vector) = (0,0,0,0)
        // (Define more balls for Group2 here)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            // Group 1 properties
            float _Threshold1;
            float _OutlineTolerance1;
            fixed4 _Color1;
            float4 _Ball0_Group1;
            float4 _Ball1_Group1;
            // (Add more balls here for Group1)

            // Group 2 properties
            float _Threshold2;
            float _OutlineTolerance2;
            fixed4 _Color2;
            float4 _Ball0_Group2;
            float4 _Ball1_Group2;
            // (Add more balls here for Group2)

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Function to calculate influence of a ball on the UV coordinates/
            float calcBall(float4 ball, float2 uv)
            {
                float xDiff = uv.x - ball.x;
                float yDiff = uv.y - ball.y;
                return ball.z * 1.0 / sqrt(xDiff * xDiff + yDiff * yDiff);
            }

            // Group 1 influence/
            float calcGroup1Influence(float2 uv)
            {
                float influence = calcBall(_Ball0_Group1, uv) + calcBall(_Ball1_Group1, uv);
                // (Add more balls if needed for Group1)
                return influence;
            }

            // Group 2 influence/
            float calcGroup2Influence(float2 uv)
            {
                float influence = calcBall(_Ball0_Group2, uv) + calcBall(_Ball1_Group2, uv);
                /*(Add more balls if needed for Group2)*/
                return influence;
            }

            // Fragment shader that handles two groups of balls*/
            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate influence for Group 1
                float influence1 = calcGroup1Influence(i.uv);
                float thresDiff1 = abs(influence1 - _Threshold1);
                float fillIntensity1 = min(10000 * max(0, influence1 - _Threshold1), 1);
                float outlineIntensity1 = max(0, (_OutlineTolerance1 - thresDiff1) / _OutlineTolerance1);
                fixed4 color1 = fixed4(_Color1.rgb, min(1.1, fillIntensity1 + outlineIntensity1));

                // Calculate influence for Group 2
                float influence2 = calcGroup2Influence(i.uv);
                float thresDiff2 = abs(influence2 - _Threshold2);
                float fillIntensity2 = min(10000 * max(0, influence2 - _Threshold2), 1);
                float outlineIntensity2 = max(0, (_OutlineTolerance2 - thresDiff2) / _OutlineTolerance2);
                fixed4 color2 = fixed4(_Color2.rgb, min(1.1, fillIntensity2 + outlineIntensity2));

                // Blend the colors from the two groups additively (you can change this logic to other blend modes if needed)/ 
                fixed4 finalColor = color1 + color2;

                // Clamp alpha to 1 to avoid overblown transparency/ 
                finalColor.a = min(1.0, finalColor.a);
                
                return finalColor;
            }

            ENDCG
        }
    }
}
