using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GhoulBaseStats
{
    [Serializable]
    public class ColorGene
    {
        private Vector3 color;
        //stiffness genes are for determining how strong some genes grow to become.
        // if you're breeding blue ghoul for a long time and you keep breeding them blue
        //blue's stiffness will grow, making the outlier on the bell smaller and making the gene harder to bred out.
        //
        //
        // breeding with a red ghoul will be fine from time to time as long as the stiffness stays up
        //
        //
        //
        // the stiffness of a gene will be cancelled out by the other ghoul stiffness.
        //
        //(EXAMPLES:)
        // Let's say a 10 stiffness is high, well a 10 stiffness of blue, against a 10 stiffness of orange cancels out since they're opposite colors and we're left with a 0 stiffness of a mixed color with some random
        // this could also lean into black/white territory. This is easier to occur 
        // 
        // 10 blue breed with 10 green, this would lean into blue territory, strengthing that, while retaining some yellow, making it close to black


        private float stiffness;

        public ColorGene(Vector3 _color, float _stiffness)
        {
            color = _color;
            stiffness = _stiffness;
        }

        public Vector3 Color
        {
            //alpha could be a rare state, making them slight transparent
            get => color;
        }

        public float Stiffness
        {
            get => stiffness;
        }
    }

    [Serializable]
    public class FloatGene
    {
        private float value;
        private float stiffness;

        public FloatGene(float _value, float _stiffness)
        {
            value = _value;
            stiffness = _stiffness;
        }

        public float Value
        {
            get => value;
        }

        public float Stiffness
        {
            get => stiffness;
        }
    }

    
    private FloatGene jowlCenterMostLeft;
    private FloatGene jowlCenterMostRight;
    private FloatGene jowlCenterMiddleLeft;
    private FloatGene jowlCenterMiddleRight;
    private FloatGene jowlOuterMiddleLeft;
    private FloatGene jowlOuterMiddleRight;
    private FloatGene jowlOuterLeft;
    private FloatGene jowlOuterRight;
    private FloatGene headHeight;
    private FloatGene headWidth;


    private ColorGene primaryColor;
    private ColorGene secondaryColor;
    private ColorGene thirdColor;

    private FloatGene alphaGene; //TODO: apply alpha gene on load, not on color picks

    private FloatGene lifeSpan;
    private FloatGene maxSize;
    private FloatGene bodySize; //TODO: body size decrease speed in race

    //certain stats like speed & jumpHeight can be trained to be higher
    //IMPORTANT: while breeding, these base stats can lean into higher outliers, thus helping to breed stronger ghoul
    private FloatGene baseSpeed;
    private FloatGene baseJumpHeight;

    private float trainedSpeed;
    private float trainedJumpHeight;

    
    //certain skills like body length, makes the ghoul longer and has more legs. They evolve their ability to climb via body, so if not long enough, can't climb for as long
    //these stats will have a much higher tendancy 
    private FloatGene bodyLength;
    private FloatGene wingSize;
    private FloatGene legLength; //TODO: leg length effects speed in a race


    //This is a list of favorite behaviours, the float being the chance to do the behaviour 0-100
    //TODO: when new behaviours get added, they can be added to the dictionary with a 0 percent chance of doing them, with high stiffness
    private Dictionary<string, FloatGene> behaviourFavoritism;

    private int currentAge; //age is recorded in minutes



    /*TODO:
     
     private FloatGene pattern; 
     need to figure out best way to deal with this longterm (do I want the patterns to try and blend? do I want them to be stiff and a chance to be random? If that way, do I increase ,multiple sizes size on a shader?
     
     */
    //this could be a long term support though. Start with a base and release new patterns and how they breed later.






    /*IMPORTANT:
    //if new genes are released later, we should make the legacy Ghoul without the genes very high by default. 
    //we can do this by simple doing this 
    .
    .
    v
    private FloatGene newGene = new FloatGene(newValue, 100f);
    
    by doing it this way, legacy Ghoul will have that gene defaulted, but our ChatBreeder.cs will be updated to now take that stat into account
    
    */

    /*
     
     TODO: return these to JUST GETTERS after things are tested
     
     
     
     public FloatGene JowlCenterMostLeft => jowlCenterMostLeft;

    public FloatGene JowlCenterMostRight => jowlCenterMostRight;

    public FloatGene JowlCenterMiddleLeft => jowlCenterMiddleLeft;

    public FloatGene JowlCenterMiddleRight => jowlCenterMiddleRight;

    public FloatGene JowlOuterMiddleLeft => jowlOuterMiddleLeft;

    public FloatGene JowlOuterMiddleRight => jowlOuterMiddleRight;

    public FloatGene JowlOuterLeft => jowlOuterLeft;

    public FloatGene JowlOuterRight => jowlOuterRight;

    public FloatGene HeadHeight => headHeight;

    public FloatGene HeadWidth => headWidth;*/
    public FloatGene JowlCenterMostLeft
    {
        get => jowlCenterMostLeft;
        set => jowlCenterMostLeft = value;
    }

    public FloatGene JowlCenterMostRight
    {
        get => jowlCenterMostRight;
        set => jowlCenterMostRight = value;
    }

    public FloatGene JowlCenterMiddleLeft
    {
        get => jowlCenterMiddleLeft;
        set => jowlCenterMiddleLeft = value;
    }

    public FloatGene JowlCenterMiddleRight
    {
        get => jowlCenterMiddleRight;
        set => jowlCenterMiddleRight = value;
    }

    public FloatGene JowlOuterMiddleLeft
    {
        get => jowlOuterMiddleLeft;
        set => jowlOuterMiddleLeft = value;
    }

    public FloatGene JowlOuterMiddleRight
    {
        get => jowlOuterMiddleRight;
        set => jowlOuterMiddleRight = value;
    }

    public FloatGene JowlOuterLeft
    {
        get => jowlOuterLeft;
        set => jowlOuterLeft = value;
    }

    public FloatGene JowlOuterRight
    {
        get => jowlOuterRight;
        set => jowlOuterRight = value;
    }

    public FloatGene HeadHeight
    {
        get => headHeight;
        set => headHeight = value;
    }

    public FloatGene HeadWidth
    {
        get => headWidth;
        set => headWidth = value;
    }

    public float TrainedSpeed
    {
        get => trainedSpeed;
        set => trainedSpeed = value;
    }

    public float TrainedJumpHeight
    {
        get => trainedJumpHeight;
        set => trainedJumpHeight = value;
    }
    
    

    public GhoulBaseStats(GhoulHead head)
    {
        jowlCenterMostLeft = new FloatGene(head.JowlCenterMostLeft, 10);
        jowlCenterMostRight = new FloatGene(head.JowlCenterMostRight, 10);
        jowlCenterMiddleLeft = new FloatGene(head.JowlCenterMiddleLeft, 10);
        jowlCenterMiddleRight = new FloatGene(head.JowlCenterMiddleRight, 10);
        jowlOuterMiddleLeft = new FloatGene(head.JowlOuterMiddleLeft, 10);
        jowlOuterMiddleRight = new FloatGene(head.JowlOuterMiddleRight, 10);
        jowlOuterLeft = new FloatGene(head.JowlOuterLeft, 10);
        jowlOuterRight = new FloatGene(head.JowlOuterRight, 10);
        headHeight = new FloatGene(head.HeadHeight, 10);
        headWidth = new FloatGene(head.HeadWidth, 10);
    }

    //TODO prior todo about stats ends here
    
    public GhoulBaseStats(Color col)
    {
        primaryColor = new ColorGene(new Vector3(col.r, col.g, col.b),10);
        secondaryColor = primaryColor;
        thirdColor = primaryColor;

    }

    public GhoulBaseStats(GhoulBaseStats parent1, GhoulBaseStats parent2)
    {
        jowlOuterLeft = MixFloatGeneValues(parent1.JowlOuterLeft, parent2.JowlOuterLeft);
        jowlOuterRight = MixFloatGeneValues(parent1.JowlOuterRight, parent2.JowlOuterRight);
        headHeight = MixFloatGeneValues(parent1.HeadHeight, parent2.HeadHeight);
        headWidth = MixFloatGeneValues(parent1.HeadWidth, parent2.HeadWidth);
        jowlOuterMiddleRight = MixFloatGeneValues(parent1.JowlOuterMiddleRight, parent2.JowlOuterMiddleRight);
        jowlOuterMiddleLeft = MixFloatGeneValues(parent1.JowlOuterMiddleLeft, parent2.JowlOuterMiddleLeft);
        jowlCenterMiddleRight = MixFloatGeneValues(parent1.JowlCenterMiddleRight, parent2.JowlCenterMiddleRight);
        jowlCenterMiddleLeft = MixFloatGeneValues(parent1.JowlCenterMiddleLeft, parent2.JowlCenterMiddleLeft);
        jowlCenterMostRight = MixFloatGeneValues(parent1.JowlCenterMostRight, parent2.JowlCenterMostRight);
        jowlCenterMostLeft = MixFloatGeneValues(parent1.JowlCenterMostLeft, parent2.JowlCenterMostLeft);

        /*
        primaryColor = MixColorGeneValues(parent1.primaryColor, parent2.primaryColor);
        secondaryColor = MixColorGeneValues(parent1.secondaryColor, parent2.secondaryColor);
        thirdColor = MixColorGeneValues(parent1.thirdColor, parent2.thirdColor);

        var newAlpha = MixFloatGeneValues(parent1.alphaGene.Value, parent2.alphaGene.Value,
            parent1.alphaGene.Stiffness, parent2.alphaGene.Stiffness);
        alphaGene = DetermineFloatStiffnessValue(newAlpha, parent1.alphaGene, parent2.alphaGene,.1f);


        var newLifeSpan = MixFloatGeneValues(
            parent1.lifeSpan.Value,
            parent2.lifeSpan.Value,
            parent1.lifeSpan.Stiffness,
            parent2.lifeSpan.Stiffness,
            GhoulStatVariables.LIFESPAN_INCREASERANGE, false);

        if (newLifeSpan < GhoulStatVariables.STARTING_AGE_MINIMUM)
            newLifeSpan = GhoulStatVariables.STARTING_AGE_MINIMUM;
        
        lifeSpan = DetermineFloatStiffnessValue(
            newLifeSpan,
            parent1.lifeSpan, 
            parent2.lifeSpan,GhoulStatVariables.LIFESPAN_INCREASERANGE);
            */

    }

    /*private ColorGene MixColorGeneValues(ColorGene col1, ColorGene col2)
    {
        var newX = MixFloatGeneValues(col1.Color.x, col2.Color.x, col1.Stiffness, col2.Stiffness);
        var newY = MixFloatGeneValues(col1.Color.y, col2.Color.y, col1.Stiffness, col2.Stiffness);
        var newZ = MixFloatGeneValues(col1.Color.z, col2.Color.z, col1.Stiffness, col2.Stiffness);

        var newColor = new Vector3(newX, newY, newZ);

        ColorGene newColorGene = DetermineNewColorStiffnessValues(newColor, col1, col2);
        return newColorGene;
    }*/

    private FloatGene MixFloatGeneValues(FloatGene val1, FloatGene val2, float intervalVal = 1, bool canCapValue = true, float capValue = 1)
    {
        var val1Portion = val1.Stiffness / (val1.Stiffness + val2.Stiffness);
        var val2Portion = val2.Stiffness / (val1.Stiffness + val2.Stiffness);
        
        var newValue = (val1.Value * val1Portion) + (val2.Value * val2Portion);

        float xDiffFrom1 = (val1.Value - newValue); //* stiffnessVal1 / (stiffnessVal1 + stiffnessVal2) ;
        float xDiffFrom2 = (val2.Value - newValue); //* stiffnessVal2 / (stiffnessVal1 + stiffnessVal2);

        if (xDiffFrom1 > xDiffFrom2)
            newValue += Random.Range(xDiffFrom2* 1f - .01f*intervalVal, xDiffFrom1* 1f + .01f*intervalVal);
        else
            newValue += Random.Range(xDiffFrom1* 1f - .01f*intervalVal, xDiffFrom2* 1f + .01f*intervalVal);

        if (newValue < 0)
            newValue = 0;
        else if (canCapValue && newValue > capValue)
            newValue = capValue;
        
        
        return DetermineFloatStiffnessValue(newValue,val1,val2);
    }

    private ColorGene DetermineNewColorStiffnessValues(Vector3 newColor, ColorGene col1, ColorGene col2)
    {
        float diffFromCol1 = (Mathf.Abs(col1.Color.x - newColor.x) + Mathf.Abs(col1.Color.y - newColor.y) +
                              Mathf.Abs(col1.Color.z - newColor.z))/3;
        float diffFromCol2 = (Mathf.Abs(col2.Color.x - newColor.x) + Mathf.Abs(col2.Color.y - newColor.y) +
                              Mathf.Abs(col2.Color.z - newColor.z))/3;

        float stiff1;
        float stiff2;
        
        if (diffFromCol1 <= .1f)
            stiff1 = col1.Stiffness + 1 * Random.Range(.4f,1.2f); //increase by slightly random amount
        else
            stiff1 = col1.Stiffness - (col1.Stiffness * diffFromCol1);

        if (diffFromCol2<= .1f)
            stiff2 = col2.Stiffness + 1 * Random.Range(.4f,1.2f); 
        else
            stiff2 = col2.Stiffness - (col2.Stiffness * diffFromCol2);

        float newStiffness = (stiff1 + stiff2) / 2;

        if (newStiffness < 1)
            newStiffness = 1;
        if (newStiffness > 100)
            newStiffness = 100;
        
        return new ColorGene(newColor,newStiffness);
    }
    
    private FloatGene DetermineFloatStiffnessValue(float newFloat, FloatGene gene1, FloatGene gene2)
    {
        float diffFromGene1 = Mathf.Abs(gene1.Value - newFloat);
        float diffFromGene2 = Mathf.Abs(gene2.Value - newFloat);

        float stiff1;
        float stiff2;
        
        //if (diffFromGene1 <= stiffnessIncreaseRange)
            stiff1 = gene1.Stiffness + 1 * Random.Range(.4f,1.2f); //increase by slightly random amount
        //else
            //stiff1 = gene1.Stiffness - (gene1.Stiffness * diffFromGene1/stiffnessIncreaseRange);

        //if (diffFromGene2<= stiffnessIncreaseRange)
            stiff2 = gene2.Stiffness + 1 * Random.Range(.4f,1.2f); 
        //else
            //stiff2 = gene2.Stiffness - (gene2.Stiffness * diffFromGene2/stiffnessIncreaseRange);

        float newStiffness = (stiff1 + stiff2) / 2;

        if (newStiffness < 1)
            newStiffness = 1;
        if (newStiffness > 100)
            newStiffness = 100;
        
        return new FloatGene(newFloat,newStiffness);
    }

    public void CreateNewRandomStats(int seed = 0)
    {
        if (seed != 0)
            Random.InitState(seed);
        
        float transparencyChance = Random.Range(0f, 1f);
        alphaGene = new FloatGene(

            Random.Range(
                ((transparencyChance >= GhoulStatVariables.ALPHA_CHANCE_THRESHOLD)
                    ? GhoulStatVariables.ALPHA_STARTING_LIMIT
                    : 1), 1f), //we are giving a approx: 2% (threshold) to get alpha down 5% (limit).
            Random.Range(1, 10)
        );

        primaryColor = new ColorGene(new Vector3(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)),
            Random.Range(1f, 10f));

        secondaryColor = new ColorGene(new Vector3(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)),
            Random.Range(1f, 10f));

        thirdColor = new ColorGene(new Vector3(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)),
            Random.Range(1f, 10f));

        currentAge = 0;

        lifeSpan = new FloatGene(
            GhoulStatVariables.STARTING_AGE_AVERAGE + Random.Range(0, GhoulStatVariables.STARTING_AGE_VARIANCE),
            Random.Range(1f, 10f));

    }
}
