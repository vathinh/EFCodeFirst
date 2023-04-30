using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodeFirstDemo.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public bool CorrectAnswer { get; set; }
        public string AnswerContent { get; set; }
        public virtual Question Question { get; set; }
    }
}
