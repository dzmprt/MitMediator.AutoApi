using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetWithQueryParams;

public class GetTestWithQueryParamsQuery : KeyRequest<int>, IRequest<string>
{
    public required string TestStringParam { get; set; }
    
    public string? TestNullableStringParam { get; set; }
    
    public string? TestNullableStringParam2 { get; set; }
    
    public int? TestNullableIntParam { get; set; }
    
    public int? TestNullableIntParam2 { get; set; }
    
    public int TestIntParam { get; set; }
    
    public DateTime DateTimeParam { get; set; }
    
    public DateTimeOffset DateTimeOffsetParam { get; set; }
    
    public required string[] ArrayParam { get; set; }
    
    public required List<string> ListParam { get; set; }
    
    public GetTestWithQueryParamsEnum TestEnumParam { get; set; } 
    
    public required GetTestWithQueryInnerObject InnerObject { get; set; }
}