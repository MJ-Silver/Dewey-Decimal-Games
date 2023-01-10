using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ST10121065 Michael Silver
namespace Prog7312
{
    public class books

    {
        public double randomNum { get; set; }
        public string author { get; set; }

        public books()
        {

        }
        public books(double randomNum, string author)
        {
            this.randomNum = randomNum;
            this.author = author;
        }

        public override string ToString()
        {
            return $"{randomNum} {author}";
        }
    }
}
