using eQuantic.Core.Mvc.Binders.Filtering;
using eQuantic.Linq.Filter;

namespace eQuantic.Core.Mvc.Tests.Binders.Filtering;

[TestFixture]
public class FilteringParserTests
{
    [TestCase("and(startedAt:gte(0001-01-01),startedAt:lte(2023-06-30))")]
    public void FilteringParser_Parse(string values)
    {
        var result = FilteringParser.Parse(values);
        var composite = result.ElementAt(0);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(composite, Is.InstanceOf<CompositeFiltering>());
        });
    }
}