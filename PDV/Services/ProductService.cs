using PDV.Ultis.Moc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDV.Services
{
    public class ProductService
    {
        public ProductService(ProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        private readonly ProductRepository ProductRepository;

        public Task<List<Models.Product>> GetProducts(string searchTerm, CancellationToken cancellationToken)
        {
            return ProductRepository.GetProductsBy(searchTerm, cancellationToken);
        }
    }


    public class ProductRepository
    {
        private static readonly (decimal price, string name)[] TupleProductArray =
            new (decimal price, string name)[] {
                (10.00m,"Coca-Cola"),
                (10.00m,"Coco"),
                (10.00m,"Coc"),
                (98.00m,"Picanha"),
                (16.00m,"Carvão"),
                (22.90m,"Pão de alho") };

        readonly List<Models.Product> products;
        public ProductRepository()
        {
            products = (from pd in TupleProductArray
                        select new Models.Product(IncrementalId.Next, pd.name, pd.price)).
                        ToList();
        }

        public Task<List<Models.Product>> GetProductsBy(string searchTerm, CancellationToken cancellationToken)
        {
            return Task.Run(() => (from p in products
                                   let searchTerm = searchTerm.ToUpperInvariant()
                                   let productName = p.Name.ToUpperInvariant()
                                   where productName.Contains(searchTerm) || p.Id == searchTerm
                                   select p).ToList(), cancellationToken); ;
        }
    }
}
