using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneratorForHW.Model
{
    class DataGenerator
    {
        public int Count { get; set; } = 1000;
        public string TemplateFilename { get; set; } = "data.txt";
        public Random Random { get; } = new Random();
    }

}
