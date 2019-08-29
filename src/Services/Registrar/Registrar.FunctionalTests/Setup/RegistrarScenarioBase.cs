namespace Registrar.FunctionalTests.Setup
{
    public class RegistrarScenarioBase
    {
        private const string ApiUrlBase = "api/v1/courses";

        public static class Get
        {
            public static string GetCourses()
            {
                return $"{ApiUrlBase}";
            }
        }
    }
}
