using System.Threading;
using System.Threading.Tasks;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Articles
{
    [Route("api/articles")]
    public class ArticlesController : Controller
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<ArticlesEnvelope> Get([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] int? limit, [FromQuery] int? offset, CancellationToken cancellationToken)
        {
            return _mediator.Send(new List.Query(tag, author, favorited, limit, offset), cancellationToken);
        }

        [HttpGet("feed")]
        public Task<ArticlesEnvelope> GetFeed([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] int? limit, [FromQuery] int? offset, CancellationToken cancellationToken)
        {
            return _mediator.Send(new List.Query(tag, author, favorited, limit, offset)
            {
                IsFeed = true
            });
        }

        [HttpGet("{slug}")]
        public Task<ArticleEnvelope> Get(string slug, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Details.Query(slug), cancellationToken);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public Task<ArticleEnvelope> Create([FromBody]Create.Command command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        [HttpPut("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public Task<ArticleEnvelope> Edit(string slug, [FromBody]Edit.Command command, CancellationToken cancellationToken)
        {
            command.Slug = slug;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public Task Delete(string slug, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Delete.Command(slug), cancellationToken);
        }
    }
}