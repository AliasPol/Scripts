using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

    Power current_Power;
    public GameObject iceBlock;
    public int colliderPosible;
    public Sprite spriteForCollect;
    public Color[] color;

    private List<GameObject> list = new List<GameObject>();
    private Color oldColor;
    private List<Color> listOfColor = new List<Color>();
    private RigidbodyType2D rbType;
    private bool uncheckPower = true;

    private bool colide;
    public bool Colide {
        set { colide = value; }
    }

    private static BlockBehavior _Instance;
    public static BlockBehavior Instance {
        get
        {
            if (_Instance == null) {
                _Instance = FindObjectOfType<BlockBehavior>();
            }
            return _Instance;
        }
    }

    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    public void UncheckPower() {

        if (uncheckPower) {
            int i = 0;

            foreach (GameObject a in list) {
                a.GetComponent<SpriteRenderer>().color = listOfColor[i];
                i++;
            }
            listOfColor.Clear();
            list.Clear();
        }
    }

    public void StartPower() {
        switch (current_Power) {
            case Power.none:
                Debug.LogError("USED DESTROY");
                IceBlockTapped();
                break;
            case Power.move:
                Debug.LogError("USED MOVE");
                StartCoroutine(MovePower());
                break;
            case Power.freeze:
                Debug.LogError("USED FREEZE");
                StartCoroutine(FreezePower());
                break;
            case Power.swap:
                Debug.LogError("USED SWAP");
                StartCoroutine(SwapPower());
                break;
            case Power.copy:
                Debug.LogError("USED COPY");
                StartCoroutine(CopyPower());
                break;
            case Power.hard:
                Debug.LogError("USED HARD");
                StartCoroutine(HardPower());
                break;
            case Power.rotate:
                Debug.LogError("USED ROTATE");
                StartCoroutine(RotatePower());
                break;
            case Power.collect:
                Debug.LogError("USED COLLECT");
                StartCoroutine(CollectPower());
                break;
            case Power.doubled:
                Debug.LogError("USED DOUBLED");
                StartCoroutine(DoubledPower());
                break;
        }
    }




    public void StartScript(Power powerToUse, GameObject iceBlockClicked) {
        current_Power = powerToUse;
        iceBlock = iceBlockClicked;
        StartPower();
    }

    public void CancelScript() {
        current_Power = Power.none;
    }



    private void IceBlockTapped() {

        
        iceBlock.GetComponent<IceBlockPhysics>().DestroyByTapped();
    }

    private IEnumerator DoubledPower() {
        if(iceBlock.GetComponent<IceBlockPhysics>().block != Block.stoneBlock) {
            list.Add(iceBlock);
            listOfColor.Add(iceBlock.GetComponent<SpriteRenderer>().color);
            iceBlock.GetComponent<SpriteRenderer>().color = color[1];
        }
        

        if (list.Count == 2) {

            list[0].GetComponent<SpriteRenderer>().color = color[0];
            list[1].GetComponent<SpriteRenderer>().color = color[1];
            list[0].GetComponent<IceBlockPhysics>().DestroyedByPower();
            list[1].GetComponent<IceBlockPhysics>().DestroyedByPower();
            GameUI.Instance.UsedPower(true);
            list.Clear();
        }

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator CollectPower() {

        if(iceBlock.tag == "fruit") {
            iceBlock.GetComponent<SpriteRenderer>().sprite = spriteForCollect;
            GameManager.Instance.CollectedFruit();
            GameUI.Instance.UsedPower(true);
        }
        else {
            GameUI.Instance.UsedPower(false);
        }


        yield return new WaitForEndOfFrame();
    }

    private IEnumerator RotatePower() {
        iceBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Vector3 rotation = iceBlock.transform.localEulerAngles;
        rotation.z += 90;

        iceBlock.transform.localEulerAngles = rotation;
        GameUI.Instance.UsedPower(true);
        iceBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        AchivmentOfRotate();
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator HardPower() {

        iceBlock.GetComponent<SpriteRenderer>().color = color[5];
        GameUI.Instance.UsedPower(true);
        iceBlock.GetComponent<IceBlockPhysics>().CanBeDestroyed(false);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator CopyPower() {
        IceBlockPhysics blockPhysics = iceBlock.GetComponent<IceBlockPhysics>();

        if(blockPhysics.block == Block.fruitBlock || blockPhysics.block == Block.powerIceBlock) {
            GameUI.Instance.UsedPower(false);
            yield break;
        }

        GameObject cloneOfTargetedObject = Instantiate(blockPhysics.gameObject);
        cloneOfTargetedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        rbType = blockPhysics.RigidbodyGetType();
        listOfColor.Add(blockPhysics.gameObject.GetComponent<SpriteRenderer>().color);

        blockPhysics.gameObject.GetComponent<SpriteRenderer>().color = color[1];
        //GetCollider
        var collider2d = cloneOfTargetedObject.GetComponent<Collider2D>();
        collider2d.isTrigger = true;
        BoxColliderScale.ScaleCollider(collider2d, true);

        SpriteRenderer spriteClone = cloneOfTargetedObject.GetComponent<SpriteRenderer>();
        spriteClone.sortingOrder = 15;


        while (true) {

            if (playerController.buttonUp) {
                uncheckPower = false;

                if (!colide) {
                    spriteClone.color = listOfColor[0];
                    spriteClone.sortingOrder = 10;

                    GameUI.Instance.UsedPower(true);
                    collider2d.isTrigger = false;
                    BoxColliderScale.ScaleCollider(collider2d, false);
                    cloneOfTargetedObject.GetComponent<IceBlockPhysics>().RigidbodyChange(rbType);
                }
                else {
                    Destroy(cloneOfTargetedObject.gameObject);
                    GameUI.Instance.UsedPower(false);
                }
                
                blockPhysics.gameObject.GetComponent<SpriteRenderer>().color = listOfColor[0];
                uncheckPower = true;
                listOfColor.Clear();
                break;
            }

            CheckColision(spriteClone);
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cloneOfTargetedObject.transform.position = position;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SwapPower() {
        if (list.Count == 1 && list[0].transform.position == iceBlock.transform.position) {
            Debug.Log("Touched The Same Block");
        }
        else {
            list.Add(iceBlock);
            listOfColor.Add(iceBlock.GetComponent<SpriteRenderer>().color);
            iceBlock.GetComponent<SpriteRenderer>().color = color[1];
        }

        if(list.Count == 2) {

            Vector3 position1 =list[0].transform.position;
            list[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            list[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


            list[0].transform.position = list[1].transform.position;
            list[1].transform.position = position1;
            list[0].GetComponent<SpriteRenderer>().color = listOfColor[0];
            list[1].GetComponent<SpriteRenderer>().color = listOfColor[1];


            list[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            list[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            list.Clear();
            GameUI.Instance.UsedPower(true);
            listOfColor.Clear();
        }

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FreezePower() {
        IceBlockPhysics blockPhysics = iceBlock.GetComponent<IceBlockPhysics>();
        blockPhysics.GetRigidbody().constraints = RigidbodyConstraints2D.FreezeAll;
        blockPhysics.gameObject.GetComponent<SpriteRenderer>().color = color[4];
        GameUI.Instance.UsedPower(true);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator MovePower() {
        colide = false;
        IceBlockPhysics blockPhysics = iceBlock.GetComponent<IceBlockPhysics>();
        rbType = blockPhysics.RigidbodyGetType();
        blockPhysics.RigidbodyChange(RigidbodyType2D.Static);

        GameObject cloneOfTargetedObject = Instantiate(blockPhysics.gameObject);

        listOfColor.Add(blockPhysics.gameObject.GetComponent<SpriteRenderer>().color);
        blockPhysics.gameObject.GetComponent<SpriteRenderer>().color = color[1];
        //GetCollider
        var collider2d = cloneOfTargetedObject.GetComponent<Collider2D>();
        collider2d.isTrigger = true;
        BoxColliderScale.ScaleCollider(collider2d, true);
        SpriteRenderer spriteClone = cloneOfTargetedObject.GetComponent<SpriteRenderer>();
        spriteClone.sortingOrder = 15;

        while (true) {

            if (playerController.buttonUp) {
                uncheckPower = false;
                if (!colide) {
                    blockPhysics.transform.position = cloneOfTargetedObject.transform.position;
                    GameUI.Instance.UsedPower(true);
                }
                else {
                    GameUI.Instance.UsedPower(false);
                }
                Destroy(cloneOfTargetedObject.gameObject);
                blockPhysics.gameObject.GetComponent<SpriteRenderer>().color = listOfColor[0];
                blockPhysics.RigidbodyChange(rbType);
                listOfColor.Clear();
                uncheckPower = true;
                break;
            }
            
            CheckColision(spriteClone);
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cloneOfTargetedObject.transform.position = position;
            yield return new WaitForEndOfFrame();
        }
    }


    private bool CheckColision(SpriteRenderer sprite) {

        if (colide) {
            sprite.color = color[3];
        }
        else {
            sprite.color = color[2];
        }


        return colide;
    }

    private void AchivmentOfRotate() {

        int rotate = PlayerPrefs.GetInt("UseRotate10Times", 0);
        rotate++;
        PlayerPrefs.SetInt("UseRotate10Times", rotate);
        if(rotate >= 10) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_pirouette);
        }
    }
}
