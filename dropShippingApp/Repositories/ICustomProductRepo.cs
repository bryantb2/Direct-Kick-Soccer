using System.Collections.Generic;
using dropShippingApp.Models;


namespace dropShippingApp.Repositories
{
	public interface ICustomProductRepo
	{
        List<ProductColor> OfferedColors { get; }
        List<ProductSize> OfferedSizes { get; }
        List<PricingHistory> PricingHistory { get; }

        void AddColor(ProductColor color);
        ProductColor RemoveColor(ProductColor color);

        void AddSize(ProductSize size);
        ProductSize RemoveSize(ProductSize size);
        void AddPriceChange(PricingHistory historyLog);
        PricingHistory RemovePriceChange(PricingHistory historyLog);

        CustomProduct AddCustomImage(CustomProduct Image);
        CustomProduct RemoveCustomImage(CustomProduct Image);

        CustomProduct AddCustomProduct(CustomProduct product);

        CustomProduct RemoveCustomProduct(CustomProduct product);

        CustomProduct GetProductBySKU(int id);
    }
}
