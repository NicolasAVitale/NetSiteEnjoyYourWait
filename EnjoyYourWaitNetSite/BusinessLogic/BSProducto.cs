using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSProducto : BSBase
    {
        public BSProducto()
        {
        }

        public async Task<bool> CreateProducto(Producto producto)
        {
            return await dataAccess.CreateProducto(producto);
        }
        public async Task<bool> DeleteProducto(int idProducto)
        {
            return await dataAccess.DeleteProducto(idProducto);
        }

        public async Task<bool> UpdateProducto(int idProducto)
        {
            return await dataAccess.UpdateProducto(idProducto);
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            return await dataAccess.GetAllProductos();
        }

        public async Task<List<TipoProducto>> ObtenerTiposProducto()
        {
            return await dataAccess.GetAllTiposProducto();
        }
    }
}
