using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Models;
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
        public async Task<bool> DisableProducto(int idProducto)
        {
            return await dataAccess.DisableProducto(idProducto);
        }

        public async Task<bool> EnableProducto(int idProducto)
        {
            return await dataAccess.EnableProducto(idProducto);
        }

        public async Task<bool> UpdateProducto(int idProducto, UpdateProductoApiModel productoApi)
        {
            return await dataAccess.UpdateProducto(idProducto, productoApi);
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
