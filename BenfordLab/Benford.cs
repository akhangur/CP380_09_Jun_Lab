using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BenfordLab
{
    public class BenfordData
    {
        public int Digit { get; set; }
        public int Count { get; set; }

        public BenfordData() { }
    }

    public class Benford
    {
       
        public static BenfordData[] calculateBenford(string csvFilePath)
        {
            // load the data
            var data = File.ReadAllLines(csvFilePath)
                .Skip(1) // For header
                .Select(s => Regex.Match(s, @"^(.*?),(.*?)$"))
                .Select(data => new
                {
                    Country = data.Groups[1].Value,
                    Population = int.Parse(data.Groups[2].Value)
                });

            // manipulate the data!
            //
            // Select() with:
            //   - Country
            //   - Digit (using: FirstDigit.getFirstDigit() )
            // 
            var meta = data
                .Select(meta => new
                {
                    Country = meta.Country,
                    Digit = FirstDigit.getFirstDigit(meta.Population)
                });
            // Then:
            //   - you need to count how many of *each digit* there are
            //
            int[] arr = new int[9];
            int[] number = new int[9];
            for(var i=0; i<9;i++)
            {
                number[i] = i+1;
                var k = 0;
                foreach (var info in meta)
                {
                    if (number[i] == info.Digit)
                    {
                        k++;
                    }
                }
                arr[i] = k;


            }






            // Lastly:
            //   - transform (select) the data so that you have a list of
            //     BenfordData objects
            //
            BenfordData[] ob = new BenfordData[9];
            for (var i=0; i<9; i++)
            {
                BenfordData obj = new BenfordData()
                {
                    Count = arr[i],
                    Digit = number[i]
                };
                ob[i] = obj;
            }

            var m = ob ;

            return m.ToArray();
        }
    }
}
