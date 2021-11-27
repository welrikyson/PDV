using System.Collections.Generic;
using System.Linq;

namespace PDV.Ultis.Moc
{
    public static class Mock
    {
        private static readonly (decimal price, string name, int count)[] TupleCartItemArray =
            new (decimal price, string name, int count)[] {
                (10.00m,"Coca-Cola",2),
                (98.00m,"Picanha",1),
                (16.00m,"Carvão",1),
                (22.90m,"Pão de alho",2),
                (10.00m,"Coca-Cola",2),
                (98.00m,"Picanha",1),
                (16.00m,"Carvão",1),
                (22.90m,"Pão de alho",2),
                (10.00m,"Coca-Cola",2),
                (98.00m,"Picanha",1),
                (16.00m,"Carvão",1),
                (22.90m,"Pão de alho",2),
                (16.00m,"Carvão",1), };

        public static List<Models.ChartItem> CartItems
        =>
            (from c in TupleCartItemArray
             let product = new Models.Product(IncrementalId.Next, c.name, c.price)
             select new Models.ChartItem(product, c.count)).ToList();

    }
}