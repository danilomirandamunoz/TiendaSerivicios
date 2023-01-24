using System.Diagnostics;

namespace TiendaServicios.Api.Gateway.Handlers
{
    public class LibroHandler: DelegatingHandler
    {
        private readonly ILogger<LibroHandler> logger;

        public LibroHandler(ILogger<LibroHandler> logger)
        {
            this.logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            var tiempo = Stopwatch.StartNew();
            logger.LogInformation("Inicio Request Libro by ID ");
            var response = await base.SendAsync(httpRequestMessage, cancellationToken);

            logger.LogInformation($" Fin del request en {tiempo.ElapsedMilliseconds}ms");


            return response;
        }
    }
}
