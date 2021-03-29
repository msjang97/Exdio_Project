using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{   
    private Animator animator;
    private Vector3 myPosition;
    public Bar bar;

    private bool isSelected;
    private float destroyedTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myPosition = this.transform.position;
    }

    // Update is called once per frame 
    void Update()
    {
        if (bar.P_isStoped == true)
        {
            SelectChoice();
            AnimationController();
        }       
    }

    private void SelectChoice()
    {
        if (bar.P_BarPosition.y < myPosition.y + 0.75 && bar.P_BarPosition.y > myPosition.y - 0.75)
        {
            bar.gameObject.transform.position = Vector3.Lerp(transform.position, myPosition, 0.01f);
            isSelected = true;
            animator.SetBool("IsSelected", true);
            ChoiceManager.P_instance.P_selectedNum = name;
        }
    }

    private void AnimationController()
    {
        if (isSelected == false)
        {
            animator.SetBool("havetoDestroy", true);
            Destroy(this.gameObject, destroyedTime);
        }          
    }
}
