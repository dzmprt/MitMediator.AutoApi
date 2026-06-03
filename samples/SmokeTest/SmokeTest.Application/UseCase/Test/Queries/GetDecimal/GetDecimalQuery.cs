using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetDecimal;

public sealed class GetDecimalQuery : KeyRequest<decimal>, IRequest<decimal>;
