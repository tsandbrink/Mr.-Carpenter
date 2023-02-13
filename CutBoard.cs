using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutBoard : MonoBehaviour
{
    //Can Perform Rip Cuts and Cross Cuts on a Board
    
    public Board boardToCut;
    public float ripCutWidth;
    public float crossCutWidth;
    public float bevelCutWidth;
    public float bevelAngle;
    public InputField ripInput;
    public InputField crossInput;
    public InputField bevelWidthInput;
    public InputField bevelAngleInput;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callRipCut(){
        ripCutWidth = (float.Parse(ripInput.text)/100);
        if (ripCutWidth < boardToCut.width)
        {
            ripCut2(ripCutWidth);
        }
        else
        {
            Debug.Log("Rip Cut Width Must be smaller than Board Width");
        }
    }

    public void callCrossCut()
    {
        boardToCut.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        crossCutWidth = (float.Parse(crossInput.text) / 100);

        if (crossCutWidth < boardToCut.length)
        {
            crossCut2(crossCutWidth);
        }
        else
        {
            Debug.Log("Cross Cut Width Must be smaller than Board Length");
        }
    }

    public void callBevelCut() 
    {
        boardToCut.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        bevelCutWidth = (float.Parse(bevelWidthInput.text) / 100);
        bevelAngle = (float.Parse(bevelAngleInput.text)*Mathf.Deg2Rad);
        if (bevelCutWidth < boardToCut.length)
        {
            bevelCut(bevelCutWidth, bevelAngle);
        }
        else
        {
            Debug.Log("Bevel Cut Width Must be smaller than Board Length");
        }
    }



    void ripCut2(float cutWidth)
    {
        //Generate Board 1
        GameObject newBoard;
        newBoard = new GameObject("newBoard");
        newBoard.AddComponent<Board>();
        //may need to assign length, width, and thickness via code
        
        MeshRenderer newBoardMeshRenderer = newBoard.AddComponent<MeshRenderer>();
        newBoardMeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoardMeshFilter = newBoard.AddComponent<MeshFilter>();

        
        Mesh newBoardMesh = new Mesh();
        
        float boardToCutPositionX = boardToCut.boardTransform.position.x;
        float boardToCutPositionY = boardToCut.boardTransform.position.y;
        float boardToCutPositionZ = boardToCut.boardTransform.position.z;

        Vector3[] newBoardVertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2 + cutWidth, 
                boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2 + cutWidth, 
                boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2 + cutWidth, 
                boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2 + cutWidth, 
                boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2)
        };
        newBoardMesh.vertices = newBoardVertices;

        int[] tris = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoardMesh.triangles = tris;
        
        //this section needs work
      /*  Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        newBoardMesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        newBoardMesh.uv = uv;*/
        //to make the color the same

        newBoardMeshFilter.mesh = newBoardMesh;
        newBoard.AddComponent<BoxCollider>();

        //Generate Board 2

        GameObject newBoard2;
        newBoard2 = new GameObject("newBoard2");
        newBoard2.AddComponent<Board>();
        //may need to assign length, width, and thickness via code

        MeshRenderer newBoard2MeshRenderer = newBoard2.AddComponent<MeshRenderer>();
        newBoard2MeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoard2MeshFilter = newBoard2.AddComponent<MeshFilter>();


        Mesh newBoard2Mesh = new Mesh();

        Vector3[] newBoard2Vertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2 - (boardToCut.width - cutWidth), 
                boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2 - (boardToCut.width - cutWidth), 
                boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2 - (boardToCut.width - cutWidth),
                boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2 - (boardToCut.width - cutWidth), 
                boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2)
        };
        newBoard2Mesh.vertices = newBoard2Vertices;

        int[] tris2 = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoard2Mesh.triangles = tris2;

        //this section needs work
       /* Vector3[] normals2 = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        newBoard2Mesh.normals = normals2;

        Vector2[] uv2 = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        newBoard2Mesh.uv = uv2;*/
        //to make the color the same

        newBoard2MeshFilter.mesh = newBoard2Mesh;
        newBoard2.AddComponent<BoxCollider>();
        newBoard2.transform.Translate(-.005f, 0, 0);

        Destroy(boardToCut.gameObject);
    }

    void crossCut2(float cutWidth)
    {
        //Generate Board 1
        GameObject newBoard;
        newBoard = new GameObject("newBoard");

        //When the newBoard gets generated it has a position of 0, 0, 0. The mesh is generated relative to this position. To center the new board's position on its
        //mesh, we have to move boardToCut so when we generate the new mesh, the center of the new mesh will be 0, 0, 0. First we save the boardToCut's current 
        // position so we can move back to it later. Then move boardToCut so our new mesh will be centered. 

        Vector3 boardToCutStartingPosition = boardToCut.transform.position;
        Vector3 newBoardPosition = new Vector3(boardToCut.transform.position.x, boardToCut.transform.position.y, boardToCut.transform.position.z + (cutWidth/2));
        Vector3 newBoardPosition2 = new Vector3(boardToCut.transform.position.x, boardToCut.transform.position.y,
            boardToCut.transform.position.z - (boardToCut.length - cutWidth) / 2);
    
        boardToCut.transform.position = new Vector3(0, 0, -cutWidth/2);
        newBoard.transform.position = newBoardPosition;
        newBoard.AddComponent<Board>();
        //may need to assign length, width, and thickness via code

        MeshRenderer newBoardMeshRenderer = newBoard.AddComponent<MeshRenderer>();
        newBoardMeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoardMeshFilter = newBoard.AddComponent<MeshFilter>();


        Mesh newBoardMesh = new Mesh();

        float boardToCutPositionX = boardToCut.transform.position.x;
        float boardToCutPositionY = boardToCut.transform.position.y;
        float boardToCutPositionZ = boardToCut.transform.position.z;

        Vector3[] newBoardVertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth)
        };

        newBoardMesh.SetVertices(newBoardVertices);
        Bounds newBoardMeshBounds = newBoardMesh.bounds;
        Debug.Log(newBoardMeshBounds);

        int[] tris = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoardMesh.triangles = tris;

        //this section needs work
        /*  Vector3[] normals = new Vector3[4]
          {
              -Vector3.forward,
              -Vector3.forward,
              -Vector3.forward,
              -Vector3.forward
          };
          newBoardMesh.normals = normals;

          Vector2[] uv = new Vector2[4]
          {
              new Vector2(0, 0),
              new Vector2(1, 0),
              new Vector2(0, 1),
              new Vector2(1, 1)
          };
          newBoardMesh.uv = uv;*/
        //to make the color the same

        newBoardMeshFilter.mesh = newBoardMesh;
        newBoard.AddComponent<BoxCollider>();

        //Generate Board 2

        GameObject newBoard2;
        newBoard2 = new GameObject("newBoard2");
        newBoard2.AddComponent<Board>();


        boardToCut.transform.position = new Vector3(0, 0, (boardToCut.length - cutWidth) / 2); //for centering
        newBoard2.transform.position = newBoardPosition2;

        //may need to assign length, width, and thickness via code

        MeshRenderer newBoard2MeshRenderer = newBoard2.AddComponent<MeshRenderer>();
        newBoard2MeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoard2MeshFilter = newBoard2.AddComponent<MeshFilter>();


        Mesh newBoard2Mesh = new Mesh();

        boardToCutPositionX = boardToCut.transform.position.x;
        boardToCutPositionY = boardToCut.transform.position.y;
        boardToCutPositionZ = boardToCut.transform.position.z;

        Vector3[] newBoard2Vertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2)
        };
        newBoard2Mesh.vertices = newBoard2Vertices;

        int[] tris2 = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoard2Mesh.triangles = tris2;

        //this section needs work
        /* Vector3[] normals2 = new Vector3[4]
         {
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward
         };
         newBoard2Mesh.normals = normals2;

         Vector2[] uv2 = new Vector2[4]
         {
             new Vector2(0, 0),
             new Vector2(1, 0),
             new Vector2(0, 1),
             new Vector2(1, 1)
         };
         newBoard2Mesh.uv = uv2;*/
        //to make the color the same

        newBoard2MeshFilter.mesh = newBoard2Mesh;
        newBoard2.AddComponent<BoxCollider>();

        float radius1 = newBoard.transform.position.z - boardToCutStartingPosition.z;
        float radius2 = newBoard2.transform.position.z - boardToCutStartingPosition.z;
        
        Vector3 movePoint1 = new Vector3(boardToCutStartingPosition.x - radius1, boardToCutStartingPosition.y, boardToCutStartingPosition.z);
        Vector3 movePoint2 = new Vector3(boardToCutStartingPosition.x + radius2, boardToCutStartingPosition.y, boardToCutStartingPosition.z);
    
        newBoard.transform.position = movePoint1;
        newBoard.transform.position = movePoint2;
        //newBoard.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        //newBoard2.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

        //newBoard2.transform.Translate(0, 0, -.05f);

       // Destroy(boardToCut.gameObject);
    }

    void bevelCut(float cutWidth, float angle)
    {
        //Generate Board 1
        GameObject newBoard;
        newBoard = new GameObject("newBoard"); 
        newBoard.AddComponent<Board>();
        //may need to assign length, width, and thickness via code

        MeshRenderer newBoardMeshRenderer = newBoard.AddComponent<MeshRenderer>();
        newBoardMeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoardMeshFilter = newBoard.AddComponent<MeshFilter>();


        Mesh newBoardMesh = new Mesh();

        float boardToCutPositionX = boardToCut.boardTransform.position.x;
        float boardToCutPositionY = boardToCut.boardTransform.position.y;
        float boardToCutPositionZ = boardToCut.boardTransform.position.z;

        Vector3[] newBoardVertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth - boardToCut.thickness*Mathf.Tan(angle)),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ + boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ - boardToCut.length/2 + cutWidth - boardToCut.thickness*Mathf.Tan(angle))
        };
        newBoardMesh.vertices = newBoardVertices;

        //review tris
        int[] tris = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoardMesh.triangles = tris;

        //this section needs work
        /*  Vector3[] normals = new Vector3[4]
          {
              -Vector3.forward,
              -Vector3.forward,
              -Vector3.forward,
              -Vector3.forward
          };
          newBoardMesh.normals = normals;

          Vector2[] uv = new Vector2[4]
          {
              new Vector2(0, 0),
              new Vector2(1, 0),
              new Vector2(0, 1),
              new Vector2(1, 1)
          };
          newBoardMesh.uv = uv;*/
        //to make the color the same

        newBoardMeshFilter.mesh = newBoardMesh;
        newBoard.AddComponent<BoxCollider>();

        //Generate Board 2

        GameObject newBoard2;
        newBoard2 = new GameObject("newBoard2");
        newBoard2.AddComponent<Board>();
        //may need to assign length, width, and thickness via code

        MeshRenderer newBoard2MeshRenderer = newBoard2.AddComponent<MeshRenderer>();
        newBoard2MeshRenderer.sharedMaterial = boardToCut.boardMaterial;

        MeshFilter newBoard2MeshFilter = newBoard2.AddComponent<MeshFilter>();


        Mesh newBoard2Mesh = new Mesh();

        Vector3[] newBoard2Vertices = new Vector3[8]
        {
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - ((boardToCut.length - cutWidth) + (boardToCut.thickness * Mathf.Tan(angle)))),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX + boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - (boardToCut.length - cutWidth)),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, 
                boardToCutPositionZ + boardToCut.length/2 - ((boardToCut.length - cutWidth) + (boardToCut.thickness * Mathf.Tan(angle)))),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY + boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2),
            new Vector3(boardToCutPositionX - boardToCut.width/2, boardToCutPositionY - boardToCut.thickness/2, boardToCutPositionZ - boardToCut.length/2)
        };
        newBoard2Mesh.vertices = newBoard2Vertices;

        int[] tris2 = new int[36]
        {  
            0, 2, 1,
            2, 3, 1,
            6, 2, 7,
            7, 2, 3,
            4, 6, 5,
            5, 6, 7,
            4, 0, 1,
            1, 5, 4,
            0, 2, 4,
            4, 2, 6,
            1, 3, 5,
            5, 3, 7
        };
        newBoard2Mesh.triangles = tris2;

        //this section needs work
        /* Vector3[] normals2 = new Vector3[4]
         {
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward
         };
         newBoard2Mesh.normals = normals2;

         Vector2[] uv2 = new Vector2[4]
         {
             new Vector2(0, 0),
             new Vector2(1, 0),
             new Vector2(0, 1),
             new Vector2(1, 1)
         };
         newBoard2Mesh.uv = uv2;*/
        //to make the color the same

        newBoard2MeshFilter.mesh = newBoard2Mesh;
        newBoard2.AddComponent<MeshCollider>();
        newBoard2.transform.Translate(0, 0, -.03f);

        Destroy(boardToCut.gameObject);
    }

}
