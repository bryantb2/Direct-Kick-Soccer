using System.Collections.Generic;
using dropShippingApp.Models;


namespace dropShippingApp.Repositories
{
	public interface ICustomProductRepo
	{
        List<ProductColor> OfferedColors { get; }
        List<ProductSize> OfferedSizes { get; }
        List<PricingHistory> PricingHistory { get; }
        List<CustomProduct> CustomProducts { get; }

        void AddColor(ProductColor color);
        ProductColor RemoveColor(ProductColor color);

        void AddSize(ProductSize size);
        ProductSize RemoveSize(ProductSize size);
        void AddPriceChange(PricingHistory historyLog);
        PricingHistory RemovePriceChange(PricingHistory historyLog);

        void AddCustomImage(string image);
        void RemoveCustomImage(string image);

        void AddCustomProduct(CustomProduct product);

        void RemoveCustomProduct(CustomProduct product);
    }
}
