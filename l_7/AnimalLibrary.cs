using System;
using System.Xml.Serialization;

namespace AnimalLibrary
{
    public class CommentAtt : Attribute
    {
        public string Comment { get; set; }
        public CommentAtt(string comment) { Comment = comment; }
    }
    public enum AnimalClassification
    {
        Herbivores,
        Carnivores,
        Omnivores
    }
    public enum FavouriteFood
    {
        Meat,
        Plants,
        Everything
    }
    [CommentAtt("Animal")]
    public class Animal
    {
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }
        public AnimalClassification WhatAnimal;
        public Animal()
        {
            Country = "";
            HideFromOtherAnimals = false;
            Name = "";
            WhatAnimal = AnimalClassification.Omnivores;
        }
        public Animal(string country, bool hide, string name, AnimalClassification what)
        {
            Country = country;
            HideFromOtherAnimals = hide;
            Name = name;
            WhatAnimal = what;
        }
        public void Deconstruct() { }
        public AnimalClassification GetClassification() { return WhatAnimal; }
        public virtual FavouriteFood GetFavouriteFood()
        {
            if (WhatAnimal == AnimalClassification.Carnivores) { return FavouriteFood.Meat; }
            else if (WhatAnimal == AnimalClassification.Herbivores) { return FavouriteFood.Plants; }
            return FavouriteFood.Everything;
        }
        public virtual void SayHello() { Console.WriteLine("Greatings."); }
    }

    [CommentAtt("Cow")]
    public class Cow : Animal
    {
        public Cow(string country, bool hide, string name, AnimalClassification what) : base(country, hide, name, what) { }
        public override FavouriteFood GetFavouriteFood() { return FavouriteFood.Plants; }
        public override void SayHello() { Console.WriteLine("Moo"); }
    }

    [CommentAtt("Lion")]
    public class Lion : Animal
    {
        public Lion(string country, bool hide, string name, AnimalClassification what) : base(country, hide, name, what) { }
        public override FavouriteFood GetFavouriteFood() { return FavouriteFood.Meat; }
        public override void SayHello() { Console.WriteLine("Roar"); }
    }

    [CommentAtt("Pig")]
    public class Pig : Animal
    {
        public Pig(string country, bool hide, string name, AnimalClassification what) : base(country, hide, name, what) { }
        public override FavouriteFood GetFavouriteFood() { return FavouriteFood.Everything; }
        public override void SayHello() { Console.WriteLine("Oink"); }
    }
}
