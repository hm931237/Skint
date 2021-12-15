using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductTypeAndBrandSpecifications()
        {
            
            AddInclude(x=>x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
        public ProductTypeAndBrandSpecifications(int id)
            :base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
