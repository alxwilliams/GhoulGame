using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulHead : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Cloth cloth;
    [Range(0f,1f)] [SerializeField] private float jowlCenterMostLeft;
    [Range(0f,1f)] [SerializeField] private float jowlCenterMostRight;
    [Range(0f,1f)] [SerializeField] private float jowlCenterMiddleLeft;
    [Range(0f,1f)] [SerializeField] private float jowlCenterMiddleRight;
    [Range(0f,1f)] [SerializeField] private float jowlOuterMiddleLeft;
    [Range(0f,1f)] [SerializeField] private float jowlOuterMiddleRight;
    [Range(0f,1f)] [SerializeField] private float jowlOuterLeft;
    [Range(0f,1f)] [SerializeField] private float jowlOuterRight;
    [Range(0.01f,1f)] [SerializeField] private float headHeight;
    [Range(0.01f,1f)] [SerializeField] private float headWidth;
    
    [Range(0f,1f)] [SerializeField] private float headBumpMiddleWidth;
    [Range(0f,1f)] [SerializeField] private float headBumpMiddleHeight;
    [Range(0f,1f)] [SerializeField] private float headBumpLeftWidth;
    [Range(0f,1f)] [SerializeField] private float headBumpLeftHeight;
    [Range(0f,1f)] [SerializeField] private float headBumpRightWidth;
    [Range(0f,1f)] [SerializeField] private float headBumpRightHeight;
    
    
    [Range(0f,1f)] [SerializeField] private float leftSideOfFaceTop;
    [Range(0f,1f)] [SerializeField] private float leftSideOfFaceBottom;
    [Range(0f,1f)] [SerializeField] private float rightSideOfFaceTop;
    [Range(0f,1f)] [SerializeField] private float rightSideOfFaceBottom;


    public float JowlCenterMostLeft
    {
        get => jowlCenterMostLeft;
        set => jowlCenterMostLeft = value;
    }

    public float JowlCenterMostRight
    {
        get => jowlCenterMostRight;
        set => jowlCenterMostRight = value;
    }

    public float JowlCenterMiddleLeft
    {
        get => jowlCenterMiddleLeft;
        set => jowlCenterMiddleLeft = value;
    }

    public float JowlCenterMiddleRight
    {
        get => jowlCenterMiddleRight;
        set => jowlCenterMiddleRight = value;
    }

    public float JowlOuterMiddleLeft
    {
        get => jowlOuterMiddleLeft;
        set => jowlOuterMiddleLeft = value;
    }

    public float JowlOuterMiddleRight
    {
        get => jowlOuterMiddleRight;
        set => jowlOuterMiddleRight = value;
    }

    public float JowlOuterLeft
    {
        get => jowlOuterLeft;
        set => jowlOuterLeft = value;
    }

    public float JowlOuterRight
    {
        get => jowlOuterRight;
        set => jowlOuterRight = value;
    }

    public float HeadHeight
    {
        get => headHeight;
        set => headHeight = value;
    }

    public float HeadWidth
    {
        get => headWidth;
        set => headWidth = value;
    }


    void Start()
    {
        
        StartCoroutine(LoadProportions());
    }

    private void Update()
    {
        
    }

    public GhoulBaseStats MakeStatsFromHead()
    {
        GhoulBaseStats stats = new GhoulBaseStats(this);
        return stats;
    }
    
    
    public void LoadStatsIntoHead(GhoulBaseStats newStats)
    {
        jowlCenterMostLeft = newStats.JowlCenterMostLeft.Value;
        jowlCenterMostRight = newStats.JowlCenterMostRight.Value;
        jowlCenterMiddleLeft = newStats.JowlCenterMiddleLeft.Value;
        jowlCenterMiddleRight = newStats.JowlCenterMiddleRight.Value;
        jowlOuterMiddleLeft = newStats.JowlOuterMiddleLeft.Value;
        jowlOuterMiddleRight = newStats.JowlOuterMiddleRight.Value;
        jowlOuterLeft = newStats.JowlOuterLeft.Value;
        jowlOuterRight = newStats.JowlOuterRight.Value;
        headHeight = newStats.HeadHeight.Value;
        headWidth = newStats.HeadWidth.Value;
        
        LoadProportions();
    }
    
    IEnumerator LoadProportions()
    {
        cloth.enabled = false;
        anim.SetFloat("Jowl Centermost Size Left", jowlCenterMostLeft);
        anim.SetFloat("Jowl Centermost Size Right", jowlCenterMostRight);
        anim.SetFloat("Jowl Centermiddle Size Left", jowlCenterMiddleLeft);
        anim.SetFloat("Jowl Centermiddle Size Right", jowlCenterMiddleRight);
        anim.SetFloat("Jowl Outermiddle Size Left", jowlOuterMiddleLeft);
        anim.SetFloat("Jowl Outermiddle Size Right", jowlOuterMiddleRight);
        anim.SetFloat("Jowl Outer Size Left", jowlOuterLeft);
        anim.SetFloat("Jowl Outer Size Right", jowlOuterRight);
        anim.SetFloat("Head Height", headHeight);
        anim.SetFloat("Head Width", headWidth);

        yield return new WaitForEndOfFrame();
        
        cloth.enabled = true;
    }
}
