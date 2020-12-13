using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveyText : MonoBehaviour
{
    public TMP_Text textmesh;
    
    private void Update()
    {
        textmesh.ForceMeshUpdate();
        for (int i = 0; i < textmesh.textInfo.characterCount; ++i)
        {
            var chrInfo = textmesh.textInfo.characterInfo[i];

            if(!chrInfo.isVisible)
                continue;
            
            
            var verts = textmesh.textInfo.meshInfo[chrInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var org = verts[chrInfo.vertexIndex + j];
                verts[chrInfo.vertexIndex + j] = org + new Vector3(0, Mathf.Sin((Time.time + i)*5)*10, 0);
            }
        }

        for (int i = 0; i < textmesh.textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textmesh.textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textmesh.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
