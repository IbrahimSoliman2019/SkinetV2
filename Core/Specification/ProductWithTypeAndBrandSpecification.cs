using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class
    ProductWithTypeAndBrandSpecification
    : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(
            ProductSpecParams productSpecParams
        ) :
            base(
                x =>
                    (
                    string.IsNullOrEmpty(productSpecParams.Search) ||
                    x.Name.ToLower().Contains(productSpecParams.Search)
                    ) &&
                    (
                    !productSpecParams.BrandId.HasValue ||
                    x.ProductBrandId == productSpecParams.BrandId
                    ) &&
                    (
                    !productSpecParams.TypeId.HasValue ||
                    x.ProductTypeId == productSpecParams.TypeId
                    )
            )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaginaging(productSpecParams.PageSize *
            (productSpecParams.PageIndex - 1),
            productSpecParams.PageSize);
            if (!string.IsNullOrEmpty(productSpecParams.sort))
            {
                switch (productSpecParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
