using System;

class Dog
{
    public string name;
    public int age;

    public void Bark()
    {
        Console.WriteLine(name + " says woof!");
    }
}

class Program
{
    static void Main()
    {
        // FIRST DOG
        Console.WriteLine("Enter first dog name:");
        string name1 = Console.ReadLine();

        Console.WriteLine("Enter first dog age:");
        int age1;
        while (!int.TryParse(Console.ReadLine(), out age1))
        {
            Console.WriteLine("Please enter a valid number:");
        }

        Dog dog1 = new Dog();
        dog1.name = name1;
        dog1.age = age1;

        // SECOND DOG
        Console.WriteLine("\nEnter second dog name:");
        string name2 = Console.ReadLine();

        Console.WriteLine("Enter second dog age:");
        int age2;
        while (!int.TryParse(Console.ReadLine(), out age2))
        {
            Console.WriteLine("Please enter a valid number:");
        }

        Dog dog2 = new Dog();
        dog2.name = name2;
        dog2.age = age2;

        // OUTPUT
        Console.WriteLine("\n--- Dogs Barking ---");
        dog1.Bark();
        dog2.Bark();
    }
}