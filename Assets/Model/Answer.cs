using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Model
{
    public class Answer
    {
        public Answer(string text, bool iscorrect)
        {
            Text = text;
            isCorrect = iscorrect;
        }

        public string Text { get; set; }
        public bool isCorrect { get; set; }
    }
}
