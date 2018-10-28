using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        // Goto : GetCollectValue Fonksiyonu recursive yazılacak
        static void Main(string[] args)
        {
            LolHero Vayne = new LolHero();
            #region Property
            Vayne.MyList = new List<int> { 1, 2, 3 };
            Vayne.MyNumbers = new int[] { 1, 2, 3 };
            Vayne.MyCharacters = new string[] { "a", "b", "c" };
            Vayne.Id = 26;
            Vayne.Name = "Best Adc";
            Vayne.NumberPi = 3.14;
            #endregion

            var prop = Vayne.GetType().GetProperties();
            bool[] IsCollection = IsICollectionArray(prop);
            int i = 0;
            foreach (var item in prop)
            {
                Console.Write(item.Name + " : ");
                if (IsCollection[i] == true)
                {
                    GetCollectionValue((ICollection)item.GetValue(Vayne));
                }
                else
                {
                    Console.Write(Vayne.GetType().GetProperty(item.Name).GetValue(Vayne, null));
                    Console.WriteLine("\n");
                }

                i++;
            }

            Console.ReadKey();
        }
        /// <summary>
        /// Collection Tipindeki nesnelerin içindeki değerleri yaz
        /// </summary>
        /// <param name="a"></param>
        public static void GetCollectionValue(ICollection a)
        {
            foreach (var item in a)
            {
                Console.Write(item + " , ");
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Class'ın propertylerinin ICollection sınıfının alt sınıfı olup olmadığını kontrol eder
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool[] IsICollectionArray(PropertyInfo[] prop)
        {
            Type ICollectionType = typeof(ICollection);
            bool[] boolList = new bool[prop.Length];
            int i = 0;
            foreach (var item in prop)
            {
                boolList[i] = (ICollectionType.IsAssignableFrom(item.PropertyType));
                i++;
            }
            return boolList;
        }
    }
}
