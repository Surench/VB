using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour {


    [SerializeField] MeshRenderer mesh;

    public void InitTube(bool isGray){
        if(isGray){
            mesh.material = GameManager.self.colorManager.Tube1Materials[ColorManager.CurrentColorIndex];
        }
        else{
            mesh.material = GameManager.self.colorManager.Tube2Materials[ColorManager.CurrentColorIndex];
        }
    }


}//end
