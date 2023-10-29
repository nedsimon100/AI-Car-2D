using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GeneticManager : MonoBehaviour
{
    public int generation = 0;
    public int agent = 0;
    public int population = 100;
    public GameObject car;
    private List<float[,]> weightList = new List<float[,]>();
    private List<float[,]> lastWeightList = new List<float[,]>();
    private List<float> fitness = new List<float>();
    private List<float> lastFitness = new List<float>();
    public float mutationRate = 0.2f;
    private float aveFitness = 0;
    [Header("time control")]
    public TextMeshProUGUI info;
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        CarController carController = car.GetComponent<CarController>();
        if (carController.dead)
        {
            carController.dead = false;
            carController.StartCoroutine("notMoving");
            weightList.Add(car.GetComponent<NNet>().weights);
            fitness.Add(car.GetComponent<CarController>().fitness);
            newAgent();

            carController.highspeed = 0;
            carController.avespeed = 0;
            carController.timeAlive = 0;
            carController.distTraveled = 0;
            carController.fitness = 0;
            if (agent != 0) 
            { 
                Debug.Log("Generation: " + generation + " -- Agent: " + agent + " -- Average Fitness: " + aveFitness + " -- Last Fitness " + fitness[fitness.Count - 1]);
                info.text = ("Generation: " + generation + " -- Agent: " + agent+ "\n" + "Average Fitness: " + Mathf.RoundToInt(aveFitness) + " -- Last Fitness " + Mathf.RoundToInt(fitness[fitness.Count - 1]));
            }
            else
            {
               
                info.text = ("Generation: " + generation +  "\n" + "ELITISM FITNESS: "+ Mathf.Max(lastFitness.ToArray()));
            }
           

            CalculateAverageFitness();
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void Start()
    {
        newAgent();
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void newAgent()
    {
        agent++;
        if (agent >= population)
        {
            CalculateAverageFitness();
            Debug.LogError("Average Fitness - "+aveFitness +"  --  Elitism Fitness - "+ Mathf.Max(lastFitness.ToArray()));
            agent = 0;
            generation++;
            lastWeightList.Clear();
            lastFitness.Clear();
            lastWeightList.AddRange(weightList);
            lastFitness.AddRange(fitness);
            weightList.Clear();
            fitness.Clear();
            Debug.Log("Next Generation");
        }

        car.GetComponent<NNet>().weights = (generation == 0) ? RandomiseWeights() : GenerateNewWeights(); 

        
        
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private float[,] RandomiseWeights()
    {
        float[,] workingWeights = new float[3, 100];

        for (int y = 0; y < 100; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                workingWeights[x, y] = Random.Range(-1f, 1f);
            }
        }

        return workingWeights;
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private float[,] GenerateNewWeights()
    {
        float totalFitness = 0f;
        foreach (float fit in lastFitness)
        {
            
            totalFitness += fit;
        }

        float[,] workingWeights = new float[3, 100];

        if (agent > population*0.9)
        {
            return RandomiseWeights();
        }
        else
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    float chosenGene = Random.Range(1, totalFitness - 1);
                    int chosenGeneIndex = 0;
                    for (int i = 0; i < lastFitness.Count; i++)
                    {
                        if (lastFitness[i] > 0)
                        {
                            chosenGene -= lastFitness[i];
                            if (chosenGene <= 0)
                            {
                                chosenGeneIndex = i;
                                i = lastFitness.Count + 10;
                            }
                        }
                    }
                    Debug.LogWarning("x: " + x + "  y: " + y + "  CG: " + chosenGeneIndex);
                    workingWeights[x, y] = lastWeightList[chosenGeneIndex][x, y];
                }
            }

            // Mutations
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (Random.Range(0, 100) < mutationRate)
                    {
                        workingWeights[x, y] += Random.Range(-0.3f, 0.3f);
                    }
                    workingWeights[x, y] = Mathf.Clamp(workingWeights[x, y], -1f, 1f);
                }
            }
        }

        if (agent == 0)
        {
            int bestIndex = lastFitness.IndexOf(Mathf.Max(lastFitness.ToArray()));
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    workingWeights[x, y] = lastWeightList[bestIndex][x, y];
                }
            }
        }

        return workingWeights;
    }
    //-------------------------------------------------------------------------------------------------------------------------------
    private void CalculateAverageFitness()
    {
        float totalFitness = 0f;
        foreach (float fit in fitness)
        {
            totalFitness += fit;
        }
        aveFitness = totalFitness / fitness.Count;
    }
}

