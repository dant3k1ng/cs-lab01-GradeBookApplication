using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base (name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) throw new InvalidOperationException("There must be at least 5 students");
                
            
                List<double> allGrades = GetAllGrades();
                List<double> betterGrades = GetBetterGrades(averageGrade, allGrades);

                int allGradesCount = allGrades.Count;
                int betterGradesCount = betterGrades.Count;
                float twentyPercentOfStudents = 0.2f * allGradesCount;

                if (betterGradesCount >= 4 * twentyPercentOfStudents) return 'A';
                else if (betterGradesCount >= 3 * twentyPercentOfStudents) return 'B';
                else if (betterGradesCount >= 2 * twentyPercentOfStudents) return 'C';
                else if (betterGradesCount >= twentyPercentOfStudents) return 'D';
                else return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5) { DisplayRankedGradingNotEnoughStudents(); return; }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5) { DisplayRankedGradingNotEnoughStudents(); return; }

            base.CalculateStudentStatistics(name);
        }

        public List<double> GetAllGrades()
        {
            List<double> grades = new List<double>();

            foreach (var student in Students)
            {
                grades.AddRange(student.Grades);
            }

            return grades;
        }

        public List<double> GetBetterGrades(double averageGrade, List<double> grades)
        {
            List<double> betterGrades = new List<double>();

            foreach (double grade in grades)
            {
                if (averageGrade > grade) betterGrades.Add(grade);
            }

            return betterGrades;
        }

        private void DisplayRankedGradingNotEnoughStudents(int minStudents = 5)
        {
            Console.WriteLine("Ranked grading requires at least {0} students.", minStudents);
        }
    }
}
