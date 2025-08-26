using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetWithQueryParams;

public class GetTestWithQueryParamsQuery : IRequest<GetTestWithQueryParamsQuery>
{
    public string TestStringParam { get; set; }
    
    public string? TestNullableStringParam { get; set; }
    
    public string? TestNullableStringParam2 { get; set; }
    
    public int? TestNullableIntParam { get; set; }
    
    public int? TestNullableIntParam2 { get; set; }
    
    public int TestIntParam { get; set; }
    
    public DateTime DateTimeParam { get; set; }
    
    public DateTimeOffset DateTimeOffsetParam { get; set; }
    
    public string[] ArrayParam { get; set; }
    
    public List<string> ListParam { get; set; }
    
    public GetTestWithQueryParamsEnum TestEnumParam { get; set; } 
    
    public GetTestWithQueryInnerObject InnerObject { get; set; }
}