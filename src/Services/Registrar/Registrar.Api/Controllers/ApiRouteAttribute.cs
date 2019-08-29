using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace Registrar.Api.Controllers
{
    public class ApiRouteAttribute : Attribute, IRouteTemplateProvider
    {
        public string Template => "api/v1/[controller]";

        public int? Order { get; set; }

        public string Name { get; set; }
    }
}
