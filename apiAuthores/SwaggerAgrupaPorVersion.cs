using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace apiAuthores
{
    public class SwaggerAgrupaPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceControlador = controller.ControllerType.Namespace;
            var versionApi = namespaceControlador.Split('.').Last().ToLower();
            controller.ApiExplorer.GroupName = versionApi;
        }
    }
}
