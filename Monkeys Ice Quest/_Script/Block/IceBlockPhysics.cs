using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockPhysics : MonoBehaviour {

    public Block block = Block.standardIceBlock;

    private Rigidbody2D rb2D;
    public float basicStrenghtHeight = 4f;
    public float basicStrenghtWidth = 4f;
    private float strenghtHight;
    private float strenghtWidth;

    private Vector2 velocityIgnore;
    private Vector2 velocity;
    

    public bool canBeDestroyed = true;

    private void Awake() {

        rb2D = GetComponent<Rigidbody2D>();
        velocityIgnore = rb2D.velocity;

        strenghtHight = this.transform.localScale.y * basicStrenghtHeight;
        strenghtWidth = this.transform.localScale.x * basicStrenghtWidth;

        if(block == Block.stoneBlock) {
            canBeDestroyed = false;
        }
    }
    
    private void FixedUpdate() {

        velocity = rb2D.velocity;
    }
    /*
    private void OnCollisionStay2D(Collision2D collision) {
        
        Debug.LogWarning(collision.gameObject.name);

        foreach (ContactPoint2D contact in collision.contacts) {
            print(contact.collider.name + " hit " + contact.otherCollider.name);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }*/
    /*
    private void OnCollisionExit2D(Collision2D collision) {
        Debug.LogError(collision.gameObject.name + "EXIT");

        velocityIgnore = Vector2.zero; //rb2D.velocity;

    }*/

    private void OnCollisionEnter2D(Collision2D collision) {

        float impactStrenghtHeight;
        float impactStrenghtWidth;

        bool rotation = CalculateRotation();
        if (rotation) {
            // Horizontal
             impactStrenghtHeight = (velocity.y - velocityIgnore.y)+ strenghtHight;
             impactStrenghtWidth = (velocity.x - velocityIgnore.x) + strenghtWidth;
        }
        else {
            //Vertical 90 degresse
            impactStrenghtHeight = velocity.y + strenghtWidth;
            impactStrenghtWidth = velocity.x + strenghtHight;
        }

        CheckDestroy(impactStrenghtHeight, impactStrenghtWidth);


        if (block == Block.stoneBlock && (impactStrenghtHeight < 0 || impactStrenghtWidth < 0)) {
            IceBlockPhysics blocks = collision.gameObject.GetComponent<IceBlockPhysics>();
            if(blocks != null && blocks.block != Block.stoneBlock && blocks.canBeDestroyed) {
                float strenghtH = blocks.strenghtHight;
                float strenghtW = blocks.strenghtWidth;

                if(rotation && (strenghtH < strenghtHight || strenghtW < strenghtWidth)) {
                    blocks.DestroyByFalling();
                }
                else if(!rotation && (strenghtH < strenghtWidth || strenghtW < strenghtHight)) {
                    blocks.DestroyByFalling();
                }
                else {
                    Vector2 vel = velocity - velocityIgnore;
                    blocks.CheckColision(vel);
                }


            }
        }

    }

    public void CheckColision(Vector2 veloc) {

        float impactStrenghtHeight;
        float impactStrenghtWidth;

        Debug.Log(velocity + "    " + velocityIgnore + "     " + strenghtHight + "      " + strenghtWidth);

        bool rotation = CalculateRotation();
        if (rotation) {
            // Horizontal
            impactStrenghtHeight = (veloc.y - velocityIgnore.y) + strenghtHight;
            impactStrenghtWidth = (veloc.x - velocityIgnore.x) + strenghtWidth;
        }
        else {
            //Vertical 90 degresse
            impactStrenghtHeight = veloc.y + strenghtWidth;
            impactStrenghtWidth = veloc.x + strenghtHight;
        }

        CheckDestroy(impactStrenghtHeight, impactStrenghtWidth);

    }

    public void CheckDestroy(float impactHeight, float impactWidth) {

        if ((impactHeight < 0 || impactWidth < 0) && canBeDestroyed) {
            canBeDestroyed = false;
            DestroyByFalling();
        }
        else {
            Debug.Log("DONT DESTROY");
        }
    }


    private bool CalculateRotation() {
        float zRotation = this.transform.localEulerAngles.z;
        zRotation = zRotation / 90;
        zRotation = Mathf.Round(zRotation);

        if(zRotation % 2 == 1) {
            return false;
        }
        else {
            return true;
        }
    }

    public void RigidbodyChange(RigidbodyType2D type) {
        rb2D.bodyType = type;
    }

    public RigidbodyType2D RigidbodyGetType() {
        return rb2D.bodyType;
    }

    public Rigidbody2D GetRigidbody() {
        return rb2D;
    }




    private void OnTriggerEnter2D(Collider2D collision) {
        BlockBehavior.Instance.Colide = true;

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.name != "CotnrollScript")
            BlockBehavior.Instance.Colide = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision) {
        BlockBehavior.Instance.Colide = false;

    }


    public void CanBeDestroyed(bool value) {
        canBeDestroyed = value;
    }

    public void DestroyByTapped() {
        

        switch (block) {
            case Block.standardIceBlock:
                GameManager.Instance.ChangeMovesValue(-1);
                Instantiate(Resources.Load("Tap_particles/Tap_Blocks_audio"), this.transform.position, this.transform.rotation);
                PlayerController.Instance.YouUseMove();
                break;
            case Block.fruitBlock:
                GameManager.Instance.ChangeFruitsValue(-1);
                Instantiate(Resources.Load("Tap_particles/Tap_friut_audio"), this.transform.position, this.transform.rotation);
                PlayerController.Instance.YouUseMove();
                break;
            case Block.woodBlock:
                GameManager.Instance.ChangeMovesValue(-1);
                Instantiate(Resources.Load("Tap_particles/Wood_Blocks_tap_audio"), this.transform.position, this.transform.rotation);
                PlayerController.Instance.YouUseMove();
                break;
            case Block.stoneBlock:
                break;
            case Block.powerIceBlock:
                Instantiate(Resources.Load("Tap_particles/Tap_Blocks_audio"), this.transform.position, this.transform.rotation);
                GetComponent<PowerFreezeInBlock>().GetFrozzenPower();
                PlayerController.Instance.YouUseMove();
                break;
            
        }
        if(block != Block.stoneBlock)
            Destroy(this.gameObject);
    }

    public void DestroyedByPower() {
        switch (block) {
            case Block.standardIceBlock:
                Instantiate(Resources.Load("Tap_particles/Tap_Blocks_audio"), this.transform.position, this.transform.rotation);
                break;
            case Block.fruitBlock:
                GameManager.Instance.ChangeFruitsValue(-1);
                Instantiate(Resources.Load("Tap_particles/Tap_friut_audio"), this.transform.position, this.transform.rotation);
                break;
            case Block.woodBlock:
                Instantiate(Resources.Load("Tap_particles/Wood_Blocks_tap_audio"), this.transform.position, this.transform.rotation);
                break;
            case Block.powerIceBlock:
                Instantiate(Resources.Load("Tap_particles/Tap_Blocks_audio"), this.transform.position, this.transform.rotation);
                GetComponent<PowerFreezeInBlock>().GetFrozzenPower();
                break;

        }
        if (block != Block.stoneBlock)
            Destroy(this.gameObject);
    }


    public void DestroyByFalling() {
        
        switch (block) {
            case Block.standardIceBlock:
                GameManager.Instance.ChangeMistakesValue(-1);
                Instantiate(Resources.Load("Crash_particles/Crash_Blocks_audio"), this.transform.position, this.transform.rotation);
                break;
            case Block.fruitBlock:
                GameManager.Instance.ChangeMistakesValue(-1);
                GameUI.Instance.EndScreen(false);
                //GameManager.Instance.ChangeFruitsValue(-1);
                Instantiate(Resources.Load("Crash_particles/Crash_Blocks_audio"), this.transform.position, this.transform.rotation);
                break;
            case Block.woodBlock:
                Debug.LogError("DONT DESTROY");
                break;
            case Block.stoneBlock:
                Debug.LogError("DONT DESTROY");
                break;
            case Block.powerIceBlock:
                GameManager.Instance.ChangeMistakesValue(-1);
                Instantiate(Resources.Load("Crash_particles/Crash_Blocks_audio"), this.transform.position, this.transform.rotation);
                break;


        }

        Destroy(this.gameObject);
    }

    
    
}
