﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
namespace BlImplementation;

class Graph
{
    private int V; // number of vertices
    private List<int>[] adj; // adjacency list

    public Graph(int V)
    {
        this.V = V;
        adj = new List<int>[V];
        for (int i = 0; i < V; i++)
        {
            adj[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w) { adj[v].Add(w); }

    private bool DFSUtil(int v, bool[] visited, bool[] recStack)
    {
        visited[v] = true;
        recStack[v] = true;

        foreach (var neighbor in adj[v])
        {
            if (!visited[neighbor])
            {
                if (DFSUtil(neighbor, visited, recStack))
                {
                    return true; // Cross edge found in the subtree rooted at 'neighbor'
                }
            }
            else if (recStack[neighbor])
            {
                return true; // Cross edge found between 'v' and 'neighbor'
            }
        }

        recStack[v] = false;
        return false;
    }

    // Function to detect cross edges in the graph
    public bool DetectCrossEdges()
    {
        bool[] visited = new bool[V];
        bool[] recStack = new bool[V];

        for (int i = 0; i < V; ++i)
        {
            if (!visited[i])
            {
                if (DFSUtil(i, visited, recStack))
                {
                    return true; // Cross edge found
                }
            }
        }

        return false; // No cross edges found
    }
    private void TopologicalSortUtil(int v, bool[] visited, Stack<int> stack)
    {
        visited[v] = true;

        foreach (var neighbor in adj[v])
        {
            if (!visited[neighbor])
            {
                TopologicalSortUtil(neighbor, visited, stack);
            }
        }

        // Push current vertex to stack which stores result
        stack.Push(v);
    }

    // Function to perform topological sort
    public int[] TopologicalSort()
    {
        Stack<int> stack = new Stack<int>();
        bool[] visited = new bool[V];

        // Mark all the vertices as not visited
        for (int i = 0; i < V; i++)
        {
            visited[i] = false;
        }

        // Call the recursive helper function to store Topological Sort starting from all vertices one by one
        for (int i = 0; i < V; i++)
        {
            if (!visited[i])
            {
                TopologicalSortUtil(i, visited, stack);
            }
        }

        // Convert stack to array
        int[] result = stack.ToArray();

        return result;
    }
}

