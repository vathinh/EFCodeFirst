using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "Surveys",
             columns: new[] { "SurveyId", "Title", "Description", "Img" },
             values: new object[,]
             {
                    { 1, "Survey 1", "Description for Survey 1", "img1.jpg" },
                    { 2, "Survey 2", "Description for Survey 2", "img2.jpg" },
                    { 3, "Survey 3", "Description for Survey 3", "img3.jpg" }
             });

            migrationBuilder.InsertData(
              table: "Questions",
              columns: new[] { "QuestionId", "Title", "Type", "QuestionContent", "SurveyId" },
              values: new object[,]
              {
                    { 1, "Question 1", "Multiple Choice", "What is the capital of France?", 1 },
                    { 2, "Question 2", "True or False", "Is the earth flat?", 1 },
                    { 3, "Question 3", "Short Answer", "What is the largest mammal?", 2 },
                    { 4, "Question 4", "Multiple Choice", "Which country is known as the Land of the Rising Sun?", 2 },
                    { 5, "Question 5", "True or False", "Is the moon a planet?", 3 }
              });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "CorrectAnswer", "AnswerContent", "QuestionId" },
                values: new object[,]
                {
                    { 1, true, "Answer 1", 1 },
                    { 2, false, "Answer 2", 1 },
                    { 3, false, "Answer 3", 1 },
                    { 4, false, "Answer 4", 1 },
                    { 5, true, "Answer 1", 2 },
                    { 6, false, "Answer 2", 2 },
                    { 7, false, "Answer 3", 2 },
                    { 8, false, "Answer 4", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all answers
            migrationBuilder.Sql("DELETE FROM Answers");

            // Delete all questions
            migrationBuilder.Sql("DELETE FROM Questions");

            // Delete all surveys
            migrationBuilder.Sql("DELETE FROM Surveys");

        }
    }
}
