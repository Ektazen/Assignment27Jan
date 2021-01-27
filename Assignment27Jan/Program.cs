using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Assignment27Jan
{
    [Serializable]
    class CalculateAge : IDeserializationCallback
    {
        public int DateOfBirth { get; set; }

        public int currentYr = DateTime.Now.Year;

        [NonSerialized]
        public int Age;
        public CalculateAge(int datebirth)
        {
            DateOfBirth = datebirth;

        }
        public void OnDeserialization(object sender)
        {
            Age = currentYr - DateOfBirth;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int yrOfBirth;
            Console.WriteLine("Enter the year of birth");
            yrOfBirth = int.Parse(Console.ReadLine());
            CalculateAge obj = new CalculateAge(yrOfBirth);
            FileStream fs = new FileStream(@"AgeTillToday", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj);
            fs.Seek(0, SeekOrigin.Begin);
            CalculateAge ca = (CalculateAge)bf.Deserialize(fs);
            Console.WriteLine("The age till current year is  " + ca.Age+" years");

        }
    }
}
