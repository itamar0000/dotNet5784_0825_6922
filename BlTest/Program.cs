﻿namespace BlTest;
using BL;
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
