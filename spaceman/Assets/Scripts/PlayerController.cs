using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //Variables del movimiento del personaje 
    public float jumpForce = 6f;
    public float runningSpeed = 2f;

    private Rigidbody2D rigidbody;
    Animator animator;
    Vector3 startPosition;

    public LayerMask whatIsGround;

    private const string STATE_ALIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";


    private int healthPoints, manaPoints;
    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 200, MAX_MANA = 30, MIN_HEALTH = 10, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        startPosition = this.transform.position;
    }

    public void StartGame(){
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("RestartPosition", 0.1f);
    }

    void RestartPosition(){
        this.transform.position = startPosition;
        this.rigidbody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("MainCamera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)
            if(Input.GetButtonDown("Jump")){
                Jump(false);
            }
            if(Input.GetButtonDown("Superjump")){
                Jump(true);
            }
        animator.SetBool(STATE_ON_THE_GROUND,IsTouchingTheGround());
       
    }

    private void FixedUpdate() {
        if(GameManager.shareInstance.currentGameState == GameState.inGame){
            if (rigidbody.velocity.x < runningSpeed){
                rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y);
            }
        }else{
            //if we dont arn't in game, we dont move
            rigidbody.velocity = new Vector2(0,rigidbody.velocity.y);
        }
    }
    

    void Jump(bool isSuperJump ){
        float jumpForceFactor = jumpForce;
        if (isSuperJump && manaPoints >= SUPERJUMP_COST){
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if(GameManager.shareInstance.currentGameState == GameState.inGame){
            if(IsTouchingTheGround()){
                rigidbody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    //Method to check if the player is on the ground
    bool IsTouchingTheGround() {
        if(Physics2D.Raycast(this.transform.position,Vector2.down,1.5f,whatIsGround)){
            //programming logic of contact with the ground
            
            return true;
        }else{
            
            return false;
        }
    }

    public void Die(){

        float traveledDistance = GetTraveledDistance();
        float previusMaxDistance = PlayerPrefs.GetFloat("maxscore",0);
        if(traveledDistance > previusMaxDistance){
            PlayerPrefs.SetFloat("maxscore", traveledDistance);
        }
        animator.SetBool(STATE_ALIVE, false);
        GameManager.shareInstance.GameOver();
    }
  

    public void CollectHealth(int points){
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH){
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points){
        this.manaPoints += points;
        if(this.manaPoints >= MAX_MANA){
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth(){
        return this.healthPoints;
    }

    public int GetMana(){
        return this.manaPoints;
    }

    public float GetTraveledDistance(){
        return this.transform.position.x - startPosition.x;
    }
}
