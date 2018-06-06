using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISD_Trial_Test
{
    enum Gender
    {
        Male, Female, Other
    }

    interface IAdult
    {
        void Work();
    }


    abstract class Human
    {
        [NonSerialized]
        protected Gender gender;

        public virtual string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (Age > 18)
                    gender = value;
            }
        }
        public void Walk()
        {
            Console.WriteLine(Name + "(" + Gender.ToString() + ")" + " walking");
        }
    }

    class Parent : Human, IAdult
    {
        public Parent(string name, Gender gender, int age)
        {
            Name = name;
            Age = age;
            this.gender = gender;
        }

        public void Work()
        {
            Console.WriteLine(Name + " is working");
        }
    }


    class Child : Human
    {
        [NonSerialized]
        private string name;

        public delegate void Action(string message);
        public event Action doSomething;

        public override string Name
        {
            get { return name; }
            set
            {
                if (Age > 16)
                {
                    name = value;
                        doSomething("name was chenged");
                }
            }
        }

        public Child(string name, Gender gender, int age)
        {
            this.name = name;
            this.gender = gender;
            Age = age;
        }
    }


    class Program
    {
        private static void ShowAction(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            List<Human> family = new List<Human>();

            Parent mom = new Parent("Marry", Gender.Female, 32);
            Parent dad = new Parent("Bill", Gender.Male, 35);
            Child baby = new Child("Ketty", Gender.Female, 1);

            family.Add(mom);
            family.Add(dad);
            family.Add(baby);

            foreach (Human person in family)
            {
                person.Walk();
                if (person is IAdult)
                {
                    (person as IAdult).Work();
                }
                Console.WriteLine(new string('-', 20));
            }
            Console.WriteLine(new string('=', 20));

            mom.Gender = Gender.Other;
            baby.Gender = Gender.Other;

            foreach (Human person in family)
            {
                person.Walk();
                if (person is IAdult)
                {
                    (person as IAdult).Work();
                }
                Console.WriteLine(new string('-', 20));
            }

            Console.WriteLine("passed 17 years");

            baby.doSomething += ShowAction;
            family[2].Age = 18;
            family[2].Name = "Supid";
            Console.WriteLine(family[2].Name);

        }
    }
}
