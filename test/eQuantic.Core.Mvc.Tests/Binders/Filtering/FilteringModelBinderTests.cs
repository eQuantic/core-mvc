using System.Globalization;
using eQuantic.Core.Mvc.Binders.Filtering;
using eQuantic.Linq.Filter;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Primitives;

namespace eQuantic.Core.Mvc.Tests.Binders.Filtering;

[TestFixture]
public class FilteringModelBinderTests
{
    [Test]
    public async Task FilteringModelBinder_BindModelAsync_successfully()
    {
        var binder = new FilteringModelBinder();
        var compositeMetadataDetailsProvider =
            new DefaultCompositeMetadataDetailsProvider(new []{ new DefaultBindingMetadataProvider()});
        var context = new DefaultModelBindingContext
        {
            ModelName = "filterBy",
            ModelState = new ModelStateDictionary(),
            ModelMetadata = new DefaultModelMetadata(
                new DefaultModelMetadataProvider(compositeMetadataDetailsProvider), 
                compositeMetadataDetailsProvider,
                new DefaultMetadataDetails( ModelMetadataIdentity.ForType(typeof(IFiltering<>)), ModelAttributes.GetAttributesForType(typeof(IFiltering<>)))
            ),
            ValueProvider = new QueryStringValueProvider(
                new BindingSource(Guid.NewGuid().ToString(), "filterBy", false, true),
                new QueryCollection(new Dictionary<string, StringValues>
                {
                    { "filterBy", new StringValues("and(startedAt:gte(0001-01-01),startedAt:lte(2023-06-30))")}
                }),
                CultureInfo.CurrentCulture
            )
        };
        await binder.BindModelAsync(context);
    }
}