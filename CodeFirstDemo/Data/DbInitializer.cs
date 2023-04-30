using System.Collections.Generic;
using CodeFirstDemo.Models;

namespace CodeFirstDemo.Data
{
    public static class DbInitializer
    {
        public static void SeedData(BlogDbContext context)
        {
            // Add surveys
            var surveys = new List<Survey>
            {
                new Survey
                {
                    Title = "Survey 1",
                    Description = "This is survey 1",
                    Img = "survey1.jpg",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Title = "Question 1",
                            Type = "Multiple Choice",
                            QuestionContent = "What is your favorite color?",
                            Answers = new List<Answer>
                            {
                                new Answer { CorrectAnswer = true, AnswerContent = "Blue" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Red" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Green" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Yellow" }
                            }
                        },
                        new Question
                        {
                            Title = "Question 2",
                            Type = "True/False",
                            QuestionContent = "Is the sky blue?",
                            Answers = new List<Answer>
                            {
                                new Answer { CorrectAnswer = true, AnswerContent = "True" },
                                new Answer { CorrectAnswer = false, AnswerContent = "False" }
                            }
                        }
                    }
                },
                new Survey
                {
                    Title = "Survey 2",
                    Description = "This is survey 2",
                    Img = "survey2.jpg",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Title = "Question 1",
                            Type = "Multiple Choice",
                            QuestionContent = "What is your favorite food?",
                            Answers = new List<Answer>
                            {
                                new Answer { CorrectAnswer = true, AnswerContent = "Pizza" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Burgers" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Tacos" },
                                new Answer { CorrectAnswer = false, AnswerContent = "Sushi" }
                            }
                        },
                        new Question
                        {
                            Title = "Question 2",
                            Type = "True/False",
                            QuestionContent = "Is the earth flat?",
                            Answers = new List<Answer>
                            {
                                new Answer { CorrectAnswer = false, AnswerContent = "True" },
                                new Answer { CorrectAnswer = true, AnswerContent = "False" }
                            }
                        }
                    }
                }
            };
            context.Surveys.AddRange(surveys);
            context.SaveChanges();
        }
    }
}
