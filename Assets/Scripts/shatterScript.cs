using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterScript : MonoBehaviour
{
    public Texture m_tex;
    public bool m_underlayEnabled;
    public Material m_mat;
    //public List<v2f> m_triData;
    private void Start()
    {
        StartCoroutine(RenderTriangles());
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

            //for (int i = 0; i < m_triData.Count; i++)
            //{
            //    GL.Begin(GL.TRIANGLES);

            //    for (int j = 0; j < 3; j++)
            //    {
            //        GL.MultiTexCoord(0, m_triData[i].UV[j]);
            //        GL.Vertex(m_triData[i].Vertices[j]);
            //        //GL.MultiTexCoord(2, m_triData[i].BC[j]);
            //    }

            //    var c = m_triData[i].Center;
            //    c.x *= screenratio;
            //    m_triData[i].Matrix = Matrix4x4.Translate(m_triData[i].Dir * offset);
            //    m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Translate(c);
            //    m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Rotate(Quaternion.Euler(m_triData[i].Rotation * rotation));
            //    m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Scale(new Vector3(0.97f, 0.97f, 0.97f));
            //    m_triData[i].Matrix = m_triData[i].Matrix * Matrix4x4.Translate(-c);

            //    GL.MultMatrix(m_triData[i].Matrix);

            //    GL.End();
            //}

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
