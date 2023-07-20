
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthmapMaker
{
    internal class ObjLoader
    {
        private int[] sequence = { 1, 2, 4, 4, 2, 3 };
        private List<float> verticies = new List<float>();
        private List<uint> indicies = new List<uint>();

        public Model LoadFile(String filename) {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {


                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("v "))
                        {
                            string[] splitline = line.Split(' ');
                            for (int i = 1; i < splitline.Length; i++)
                            {
                                if (splitline[i] == "")
                                {
                                    //accounts for double spaces present in some obj files
                                    //probably a cleaner way to do this in the split
                                    continue;
                                }
                                verticies.Add(float.Parse(splitline[i], CultureInfo.InvariantCulture.NumberFormat));
                            }
                        }
                        else if (line.StartsWith("f "))
                        {
                            string[] splitline = line.Split(' ');
                            //accounting for quads by breaking them down into tris
                            if (splitline.Length > 4)
                            {
                                foreach (int i in sequence)
                                {
                                    string[] faceSplit = splitline[i].Split('/');
                                    indicies.Add(uint.Parse(faceSplit[0], CultureInfo.InvariantCulture.NumberFormat) - 1);
                                }
                            }
                            else {
                                for (int i = 1; i < splitline.Length; i++)
                                {
                                    //could use regex to access string and pull the first number and save on memory
                                    string[] faceSplit = splitline[i].Split('/');
                                    indicies.Add(uint.Parse(faceSplit[0], CultureInfo.InvariantCulture.NumberFormat) - 1);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Model(verticies, indicies);
        }
    }
}