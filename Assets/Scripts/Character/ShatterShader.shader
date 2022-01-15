Shader "Custom/ShatterShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags
		{
			"Queue" = "Overlay"
		}

		Cull Off ZWrite Off ZTest Always
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
				float4 vbc : TEXCOORD2;
			};

			half _ScreenRatio;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 vbc : TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;

				// write vertex data to local var
				o.vertex = v.vertex;

				// adjust x to compensate for screen ratio
				o.vertex.x = o.vertex.x * _ScreenRatio;

				// transform vertex
				o.vertex.xyz = UnityObjectToViewPos(o.vertex);

				// revert x value to NDCs
				o.vertex.x = o.vertex.x / _ScreenRatio;

				// update barycentric coord
				o.vbc = v.vbc;

				// I don't know why this works but this prevents clipping when rotating 
				o.vertex.z = 0;

				// Pass in uv coords
				o.uv = v.uv;

				return o;
			}

			sampler2D _MainTex;

			half _Alpha;

			fixed4 frag(v2f i) : SV_Target
			{
				//i.vbc.b = (1 - i.vbc.r - i.vbc.g);
				//half3 a3 = i.vbc;

				//half3 d = fwidth(i.vbc);
				//half3 a3 = smoothstep(half3(0, 0, 0), d * 3, i.vbc);
				//half minimum = min(min(a3.x, a3.y), a3.z);

				fixed4 col = tex2D(_MainTex, i.uv);

			//col = lerp(col, fixed4(1, 1, 1, 1), 1 - minimum);

			col.a = _Alpha;

			return col;
		}
		ENDCG
	}
}
	IEnumerator RenderTriangles()
	{
		if (m_tex == null) yield return null;

		float offset = 0;
		float alpha = 1;
		float rotation = 0;
		float transformDelay = 0.5f;
		float transitionTime = 0;

		while (alpha > 0)
		{
			yield return new WaitForEndOfFrame();

			if (!m_underlayEnabled)
			{
				//GameManager.EnableLoadingScreen();
				m_underlayEnabled = true;

				Time.timeScale = 1;
			}

			if (!m_mat)
			{
				var shader = Shader.Find("Custom/ShatterShader");
				m_mat = new Material(shader);
				m_mat.hideFlags = HideFlags.HideAndDontSave;

				m_mat.mainTexture = m_tex;
			}

			GL.LoadOrtho();
			m_mat.SetPass(0);

			var screenratio = (float)Screen.width / Screen.height;
			m_mat.SetFloat("_ScreenRatio", screenratio);
			m_mat.SetFloat("_Alpha", alpha);

			for (int i = 0; i < m_triData.Count; i++)
			{
				GL.Begin(GL.TRIANGLES);

				for (int j = 0; j < 3; j++)
				{
					GL.MultiTexCoord(0, m_triData[i].UV[j]);
					GL.Vertex(m_triData[i].Vertices[j]);
					//GL.MultiTexCoord(2, m_triData[i].BC[j]);
				}

				var c = m_triData[i].Center;
				c.x *= screenratio;
				m_triData[i].Matrix = Matrix4x4.Translate(m_triData[i].Dir * offset);
				m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Translate(c);
				m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Rotate(Quaternion.Euler(m_triData[i].Rotation * rotation));
				m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Scale(new Vector3(0.97f, 0.97f, 0.97f));
				m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Translate(-c);

				GL.MultMatrix(m_triData[i].Matrix);

				GL.End();
			}

			if (transformDelay < transitionTime)
			{
				alpha -= 0.4f * Time.deltaTime;
				offset += 0.1f * Time.deltaTime;
				rotation += 0.4f * Time.deltaTime;
			}
			else
			{
				transitionTime += Time.deltaTime;
			}
		}

		//GameManager.DisableLoadingScreen();
	}
}
