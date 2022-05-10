using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorDaprDemo.FakeUser.Handlers
{
    // https://github.com/dotnet/aspnetcore/blob/a450cb69b5e4549f5515cdb057a68771f56cefd7/src/Components/WebAssembly/WebAssembly.Authentication/src/Services/AuthorizationMessageHandler.cs#L16
    public class FakeUserMessageHandler : DelegatingHandler
    {
        public FakeUserMessageHandler()
        {
        }

        protected override async Task<HttpResponseMessage>
          SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
