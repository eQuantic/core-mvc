using eQuantic.Core.Mvc.Binders.Filtering;

namespace eQuantic.Core.Mvc.Tests.Binders.Filtering;

[TestFixture]
public class FilteringParserTests
{
    [TestCase("and(startedAt:gte(0001-01-01),startedAt:lte(2023-06-30))")]
    public void FilteringParser_Parse(string values)
    {
        var result = FilteringParser.Parse(values);
        
        Assert.That(result.Count(), Is.EqualTo(1));
    }
}