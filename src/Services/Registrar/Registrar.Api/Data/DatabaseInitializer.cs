using System.Collections.Generic;
using System.Linq;

namespace Registrar.Api.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(RegistrarContext context)
        {
            //context.Database.EnsureCreated();

            var subjects = SeedSubjects(context);
            var instructors = SeedInstructors(context);
            SeedCourses(context, subjects, instructors);

            context.SaveChanges();
        }

        private static IList<Subject> SeedSubjects(RegistrarContext context)
        {
            if (context.Subjects.Any())
            {
                return new Subject[] { };
            }
            else
            {
                var subjects =
                    new Subject[]
                    {
                        new Subject()
                        {
                            NameEn = "Computer Science"
                        },
                        new Subject()
                        {
                            NameEn = "Public Health"
                        }
                    };

                foreach (Subject s in subjects)
                {
                    context.Subjects.Add(s);
                }

                return subjects;
            }
        }

        private static IList<Instructor> SeedInstructors(RegistrarContext context)
        {
            if (context.Instructors.Any())
            {
                return new Instructor[] { };
            }
            else
            {
                var instructors =
                    new Instructor[]
                    {
                       new Instructor()
                       {
                           Name = "Bill Gates",
                           TitleEn = "Dr.",
                           Email = "bill.gates@email.com",
                           PhoneNumber = "613-555-0120"
                       },
                       new Instructor()
                       {
                           Name = "Philiova Pavloski",
                           TitleEn = "Dr.",
                           Email = "philipiova.pavloski@email.com",
                           PhoneNumber = "613-555-0130"
                       }
                    };

                foreach (Instructor i in instructors)
                {
                    context.Instructors.Add(i);
                }

                return instructors;
            }
        }

        private static IList<Course> SeedCourses(RegistrarContext context, IList<Subject> subjects, IList<Instructor> instructors)
        {
            if (context.Courses.Any())
            {
                return new Course[] { };
            }
            else
            {
                var courses =
                    new Course[]
                    {
                        new Course()
                        {
                            Subject = subjects.First(),
                            ReferenceCode = "1001",
                            TitleEn = "Introduction to Computer Science",
                            DescriptionEn = string.Empty,
                            Instructor = instructors.Where(i => i.Name == "Bill Gates").Single()
                        },
                        new Course()
                        {
                            Subject = subjects.First(),
                            ReferenceCode = "1002",
                            TitleEn = "Introduction to Programming",
                            DescriptionEn = "A delightful introduction to the fantastic world of computer programming.",
                            Instructor = instructors.Where(i => i.Name == "Bill Gates").Single()
                        },
                        new Course()
                        {
                            Subject = subjects.Where(s => s.NameEn == "Public Health").Single(),
                            ReferenceCode = "1001",
                            TitleEn = "History of Epidemiology",
                            DescriptionEn = string.Empty,
                            Instructor = instructors.Where(i => i.Name == "Philiova Pavloski").Single()
                        }
                    };

                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }

                return courses;
            }
        }
    }
}
